<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/Pruebas/Prueba.Master" AutoEventWireup="true" CodeBehind="VentanaTecnicaPC.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaTecnicaPC" %>

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

            Table td .subrayado {
                max-width: 600px;
                border-bottom: 1px dotted gray;
            }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPruebas" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest" ClientEvents-OnResponseEnd="retorno">
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
            var a = new Array();
            var vPruebaEstatus = "";
            var c = "";
            window.onload = function (sender, args) {
                if ('<%=this.vTipoRevision%>' != "REV" && '<%=this.vTipoRevision%>' != "EDIT") {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                        if (shouldSubmit) {
                            var ajaxManager = $find("<%=RadAjaxManager1.ClientID%>");
                            ajaxManager.ajaxRequest(null);
                            var segundos = '<%=vTiempoPrueba%>';
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

                                var pane = $find("<%= radPanelPreguntas.ClientID %>");
                                pane.collapse();
                            }
                        }
                        else {
                            //window.close();
                            window.location = "Default.aspx?ty=Ini";
                        }
                    });
                    var text = "<label><b>Instrucciones:</b><br/>Contesta SI o NO, en cada una de las opciones A, B, C y D (por favor contesta todas las opciones).</label>";
                    radconfirm(JustificarTexto(text), callBackFunction, 950, 600, null, "Técnica PC");
                }
            };

            function retorno(sender, args) {
                var segundos = '<%=this.vTiempoPrueba%>';
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

                setTimeout(function () {
                    var pane = $find("<%= radPanelPreguntas.ClientID %>");
                    pane.expand();
                }, 1000);                
            }

            function OpenReport() {
                var vURL = "ReporteadorPruebasIDP.aspx";
                var vTitulo = "Impresión Técnica PC";

                var IdPrueba = '<%= vIdPrueba %>';
                var ClToken = '<%= vClToken %>';



                var windowProperties = {
                    width: document.documentElement.clientWidth - 20,
                    height: document.documentElement.clientHeight - 20
                };

                vURL = vURL + "?IdPrueba=" + IdPrueba + "&ClToken=" + ClToken + "&ClPrueba=TECNICAPC";
                var win = window.open(vURL, '_blank');
                win.focus();
                //var wnd = openChildDialog(vURL, "winVistaPrevia", vTitulo, windowProperties);
            }

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
                    radconfirm(text, callBackFunction, 400, 160, null, "");
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
                //var oWnd = radalert("Usted ha terminado su prueba exitosamente o el tiempo de aplicación de la prueba ha concluido. <br> Recuerde que no es posible volver a ingresar la prueba previa; si intenta hacerlo por medio del botón del navegador, la aplicación no te lo permitirá: se generará un error y el intento quedará registrado", 400, 300, "");
                var oWnd = radalert("El tiempo de la aplicación de la prueba ha concluido. Recuerda que no es posible regresar a ella, si intentas hacerlo a través del botón del navegador, la aplicación no lo permitirá, generando un error y registrando el intento.", 400, 300, "");
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


    <label style="font-size: 21px;">Técnica PC</label>
    <div style="height: calc(100% - 100px);">

        <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Horizontal">

            <telerik:RadPane ID="rpnAyudaTexto" runat="server" Height="30" Width="100%" Scrolling="None">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="RSPHelp" runat="server" Title="Instrucciones" Width="100%" Height="200">
                        <div style="margin: 10px; text-align: justify;">
                            <p>
                                <label id="Label104" runat="server">Contesta SI o NO, en cada una de las opciones A, B, C y D (por favor contesta todas las opciones).</label></p>
                        </div>
                    </telerik:RadSlidingPane>

                </telerik:RadSlidingZone>

            </telerik:RadPane>

            <telerik:RadPane ID="radPanelPreguntas" runat="server">


                <div style="width: 95%; margin-left: 2%; margin-right: 2%;">
                    <table>
                        <thead>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td colspan="3">
                                    <label style="font-weight: bold;" runat="server" name="">Comunicación</label>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3">
                                    <label id="Label1" style="font-weight: bold;" runat="server" name="">
                                        1. Una Smart Tv permite:
                                    </label>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label2" style="margin-left: 5px;" runat="server" name="">
                                        a. Recibir señal digitial de televisión
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta1Resp1SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1A"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>

                                <td>
                                    <telerik:RadButton ID="btnPregunta1Resp1NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1A"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                            <tr>
                                <td class="subrayado">
                                    <label id="Label3" style="margin-left: 5px;" runat="server" name="">
                                        b. Ver contenido streaming a través de aplicaciones preinstaladas
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta1Resp2SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1B"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta1Resp2NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1B"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label4" style="margin-left: 5px;" runat="server" name="">
                                        c. Grabar videos desde una cámara integrada
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta1Resp3SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1C"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta1Resp3NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1C"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label5" style="margin-left: 5px;" runat="server" name="">
                                        d. Acceder a páginas de Internet
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta1Resp4SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1D"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta1Resp4NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1D"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3">&nbsp</td>
                            </tr>
                            <%--Pregunta 2--%>

                            <tr>
                                <td colspan="3">
                                    <label id="Label6" style="font-weight: bold;" runat="server" name="">
                                        2. El conmutador telefónico:
                                    </label>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label7" style="margin-left: 5px;" runat="server" name="">
                                        a. Permite la recepción y distribución de llamadas
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta2Resp1SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2A"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta2Resp1NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2A"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label9" style="margin-left: 5px;" runat="server" name="">
                                        b. Controla las líneas teléfonicas
                                    </label>
                                </td>

                                <td>
                                    <telerik:RadButton ID="btnPregunta2Resp2SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2B"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta2Resp2NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2B"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label76" style="margin-left: 5px;" runat="server" name="">
                                        c. Permite el uso de video llamadas
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta2Resp3SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2C"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta2Resp3NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2C"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label10" style="margin-left: 5px;" runat="server" name="">
                                        d. Funciona como agenda telefónica
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta2Resp4SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2D"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta2Resp4NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2D"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3">&nbsp</td>
                            </tr>

                            <%--Pregunta 3--%>

                            <tr>
                                <td colspan="3">
                                    <label id="Label11" style="font-weight: bold;" runat="server" name="">3. Un teléfono móvil te permite:</label>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label12" style="margin-left: 5px;" runat="server" name="">a. Permite hacer llamadas telefónicas</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta3Resp1SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3A"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta3Resp1NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3A"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label13" style="margin-left: 5px;" runat="server" name="">b. Permite enviar mensajes de texto</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta3Resp2SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3B"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta3Resp2NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3B"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label14" style="margin-left: 5px;" runat="server" name="">c.	Contribuye a mejorar el medio ambiente </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta3Resp3SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3C"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta3Resp3NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3C"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label15" style="margin-left: 5px;" runat="server" name="">d. Contribuye a mejorar la comunicación entre las familias</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta3Resp4SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3D"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta3Resp4NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3D"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3">&nbsp</td>
                            </tr>
                            <%--Pregunta 4--%>
                            <tr>
                                <td colspan="3">
                                    <label id="Label16" style="font-weight: bold;" runat="server" name="">
                                        4. Una tablet:
                                    </label>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label17" style="margin-left: 5px;" runat="server" name="">
                                        a. Te permite el uso de herramientas de office sin estar conectado a Internet
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta4Resp1SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4A"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta4Resp1NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4A"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label18" style="margin-left: 5px;" runat="server" name="">
                                        b. Tiene una cámara incorporada que permite grabar video y toma de fotografías en alta definición
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta4Resp2SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4B"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta4Resp2NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4B"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label19" style="margin-left: 5px;" runat="server" name="">
                                        c. Requiere un teclado externo para el uso de las aplicaciones
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta4Resp3SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4C"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta4Resp3NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4C"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label20" style="margin-left: 5px;" runat="server" name="">
                                        d. Tiene una batería de mayor duración que la de una computadora portátil
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta4Resp4SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4D"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta4Resp4NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4D"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3">&nbsp</td>
                            </tr>
                            <%--Pregunta 5--%>

                            <tr>
                                <td colspan="3">
                                    <label id="Label21" style="font-weight: bold;" runat="server" name="">
                                        5. Un Smartphone:
                                    </label>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label22" style="margin-left: 5px;" runat="server" name="">
                                        a. Sirve como cajero automático
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta5Resp1SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5A"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta5Resp1NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5A"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label23" style="margin-left: 5px;" runat="server" name="">
                                        b. Permite el envío y recepción de correos electrónicos
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta5Resp2SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5B"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta5Resp2NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5B"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label24" style="margin-left: 5px;" runat="server" name="">
                                        c. Permite al usuario realizar transacciones bancarias a través de aplicaciones especializadas
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta5Resp3SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5C"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta5Resp3NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5C"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                            <tr>
                                <td class="subrayado">
                                    <label id="Label25" style="margin-left: 5px;" runat="server" name="">
                                        d. Permite el acceso a redes sociales como Facebook o Twiter
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta5Resp4SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5D"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta5Resp4NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5D"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3">&nbsp</td>
                            </tr>
                            <%--Pregunta 6--%>
                            <tr>
                                <td colspan="3">
                                    <label id="Label8" style="font-weight: bold;" runat="server" name="">Software</label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <label id="Label26" style="font-weight: bold;" runat="server" name="">6. Microsoft Excel sirve para:</label>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label27" style="margin-left: 5px;" runat="server" name="">
                                        a. Graficar información
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta6Resp1SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6A"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta6Resp1NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6A"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label28" style="margin-left: 5px;" runat="server" name="">
                                        b. Reproducir videos
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta6Resp2SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6B"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta6Resp2NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6B"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label29" style="margin-left: 5px;" runat="server" name="">
                                        c. Crear bases de datos
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta6Resp3SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6C"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>

                                <td>
                                    <telerik:RadButton ID="btnPregunta6Resp3NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6C"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label30" style="margin-left: 5px;" runat="server" name="">
                                        d. Crear hojas de cálculo
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta6Resp4SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6D"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta6Resp4NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6D"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3">&nbsp</td>
                            </tr>
                            <%--Pregunta 7--%>
                            <tr>
                                <td colspan="3">
                                    <label id="Label31" style="font-weight: bold;" runat="server" name="">7. Microsoft Word sirve para:</label>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label32" style="margin-left: 5px;" runat="server" name="">
                                        a. Edición profesional de fotografías
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta7Resp1SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7A"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta7Resp1NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7A"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label33" style="margin-left: 5px;" runat="server" name="">
                                        b. Revisar ortografía
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta7Resp2SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7B"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta7Resp2NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7B"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label34" style="margin-left: 5px;" runat="server" name="">
                                        c. Generar y editar textos
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta7Resp3SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7C"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta7Resp3NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7C"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label35" style="margin-left: 5px;" runat="server" name="">
                                        d. Crear sobres y etiquetas personalizadas
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta7Resp4SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7D"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta7Resp4NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7D"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3">&nbsp</td>
                            </tr>
                            <%--Pregunta 8--%>
                            <tr>
                                <td colspan="3">
                                    <label id="Label36" style="font-weight: bold;" runat="server" name="">8. Microsoft PowerPoint sirve para:</label>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label37" style="margin-left: 5px;" runat="server" name="">
                                        a. Enviar correos masivos
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta8Resp1SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8A"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta8Resp1NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8A"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label38" style="margin-left: 5px;" runat="server" name="">b. Diseñar mapas mentales o conceptuales</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta8Resp2SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8B"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>

                                <td>
                                    <telerik:RadButton ID="btnPregunta8Resp2NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8B"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label39" style="margin-left: 5px;" runat="server" name="">c. Crear presentaciones gráficas	</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta8Resp3SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8C"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta8Resp3NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8C"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label40" style="margin-left: 5px;" runat="server" name="">d. Modificar imágenes prediseñadas </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta8Resp4SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8D"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta8Resp4NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8D"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3">&nbsp</td>
                            </tr>
                            <%--Pregunta 9--%>

                            <tr>
                                <td colspan="3">
                                    <label id="Label41" style="font-weight: bold;" runat="server" name="">9. Microsoft Outlook sirve para:</label>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label42" style="margin-left: 5px;" runat="server" name="">a. Crear dibujos</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta9Resp1SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9A"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta9Resp1NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9A"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label43" style="margin-left: 5px;" runat="server" name="">b. Enviar y recibir correo electrónico</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta9Resp2SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9B"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta9Resp2NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9B"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label44" style="margin-left: 5px;" runat="server" name="">
                                        c. Descargar música de Internet
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta9Resp3SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9C"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta9Resp3NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9C"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label45" style="margin-left: 5px;" runat="server" name="">
                                        d. Administrar tu lista de contactos
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta9Resp4SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9D"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta9Resp4NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9D"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3">&nbsp</td>
                            </tr>
                            <%--Pregunta 10--%>

                            <tr>
                                <td colspan="3">
                                    <label id="Label46" style="font-weight: bold;" runat="server" name="">10. Las siguientes fórmulas de Microsoft Excel son correctas:</label>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label47" style="margin-left: 5px;" runat="server" name="">a.	=SUMA(AZ+BD)</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta10Resp1SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10A"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta10Resp1NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10A"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label48" style="margin-left: 5px;" runat="server" name="">b. =PROMEDIO(C5:G5) </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta10Resp2SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10B"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta10Resp2NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10B"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label49" style="margin-left: 5px;" runat="server" name="">c.	SUMA A150:A169</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta10Resp3SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10C"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta10Resp3NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10C"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label50" style="margin-left: 5px;" runat="server" name="">d.	SUMA(A150:A169)</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta10Resp4SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10D"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta10Resp4NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10D"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3">&nbsp</td>
                            </tr>
                            <%--Pregunta 11--%>
                            <tr>
                                <td colspan="3">
                                    <label id="Label51" style="font-weight: bold;" runat="server" name="">
                                        11. Adobe Reader permite:
                                    </label>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label52" style="margin-left: 5px;" runat="server" name="">
                                        a. Realizar documentos de texto y gráficos
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta11Resp1SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11A"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta11Resp1NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11A"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label53" style="margin-left: 5px;" runat="server" name="">
                                        b. Reproducir archivos .MP3
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta11Resp2SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11B"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta11Resp2NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11B"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label54" style="margin-left: 5px;" runat="server" name="">
                                        c. Visualizar archivos digitalizados
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta11Resp3SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11C"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta11Resp3NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11C"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label55" style="margin-left: 5px;" runat="server" name="">
                                        d. Agregar comentarios a un archivo .pdf
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta11Resp4SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11D"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta11Resp4NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11D"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3">&nbsp</td>
                            </tr>
                            <%--Pregunta 12--%>

                            <tr>
                                <td colspan="3">
                                    <label id="Label107" style="font-weight: bold;" runat="server" name="">Internet</label>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3">
                                    <label id="Label56" style="font-weight: bold;" runat="server" name="">12. Un navegador de internet (Internet Explorer, FireFox, Opera) es: </label>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label57" style="margin-left: 5px;" runat="server" name="">a. Un videojuego</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta12Resp1SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12A"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta12Resp1NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12A"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label58" style="margin-left: 5px;" runat="server" name="">
                                        b. Un programa que muestra páginas HTML
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta12Resp2SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12B"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta12Resp2NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12B"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label59" style="margin-left: 5px;" runat="server" name="">
                                        c. Un programa que permite realizar búsquedas en la web
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta12Resp3SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12C"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta12Resp3NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12C"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label60" style="margin-left: 5px;" runat="server" name="">
                                        d. Una aplicación que permite visualizar documentos de texto
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta12Resp4SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12D"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta12Resp4NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12D"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3">&nbsp</td>
                            </tr>
                            <%--Pregunta 13--%>
                            <tr>
                                <td colspan="3">
                                    <label id="Label61" style="font-weight: bold;" runat="server" name="">13. Una dirección de página de Internet correcta es:</label>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label62" style="margin-left: 5px;" runat="server" name="">a. www.yahoo.com</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta13Resp1SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13A"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta13Resp1NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13A"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label63" style="margin-left: 5px;" runat="server" name="">b.	http://www.yahoo.com</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta13Resp2SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13B"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta13Resp2NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13B"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label64" style="margin-left: 5px;" runat="server" name="">c.	c:/yahoo.com</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta13Resp3SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13C"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta13Resp3NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13C"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label65" style="margin-left: 5px;" runat="server" name="">d.	www.yahoo@com</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta13Resp4SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13D"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta13Resp4NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13D"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3">&nbsp</td>
                            </tr>
                            <%--Pregunta 14--%>
                            <tr>
                                <td colspan="3">
                                    <label id="Label106" style="font-weight: bold;" runat="server" name="">14. Una dirección de correo electrónico correcta es:</label>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label72" style="margin-left: 5px;" runat="server" name="">a. Juanperez.ventas@sutienda.com</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta14Resp1SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14A"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta14Resp1NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14A"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label73" style="margin-left: 5px;" runat="server" name="">b. Juan perez lopez@su tienda.com.mx</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta14Resp2SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14B"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta14Resp2NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14B"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label74" style="margin-left: 5px;" runat="server" name="">c. juanperez@yahoo.com</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta14Resp3SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14C"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta14Resp3NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14C"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label75" style="margin-left: 5px;" runat="server" name="">d. JUANPEREZ@SUTIENDA.COM.MX</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta14Resp4SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14D"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta14Resp4NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14D"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3">&nbsp</td>
                            </tr>
                            <%--Pregunta 15--%>

                            <tr>
                                <td colspan="3">
                                    <label id="Label66" style="font-weight: bold;" runat="server" name="">
                                        15. Google y Yahoo son:
                                    </label>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label67" style="margin-left: 5px;" runat="server" name="">
                                        a. Páginas de búsqueda de internet
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta15Resp1SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15A"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta15Resp1NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15A"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label68" style="margin-left: 5px;" runat="server" name="">
                                        b. Sistemas de mensajería instantánea
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta15Resp2SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15B"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta15Resp2NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15B"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label69" style="margin-left: 5px;" runat="server" name="">
                                        c. Servidores de correo web
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta15Resp3SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15C"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta15Resp3NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15C"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label70" style="margin-left: 5px;" runat="server" name="">d. Apps para almacenamiento de información</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta15Resp4SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15D"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta15Resp4NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15D"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3">&nbsp</td>
                            </tr>
                            <%--Pregunta 16--%>

                            <tr>
                                <td colspan="3">
                                    <label id="Label108" style="font-weight: bold;" runat="server" name="">Hardware</label>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3">
                                    <label id="Label71" style="font-weight: bold;" runat="server" name="">16. Un cable HDMI:</label>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label77" style="margin-left: 5px;" runat="server" name="">a. Permite la sintonización de audio y video entre una fuente y un monitor</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta16Resp1SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16A"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta16Resp1NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16A"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label78" style="margin-left: 5px;" runat="server" name="">
                                        b. Permite conectar dos computadoras entre sí
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta16Resp2SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16B"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta16Resp2NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16B"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label79" style="margin-left: 5px;" runat="server" name="">
                                        c. Permite visualizar juegos de consolas en una Smart Tv
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta16Resp3SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16C"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta16Resp3NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16C"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label80" style="margin-left: 5px;" runat="server" name="">d. Permite visualizar juegos desde una consola en una pantalla</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta16Resp4SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16D"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta16Resp4NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16D"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3">&nbsp</td>
                            </tr>
                            <%--Pregunta 17--%>
                            <tr>
                                <td colspan="3">
                                    <label id="Label81" style="font-weight: bold;" runat="server" name="">17. Un multifuncional:</label>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label82" style="margin-left: 5px;" runat="server" name="">
                                        a. Permite el envío de correos electrónicos
                                    </label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta17Resp1SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17A"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta17Resp1NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17A"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label83" style="margin-left: 5px;" runat="server" name="">b. Permite digitalizar documentos</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta17Resp2SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17B"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta17Resp2NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17B"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label84" style="margin-left: 5px;" runat="server" name="">c. Puede imprimir gráficos</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta17Resp3SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17C"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta17Resp3NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17C"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label85" style="margin-left: 5px;" runat="server" name="">d. Permite la creación de documentos de texto</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta17Resp4SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17D"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta17Resp4NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17D"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3">&nbsp</td>
                            </tr>
                            <%--Pregunta 18--%>

                            <tr>
                                <td colspan="3">
                                    <label id="Label86" style="font-weight: bold;" runat="server" name="">18. Una impresora:</label>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label87" style="margin-left: 5px;" runat="server" name="">a. La impresora láser utiliza tóner para imprimir</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta18Resp1SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18A"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta18Resp1NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18A"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label88" style="margin-left: 5px;" runat="server" name="">b. La impresora láser puede imprimir offset</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta18Resp2SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18B"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta18Resp2NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18B"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label89" style="margin-left: 5px;" runat="server" name="">c. La impresora de inyección utiliza cinta</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta18Resp3SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18C"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta18Resp3NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18C"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label90" style="margin-left: 5px;" runat="server" name="">d. La impresora de impacto o matriz utiliza cartuchos de tinta</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta18Resp4SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18D"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta18Resp4NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18D"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3">&nbsp</td>
                            </tr>
                            <%--Pregunta 19--%>

                            <tr>
                                <td colspan="3">
                                    <label id="Label91" style="font-weight: bold;" runat="server" name="">19. Una unidad SD:</label>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label92" style="margin-left: 5px;" runat="server" name="">a. Es un formato de tarjeta de memoria para dispositivos portátiles</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta19Resp1SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19A"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta19Resp1NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19A"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label93" style="margin-left: 5px;" runat="server" name="">b. Puede almacenar documentos, imágenes y videos</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta19Resp2SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19B"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta19Resp2NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19B"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label94" style="margin-left: 5px;" runat="server" name="">c. Los tamaños de SD son: estándar, miniSD y microSD</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta19Resp3SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19C"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta19Resp3NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19C"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label95" style="margin-left: 5px;" runat="server" name="">d. Tiene la opción de aumentar la memoria que tenga de fábrica</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta19Resp4SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19D"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta19Resp4NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19D"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3">&nbsp</td>
                            </tr>
                            <%--Pregunta 20--%>

                            <tr>
                                <td colspan="3">
                                    <label id="Label96" style="font-weight: bold;" runat="server" name="">20. Un puerto USB sirve para:</label>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label97" style="margin-left: 5px;" runat="server" name="">a. Gestionar una base de datos</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta20Resp1SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20A"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta20Resp1NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20A"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label98" style="margin-left: 5px;" runat="server" name="">b. Conectar periféricos (i.e. impresoras, scanners, drivers)</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta20Resp2SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20B"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta20Resp2NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20B"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label99" style="margin-left: 5px;" runat="server" name="">c. Conectar un ratón</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta20Resp3SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20C"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta20Resp3NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20C"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label100" style="margin-left: 5px;" runat="server" name="">d. Conectar una cámara de video</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta20Resp4SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20D"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta20Resp4NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20D"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3">&nbsp</td>
                            </tr>
                            <%--Pregunta 21--%>
                            <tr>
                                <td colspan="3">
                                    <label id="Label101" style="font-weight: bold;" runat="server" name="">21.	Un disco duro:</label>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label102" style="margin-left: 5px;" runat="server" name="">a. Es un CD con mayor capacidad</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta21Resp1SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21A"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta21Resp1NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21A"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label103" style="margin-left: 5px;" runat="server" name="">b. Es una unidad donde se guarda la información</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta21Resp2SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21B"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta21Resp2NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21B"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label105" style="margin-left: 5px;" runat="server" name="">c. Puede almacenar videos</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta21Resp3SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21C"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta21Resp3NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21C"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                            <tr>
                                <td class="subrayado">
                                    <label id="Label109" style="margin-left: 5px;" runat="server" name="">d. Puede ser retirable</label>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta21Resp4SI" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21D"
                                        Text="Sí" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="1">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnPregunta21Resp4NO" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21D"
                                        Text="No" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor" Value="0">
                                    </telerik:RadButton>
                                </td>
                            </tr>



                            <%--End Test--%>
                        </tbody>
                    </table>
                </div>

            </telerik:RadPane>

        </telerik:RadSplitter>
    </div>

    <div style="clear: both; height: 10px;"></div>

    <div class="DivMoveLeft" id="cronometro" runat="server">
        <div class="Cronometro">Tiempo restante <span id="time"></span></div>
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
            <telerik:RadButton ID="btnEliminar" runat="server"  Text="Eliminar" AutoPostBack="true" OnClientClicking="ConfirmarEliminarPrueba" OnClick="btnEliminar_Click"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnEliminarBateria" runat="server" Text="Eliminar batería" AutoPostBack="true" OnClientClicking="ConfirmarEliminarRespuestas" OnClick="btnEliminarBateria_Click"></telerik:RadButton>
         </div>
    </div>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>

