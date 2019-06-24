<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/Pruebas/Prueba.Master" AutoEventWireup="true" CodeBehind="VentanaIngles.aspx.cs" Inherits="SIGE.WebApp.IDP.Pruebas.VentanaIngles" %>

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

        .labelPregunta {
            font-weight: bold !important;
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
                    <telerik:AjaxUpdatedControl ControlID="time" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnTerminar" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="time">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="time" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="tbInglesSecciones">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="tbInglesSecciones" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="mpgIngles" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>


    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">
            var vPruebaEstatus = "";
            var c = "";
            var a = new Array(); //array de preguntas contestadas para validar al final de la prueba
            window.onload = function (sender, args) {
                var multiPage = $find("<%=mpgIngles.ClientID %>");
                if ('<%=this.vTipoRevision%>' != "REV" && '<%=this.vTipoRevision%>' != "EDIT") {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                        if (shouldSubmit) {
                            var segundos = "";
                            segundos = setInitTime(multiPage.get_selectedIndex() + "");
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
                            //window.close();
                            window.location = "Default.aspx?ty=Ini";
                        }
                    });
                    var text = "";
                    text = prueba(multiPage.get_selectedIndex());
                    radconfirm(JustificarTexto(text), callBackFunction, 950, 600, null, "Inglés");
                }
                else {
                    llenar_GruposContestados();
                    prueba(multiPage.get_selectedIndex());
                }
            };

            function close_window(sender, args) {
                if (vPruebaEstatus != "Terminado") {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                        if (shouldSubmit) {
                            if (ValidarContendorPreguntas()) {
                                a = [];
                                clearInterval(c);//Se agrega para detener el tiempo del reloj antes de guardar resultados 12/04/2018
                                var btn = $find("<%=btnTerminar.ClientID%>");
                                btn.click();
                            }
                        }
                    });
                    var text = "¿Estás seguro que deseas terminar tu prueba?";
                    radconfirm(text, callBackFunction, 450, 150, null, "");
                    args.set_cancel(true);
                }
                else {
                    window.close();
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
                    var multiPage = $find("<%=mpgIngles.ClientID %>");
                multiPage.set_selectedIndex(multiPage.get_selectedIndex() + 1);
            }
            function CloseTest() {
                window.location = "Default.aspx?ty=sig";
            }
            function Close() {
                window.top.location.href = window.top.location.href;
                //window.close();
            }
            function updateTimer(seccion) {
                var multiPage = $find("<%=mpgIngles.ClientID %>");
                if ('<%=this.vTipoRevision%>' != "REV" && '<%=this.vTipoRevision%>' != "EDIT") {
                    var position = seccion;
                    clearInterval(c);
                    switch (position) {
                        case "-1":
                            window.location = "Default.aspx?ty=sig";
                            break;
                        case "1":
                            var segundos = '<%=this.vSeccionBtime%>';
                            prueba(seccion);
                            if (segundos <= 0) {
                                mensajePruebaTerminada();
                            }
                            else {
                                var display = document.querySelector('#time');
                                c = Cronometro(segundos, display);
                                multiPage.set_selectedIndex(parseInt(position));
                            }
                            break;
                        case "2":
                            prueba(seccion);
                            var segundos = '<%=this.vSeccionCtime%>';
                            if (segundos <= 0) {
                                mensajePruebaTerminada();
                            }
                            else {
                                var display = document.querySelector('#time');
                                c = Cronometro(segundos, display);
                                multiPage.set_selectedIndex(parseInt(position));
                            }

                            break;
                        case "3":
                            prueba(seccion);
                            var segundos = '<%=this.vSeccionDtime%>';
                            if (segundos <= 0) {
                                mensajePruebaTerminada();
                            }
                            else {
                                prueba();
                                var display = document.querySelector('#time');
                                c = Cronometro(segundos, display);
                                multiPage.set_selectedIndex(parseInt(position));
                            }
                            break;
                        default: break;
                    }
                }
                else {
                    multiPage.set_selectedIndex(parseInt(seccion));
                }
            }

            function setInitTime(segmento) {
                var seconds = "";
                switch (segmento) {
                    case "0": seconds = parseInt('<%=this.vSeccionAtime%>'); break;
                    case "1": seconds = parseInt('<%=this.vSeccionBtime%>'); break;
                    case "2": seconds = parseInt('<%=this.vSeccionCtime%>'); break;
                    case "3": seconds = parseInt('<%=this.vSeccionDtime%>'); break;
                    default: break;
                }
                return seconds;
            }

            function prueba(posicion) {
                var pos = parseInt(posicion);
                var div = document.getElementById('texto');
                switch (pos) {
                    case 0: div.innerHTML = '<p style="margin: 10px;"> <label><b>I Directions</b></label> <br />' +
                  '<label>Read carefully the following questions, choose the correct answer, there is only one correct per item. Each question has a limit time to be answered don´t spend a lot of time in select one.</label>' +
                  '</p>'; break;
                    case 1: div.innerHTML = '<p style="margin: 10px;"><label><b>II Directions</b></label><br />' +
                  '<label>Read carefully the following questions, choose the correct answer, there is only one correct per item. Each question has a limit time to be answered don´t spend a lot of time in select one.</label>' +
                  '</p>'; break;
                    case 2: div.innerHTML = '<p style="margin: 10px;"><label><b>III Directions</b></label><br />' +
                  '<label>Read carefully the following questions, choose the correct answer, there is only one correct per item. Each question has a limit time to be answered don´t spend a lot of time in select one.</label>' +
                  '</p>'; break;
                    case 3: div.innerHTML = '<p style="margin: 10px;"><label><b>IV Directions</b></label><br />' +
                  '<label>Read carefully the following questions, choose the correct answer, there is only one correct per item. Each question has a limit time to be answered don´t spend a lot of time in select one.</label>' +
                  '</p>'; break;
                    default:
                        div.innerHTML = '<p style="margin: 10px;"><label><b>Directions</b></label><br />' +
                  '<label>Read carefully the following questions, choose the correct answer, there is only one correct per item. Each question has a limit time to be answered don´t spend a lot of time in select one.</label>' +
                  '</p>';
                        break;
                }
                return div.innerHTML;
            }


            function addGrupoContestado(valor) {
                if (a.indexOf(valor) == -1 || a.length == 0) {
                    a.push(valor);
                    //console.info(a);
                }
            }

            function valueChanged(sender, args) {
                addGrupoContestado(sender._groupName);
            }

            function ValidarContendorPreguntas(sender, args) {
                var flag = true;
                var vPosSeccion = $find("<%=mpgIngles.ClientID %>").get_selectedIndex();
                var vContenedor = document.getElementsByClassName("Contenedor" + (vPosSeccion + 1));

                var i = 0;
                for (i = 0; i < vContenedor.length; i++) {
                    if (a.indexOf(vContenedor[i].control._groupName) == -1) {
                        var GrupoNoContestado = document.getElementById(vContenedor[i].id);
                        GrupoNoContestado.focus();
                        GrupoNoContestado.style.borderWidth = '1px';
                        var flag = false;
                        scrollTo(vContenedor[i].id);
                        break;
                    }
                }
                if (flag == true) {
                    SetScroll(vPosSeccion);
                }
                return flag;
            }

            function scrollTo(hash) {
                location.hash = "#" + hash;
            }

            function SetScroll(pos) {
                var lblAnclar = null;
                switch (pos + "") {
                    case "0":
                        lblAnclar = document.getElementById("<%= lblA.ClientID %>");
                            scrollTo(lblAnclar.id);
                            break;
                        case "1":
                            lblAnclar = document.getElementById("<%= lblB.ClientID %>");
                            scrollTo(lblAnclar.id);
                            break;
                        case "2":
                            lblAnclar = document.getElementById("<%= lblC.ClientID %>");
                            scrollTo(lblAnclar.id); break;
                        case "3":
                            lblAnclar = document.getElementById("<%= lblD.ClientID %>");
                            scrollTo(lblAnclar.id); break;
                        default: break;
                    }
            }

            function OpenReport() {
                var vURL = "ReporteadorPruebasIDP.aspx";
                var vTitulo = "Impresión Inglés";

                var IdPrueba = '<%= vIdPrueba %>';
                var ClToken = '<%= vClToken %>';

                var windowProperties = {
                    width: document.documentElement.clientWidth - 20,
                    height: document.documentElement.clientHeight - 20
                };

                vURL = vURL + "?IdPrueba=" + IdPrueba + "&ClToken=" + ClToken + "&ClPrueba=INGLES";
                var win = window.open(vURL, '_blank');
                win.focus();
            }

                function llenar_GruposContestados() {
                    a = [];
                    var vPosSeccion = $find("<%=mpgIngles.ClientID %>").get_selectedIndex();
                var vContenedor = document.getElementsByClassName("Contenedor" + (vPosSeccion + 1));
                for (i = 0; i < vContenedor.length; i++) {
                    if (vContenedor[i].control._checked == true) {
                        addGrupoContestado(vContenedor[i].control._groupName);
                    }
                }
                }

            function ConfirmarEliminarRespuestas(sender, args) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        this.click();
                    }
                });
                radconfirm("Este proceso borrará las respuestas de todas las pruebas de la batería ¿Desea continuar?", callBackFunction, 400, 180, null, "Aviso");
                args.set_cancel(true);
            }

            function ConfirmarEliminarPrueba(sender, args) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        this.click();
                    }
                });
                radconfirm("Este proceso borrará las respuestas de la prueba seleccionada ¿Desea continuar?", callBackFunction, 400, 180, null, "Aviso");
                args.set_cancel(true);
            }

        </script>
    </telerik:RadCodeBlock>
   <label class="labelPregunta" style="font-size:21px;">Inglés</label>
    <div style="height: calc(100% - 100px);">
        <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Horizontal">
            <telerik:RadPane ID="rpnAyudaTexto" runat="server" Height="30" Width="100%" Scrolling="None">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="RSPHelp" runat="server" Title="Instrucciones" Width="100%" Height="200">
                        <div id="texto">
                        </div>
                    </telerik:RadSlidingPane>

                </telerik:RadSlidingZone>

            </telerik:RadPane>
            <%----%>
            <telerik:RadPane ID="radPanelPreguntas" runat="server">
                <telerik:RadTabStrip ID="tbInglesSecciones" runat="server" AutoPostBack="true" SelectedIndex="0" MultiPageID="mpgIngles" OnTabClick="tbInglesSecciones_TabClick">
                    <Tabs>
                        <telerik:RadTab Text="1" Visible="false" Width="150"></telerik:RadTab>
                        <telerik:RadTab Text="2" Visible="false" Width="150"></telerik:RadTab>
                        <telerik:RadTab Text="3" Visible="false" Width="150"></telerik:RadTab>
                        <telerik:RadTab Text="4" Visible="false" Width="150"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>

                <div style="height: 5px"></div>
                <telerik:RadMultiPage ID="mpgIngles" runat="server" SelectedIndex="0" Height="100%" AutoPostBack="false">
                    <telerik:RadPageView ID="RPView1" runat="server">

                        <div style="width: 95%; margin-left: 2%; margin-right: 2%;" id="go">
                            <table style="width: 100%;">
                                <thead>
                                    <tr>
                                        <td width="100%"></td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <label class="labelPregunta"  id="lblA" runat="server">1.	My name ___ Lis.</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta1Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                                    Text="a) am" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta1Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                                    Text="b) is" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta1Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                                    Text="c) has" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta1Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                                    Text="d) are" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--Pregunta 2--%>
                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >2. I´m Nash. ___ mother and father are in Canada.</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta2Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                                    Text="a) Her" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta2Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                                    Text="b) His" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta2Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                                    Text="c) My" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta2Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                                    Text="d) Their" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--Pregunta 3--%>

                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >3.  __ are you from?</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta3Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                                    Text="a) How far" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta3Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                                    Text="b) How many" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta3Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                                    Text="c) Where" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta3Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                                    Text="d) What" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
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
                                        <td>
                                            <label class="labelPregunta" >4.  Is there ____ ashtray on the table?</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta4Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                                    Text="a) some" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta4Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                                    Text="b) a" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta4Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                                    Text="c) an" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta4Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                                    Text="d) the" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 5--%>

                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >5.	She ___ got a nice house.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta5Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                                    Text="a) is" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta5Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                                    Text="b) are" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta5Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                                    Text="c) has" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta5Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                                    Text="d) have" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 6--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >6.  __ is your name?</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta6Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                                    Text="a) Who" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta6Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                                    Text="b) When" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta6Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                                    Text="c) What" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta6Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                                    Text="d) How" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 7--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >7.  Jonathan works ____ Melody radio.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta7Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                                    Text="a) from" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta7Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                                    Text="b) for" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta7Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                                    Text="c) to" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta7Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                                    Text="d) of" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 8--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >8.  ____ the evening, my father has a cup of tea.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta8Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                                    Text="a) On" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta8Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                                    Text="b) In" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta8Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                                    Text="c) At" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta8Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                                    Text="d) From" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 9--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >9.  ___ you like an ice cream now?</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta9Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                                    Text="a) Do" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta9Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                                    Text="b) Does" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta9Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                                    Text="c) Are" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta9Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                                    Text="d) Would" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 10--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >10. ___ your brother like jazz?</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta10Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                                    Text="a) Is" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta10Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                                    Text="b) Would" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta10Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                                    Text="c) Does" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta10Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                                    Text="d) Do" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 11--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >11. A: Where ___ you yesterday?
                                                <br />
                                                B: I ___ in the office.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta11Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                                    Text="a) are/am" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta11Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                                    Text="b) are/was" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta11Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                                    Text="c) were/was" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta11Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                                    Text="d) were/am" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 12--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >12.  ___ your friend Archie ___ fish?</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta12Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                                    Text="a) Do/love" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta12Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                                    Text="b) Does/loves" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta12Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                                    Text="c) Do/loves" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta12Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                                    Text="d) Does/love" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 13--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >13.  My friend Francesca ___ her composition at the moment.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta13Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                                    Text="a) writes" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta13Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                                    Text="b) write" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta13Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                                    Text="c) is writing" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta13Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                                    Text="d) wrote" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>


                                    <%--Pregunta 14--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >14. A: ____ the photographers in New York now?
                                                <br />
                                                B: No. They ____there yesterday. </label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta14Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                                    Text="a) are/were" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta14Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                                    Text="b) were/are" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta14Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                                    Text="c) are/are" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta14Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                                    Text="d) were/were" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 15--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >15. In our class everyone ____reading magazines.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta15Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                                    Text="a) likes" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta15Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                                    Text="b) like" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta15Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                                    Text="c) doesn´t like" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta15Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                                    Text=" d) don´t like" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 16--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >16. Some people ___ two magazines every week.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta16Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                                    Text="a) don´t buy" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta16Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                                    Text="b) buy" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta16Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                                    Text="c) buys" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta16Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                                    Text="d) doesn´t buy" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 17--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >17. I ___ an article about the royal family at the moment.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta17Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                                    Text="a) write" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta17Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                                    Text="b) wrote" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta17Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                                    Text="c) am writing" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta17Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                                    Text="d) writes" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 18--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >18. She ___ home at six o´clock yesterday.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta18Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                                    Text="a) leave" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta18Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                                    Text="b) left" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta18Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                                    Text="c) is leaving" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta18Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                                    Text="d) leaves" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 19--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >19. What time ___ you ___ up this morning?</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta19Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                                    Text="a) do/get" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta19Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                                    Text="b) do/got" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta19Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                                    Text="c) did/got" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta19Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                                    Text="d) did/get" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 20--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >20. Chesse is good  for you, but don´t eat too ___</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta20Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                                    Text="a) little" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta20Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                                    Text="b) few" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta20Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                                    Text="c) much" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta20Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                                    Text="d) many" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 21--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >21. London is ___ Athens</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta21Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21"
                                                    Text="a) big than" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta21Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21"
                                                    Text="b) bigger that" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta21Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21"
                                                    Text="c) bigger than" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta21Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21"
                                                    Text="d) the bigger" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 22--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >22. She´s a good student. She´s ___ in the class. </label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta22Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22"
                                                    Text="a) better" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta22Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22"
                                                    Text="b) best" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta22Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22"
                                                    Text="c) the best" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta22Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22"
                                                    Text="d) best than" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 23--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >23. Glasgow is a good place for ___ art and music.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta23Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23"
                                                    Text="a) the" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta23Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23"
                                                    Text="b) a" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta23Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23"
                                                    Text="c) an" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta23Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23"
                                                    Text="d) zero article" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>


                                    <%--Pregunta 24--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >24. We´re getting ___ the train  at the next station. John´s meeting us there. </label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta24Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24"
                                                    Text="a) to" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta24Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24"
                                                    Text="b) of" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta24Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24"
                                                    Text="c) out" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta24Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24"
                                                    Text="d) off" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 25--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >25. We ___ a party for my son next Saturday.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta25Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta25"
                                                    Text="a) are going to have" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta25Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta25"
                                                    Text="b) going to have" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta25Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta25"
                                                    Text="c) are going have" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta25Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta25"
                                                    Text="d) having" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 26--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >26. Ashley ___ a lot of presents last week.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta26Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta26"
                                                    Text="a) have" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta26Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta26"
                                                    Text="b) had" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta26Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta26"
                                                    Text="c) got" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta26Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta26"
                                                    Text="d) get" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 27--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >27. ___ is a big problem here.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta27Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta27"
                                                    Text="a) Employ" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta27Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta27"
                                                    Text="b) Employment" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta27Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta27"
                                                    Text="c) Unemployment" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta27Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta27"
                                                    Text="d) Employed" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 28--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >28. Argentina won the ____</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta28Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta28"
                                                    Text="a) goal" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta28Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta28"
                                                    Text="b) match" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta28Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta28"
                                                    Text="c) players" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta28Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta28"
                                                    Text="d) champions" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 29--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >29. Liverpool didn´t score. We won three ___</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta29Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta29"
                                                    Text="a) draw" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta29Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta29"
                                                    Text="b) team" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta29Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta29"
                                                    Text="c) nil" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta29Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta29"
                                                    Text="d) game" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 30--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >30. A: What would you like to eat?    
                                                <br />
                                                B: ____ thank you. I´m not hungry.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta30Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta30"
                                                    Text="a) Something" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta30Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta30"
                                                    Text="b) No one" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta30Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta30"
                                                    Text="c) Anything" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="APregunta30Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta30"
                                                    Text="d) Nothing" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor1">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--End Test--%>
                                </tbody>
                            </table>
                        </div>
                    </telerik:RadPageView>

                    <telerik:RadPageView ID="RPView2" runat="server">
                        <div style="width: 95%; margin-left: 2%; margin-right: 2%;">
                            <table style="width: 100%;">
                                <thead>
                                    <tr>
                                        <td width="100%"></td>
                                    </tr>
                                </thead>
                                <tbody>

                                    <%--Pregunta 31 --%>

                                    <tr>
                                        <td>
                                            <label class="labelPregunta"  id="lblB" runat="server">31.  ___ is wrong with the car. I can´t start it.</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta1Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                                    Text="a) Nothing" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta1Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                                    Text="b) Anything" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta1Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                                    Text="c) Something" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta1Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                                    Text="d) No one" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 32--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >32.  ___ you ever ___ scuba diving?</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta2Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                                    Text="a) Do/try" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta2Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                                    Text="b) Are/tried" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta2Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                                    Text="c) Did/tried" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta2Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                                    Text="d) Have/tried" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--Pregunta 33--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >33.  A: Will they win the cup this year?
                                                <br />
                                                B: Yes, I think so. They´re a very ___ team.</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta3Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                                    Text="a) easily" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta3Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                                    Text="b) good" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta3Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                                    Text="c) bad" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta3Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                                    Text="d) well" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--34--%>

                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >34.  A: Did you win?
                                                <br />
                                                B: Yes, I always win. He always plays very ___</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta4Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                                    Text="a) good" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta4Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                                    Text="b) bad" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta4Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                                    Text="c) well" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta4Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                                    Text="d) badly" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 35--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >35.  I worried ___ my son.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta5Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                                    Text="a) at" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta5Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                                    Text="b) of" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta5Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                                    Text="c) about" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta5Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                                    Text="d) with" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 36--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >36.  The plane is about to take off. Please ___ your seat belt.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta6Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                                    Text="a) insert" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta6Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                                    Text="b) send" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta6Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                                    Text="c) fasten" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta6Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                                    Text="d) make" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 37--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >37.  I have to ___ a resevation. I´m leaving on vacation soon.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta7Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                                    Text="a) book" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta7Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                                    Text="b) make" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta7Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                                    Text="c) use" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta7Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                                    Text="d) send" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 38--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >38.  I ___ a telephone call from Sonia last night.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta8Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                                    Text="a) have" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta8Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                                    Text="b) get" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta8Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                                    Text="c) got" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta8Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                                    Text="d) had" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 39--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >39.  Get ___ the car. We´re leaving now.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta9Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                                    Text="a) to" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta9Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                                    Text="b) in" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta9Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                                    Text="c) on" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta9Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                                    Text="d) at" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 40--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >40. You can have a shower when we get ____ to the hotel.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta10Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                                    Text="a) up" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta10Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                                    Text="b) in" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta10Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                                    Text="c) back" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta10Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                                    Text="d) on" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 41--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >41. I´ve been living here ___ six months.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta11Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                                    Text="a) in" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta11Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                                    Text="b) ago" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta11Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                                    Text="c) for" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta11Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                                    Text="d) since" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 42--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >42. I haven´t seen Janet ___ two years.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta12Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                                    Text="a) for" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta12Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                                    Text="b) since" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta12Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                                    Text="c) in" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta12Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                                    Text="d) on" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 43--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >43. Have you ___ seen Noel Edmonds on television?</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta13Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                                    Text="a) ever" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta13Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                                    Text="b) yet" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta13Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                                    Text="c) already" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta13Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                                    Text="d) just" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>


                                    <%--Pregunta 44--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >44. I want to watch the news. Has it started ___?</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta14Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                                    Text="a) never" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta14Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                                    Text="b) already" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta14Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                                    Text="c) just" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta14Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                                    Text="d) yet" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 45--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >45. He´s quite keen ___ tennis.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta15Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                                    Text="a) with" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta15Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                                    Text="b) of" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta15Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                                    Text="c) about" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta15Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                                    Text="d) on" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 46--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >46. Meg says Melbourne is much ___ than London.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta16Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                                    Text="a) expensive" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta16Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                                    Text="b) expensiver" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta16Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                                    Text="c) most expensive" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta16Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                                    Text="d) more expensive" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 47--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >47. Have you got ___ biscuits?</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta17Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                                    Text="a) a" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta17Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                                    Text="b) some" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta17Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                                    Text="c) any" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta17Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                                    Text="d) little" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 48--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >48. We haven´t got ___ boxes. I can give you ___ plastic bags.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta18Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                                    Text="a) few/some" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta18Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                                    Text="b) some/any" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta18Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                                    Text="c) some/little" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta18Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                                    Text="d) any/some" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 49--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >49. Have you ___ your homework?</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta19Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                                    Text="a) do" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta19Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                                    Text="b) did" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta19Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                                    Text="c) done" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta19Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                                    Text="d) doing" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 50--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >50. I ___ the cleaning last night. It took hours.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta20Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                                    Text="a) made" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta20Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                                    Text="b) did" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta20Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                                    Text="c) make" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta20Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                                    Text="d) do" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 51--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >51. There´s ___ on television tonight.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta21Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21"
                                                    Text="a) no" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta21Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21"
                                                    Text="b) nothing" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta21Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21"
                                                    Text="c) nobody" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta21Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21"
                                                    Text="d) anything" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 52--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >52. There are ___ good films this week.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta22Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22"
                                                    Text="a) nothing" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta22Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22"
                                                    Text="b) nobody" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta22Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22"
                                                    Text="c) anything" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta22Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22"
                                                    Text="d) no" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 53--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >53. Colin left school ___ he was 16.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta23Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23"
                                                    Text="a) then" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta23Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23"
                                                    Text="b) when" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta23Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23"
                                                    Text="c) until" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta23Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23"
                                                    Text="d) from" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>


                                    <%--Pregunta 54--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >54. Six months ___, he joined the police and went to the Police Training School in Hendon.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta24Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24"
                                                    Text="a) when" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta24Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24"
                                                    Text="b) then" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta24Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24"
                                                    Text="c) later" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta24Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24"
                                                    Text="d) after" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 55--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >55. When you go out, please remember to ___ the door.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta25Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta25"
                                                    Text="a) cancel" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta25Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta25"
                                                    Text="b) lock" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta25Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta25"
                                                    Text="c) turn" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta25Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta25"
                                                    Text="d) leave" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 56--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >56. Last week , two men ___ into the house and stole the CD player.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta26Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta26"
                                                    Text="a) broke" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta26Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta26"
                                                    Text="b) got" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta26Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta26"
                                                    Text="c) came" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta26Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta26"
                                                    Text="d) entered" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 57--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >57. My son´s got dark curly hair and my daugther has ___ .</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta27Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta27"
                                                    Text="a) but" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta27Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta27"
                                                    Text="b) as well" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta27Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta27"
                                                    Text="c) both" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta27Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta27"
                                                    Text="d) neither" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 58--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >58. ___ of us likes opera.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta28Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta28"
                                                    Text="a) Both" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta28Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta28"
                                                    Text="b) Neither" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta28Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta28"
                                                    Text="c) Too" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta28Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta28"
                                                    Text="d) As well" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 59--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >59. She´s a very ___ person. Her house is neat.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta29Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta29"
                                                    Text="a) romantic" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta29Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta29"
                                                    Text="b) tidy" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta29Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta29"
                                                    Text="c) ambitious" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta29Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta29"
                                                    Text="d) shy" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 60--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >60. If you have a holiday in the summer, where ___ you go?</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta30Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta30"
                                                    Text="a) will" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta30Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta30"
                                                    Text="b) do" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta30Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta30"
                                                    Text="c) would" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="BPregunta30Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta30"
                                                    Text="d) did" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor2">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>


                                    <%--End Test--%>
                                </tbody>
                            </table>
                        </div>


                    </telerik:RadPageView>

                    <telerik:RadPageView ID="RPView3" runat="server">
                        <div style="width: 95%; margin-left: 2%; margin-right: 2%;">
                            <table style="width: 100%;">
                                <thead>
                                    <tr>
                                        <td width="100%"></td>
                                    </tr>
                                </thead>
                                <tbody>

                                    <%--Pregunta 61--%>

                                    <tr>
                                        <td>
                                            <label class="labelPregunta"  id="lblC" runat="server">61.  Peter ___ his brother´s car when he ___ the accident.</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta1Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                                    Text="a) drove/had" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta1Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                                    Text="b) was driving/had" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta1Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                                    Text="c) drove/was having" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta1Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                                    Text="d) was driving/was having" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 62--%>
                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >62.  When the accident ___ , my wife and I ___ outside the house.</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta2Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                                    Text="a) happened/ stood" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta2Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                                    Text="b) happened/ were standing" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta2Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                                    Text="c) was happening/were standing" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta2Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                                    Text="d) was happening/ stood" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--Pregunta 63--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >63.  A: How´s Jonathan?
                                                <br />
                                                B: Well, he´s a lot better. He ___ walk without crutches now but he ___ run.</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta3Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                                    Text="a) should/ shouldn´t" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta3Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                                    Text="b) could/ couldn´t" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta3Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                                    Text="c) can/can´t" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta3Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                                    Text="d) has to/had to" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--64--%>

                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >64.  A: Was it a very serious injury?
                                                <br />
                                                B: Yes, it was.  He ___ stay in bed for a month.</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta4Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                                    Text="a) should" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta4Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                                    Text="b) could" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta4Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                                    Text="c) had to" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta4Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                                    Text="d) must" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 65--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >65. Geraldine´s  a hairdresser, ___?</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta5Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                                    Text="a) is she" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta5Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                                    Text="b) isn´t she" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta5Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                                    Text="c) has she" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta5Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                                    Text="d) hasn´t she" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 66--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >66.  Use the ___ to cut your hair.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta6Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                                    Text="a) comb " Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta6Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                                    Text="b) trim" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta6Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                                    Text="c) shampoo" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta6Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                                    Text="d) scissors" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 67--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >67. She owns the  business ___ .</label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta7Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                                    Text="a) her" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta7Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                                    Text="b) by herself" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta7Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                                    Text=" c) herself " Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta7Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                                    Text="d) she" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 68--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >68.  You didn´t want a big place, ___ ?</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta8Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                                    Text="a) did you" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta8Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                                    Text="b) didn´t you" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta8Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                                    Text="c) do you" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta8Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                                    Text="d) don´t you" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 69--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >69. You have to believe in ___ .</label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta9Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                                    Text="a) your                                         " Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta9Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                                    Text="b) you" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta9Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                                    Text="c) yours" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta9Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                                    Text="d) yourself" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 70--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >70. Maria Montse is a Brazilian singer ___ was in London last year.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta10Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                                    Text="a) who" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta10Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                                    Text="b) whom" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta10Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                                    Text="c) that" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta10Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                                    Text="d) which" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 71--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >71. Pele was a  wonderful player ___ everyone respected.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta11Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                                    Text="a) that" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta11Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                                    Text="b) which" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta11Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                                    Text="c) whom" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta11Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                                    Text="d) who" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 72--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >72. I don´t know him, do you know ____?</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta12Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                                    Text="a) who is he" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta12Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                                    Text="b) that he is" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta12Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                                    Text="c) that is he" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta12Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                                    Text="d) who he is" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 73--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >73. A: How big is the taxi company?
                                                <br />
                                                B: She asked him ___ . </label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta13Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                                    Text="a) how big is the taxi company" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta13Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                                    Text="b) how big the taxi company is" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta13Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                                    Text="c) how big the taxi company was" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta13Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                                    Text="d) how big was the taxi company" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>


                                    <%--Pregunta 74--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >74. Proposals for a tunnel ___ by French and British engineers 100 years ago.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta14Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                                    Text="a) made" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta14Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                                    Text="b) are made" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta14Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                                    Text="c) were made" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta14Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                                    Text="d) was made" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 75--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >75. A: ___ you like something to eat? 
                                                <br />
                                                B: Yes, a sandwich please.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta15Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                                    Text="a) Do" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta15Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                                    Text="b) Would" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta15Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                                    Text="c) Will" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta15Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                                    Text="d) Did" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 76--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >76. Charles Dickens ___ a Tale of Two Cities.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta16Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                                    Text="a) was written" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta16Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                                    Text="b) were written" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta16Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                                    Text="c) wrote" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta16Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                                    Text="d) was" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 77--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >77. It´s ___ an old coat that I´m going to throw it away.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta17Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                                    Text="a) too" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta17Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                                    Text="b) so" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta17Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                                    Text="c) such" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta17Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                                    Text="d) as" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 78--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >78. Tom says you ___ him some money.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta18Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                                    Text="a) owe" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta18Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                                    Text="b) pay" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta18Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                                    Text="c) borrow" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta18Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                                    Text="d) run" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 79--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >79. I can´t pay back because I´ve ___ out of money.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta19Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                                    Text="a) lent" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta19Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                                    Text="b) borrowed" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta19Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                                    Text="c) ran" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta19Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                                    Text="d) owed" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 80--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >80. Hello. Where have you ___ ?</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta20Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                                    Text="a) gone" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta20Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                                    Text="b) been" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta20Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                                    Text="c) went" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta20Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                                    Text="d) was" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 81--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >81. Heat a little water, add the sugar and ___ .</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta21Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21"
                                                    Text="a) cover" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta21Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21"
                                                    Text="b) remove" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta21Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21"
                                                    Text="c) stir" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta21Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21"
                                                    Text="d) pour" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 82--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >82. I´m ___ by these songs.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta22Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22"
                                                    Text="a) interested" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta22Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22"
                                                    Text="b) different" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta22Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22"
                                                    Text="c) fascinated" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta22Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22"
                                                    Text="d) liked" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 83--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >83. It´s one of the ___ films I´ve ever seen; I´d strongly recommend it.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta23Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23"
                                                    Text="a) more sensitive" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta23Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23"
                                                    Text="b) as warm" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta23Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23"
                                                    Text="c) worst" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta23Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23"
                                                    Text="d) best" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>


                                    <%--Pregunta 84--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >84. Tea ___ in the south of the country by them.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta24Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24"
                                                    Text="a) grows" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta24Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24"
                                                    Text="b) are grown" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta24Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24"
                                                    Text="c) grow" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta24Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24"
                                                    Text="d) is grown" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 85--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >85. He often __ with the French sister company. He __ in Paris this week.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta25Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta25"
                                                    Text="a) work/worked" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta25Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta25"
                                                    Text="b) is working/works" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta25Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta25"
                                                    Text="c) works/is working" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta25Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta25"
                                                    Text="d) worked/works" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 86--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >86. Tim Cole, aged 5, ___ with a football when he ___ into the pond.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta26Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta26"
                                                    Text="a) played/was falling" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta26Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta26"
                                                    Text="b) was playing/fall" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta26Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta26"
                                                    Text="c) palyed/fall" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta26Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta26"
                                                    Text="d) was playing/fell" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 87--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >87. Our students are usually successful but if you ___ study, you  won´t pass your  examinations.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta27Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta27"
                                                    Text="a) will" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta27Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta27"
                                                    Text="b) won´t" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta27Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta27"
                                                    Text="c) do" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta27Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta27"
                                                    Text="d) don´t" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 88--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >88. Use the cream daily and your skin ___ be. Smoother and softer.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta28Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta28"
                                                    Text="a) won´t" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta28Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta28"
                                                    Text="b) can" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta28Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta28"
                                                    Text="c) will" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta28Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta28"
                                                    Text="d) would" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 89--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >89. We exchange goods bought here but remember we ___ give you your money back unless you have a receipt.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta29Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta29"
                                                    Text="a) will" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta29Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta29"
                                                    Text="b) can´t" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta29Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta29"
                                                    Text="c) don´t have to" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta29Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta29"
                                                    Text="d) can" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 90--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >90. I ___ someone who ___ back from South Africa.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta30Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta30"
                                                    Text="a) met/came" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta30Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta30"
                                                    Text="b) had just met/has just come" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta30Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta30"
                                                    Text="c) had just met/came" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="CPregunta30Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta30"
                                                    Text="d) met/had just come" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor3">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>


                                    <%--End Test--%>
                                </tbody>
                            </table>
                        </div>
                    </telerik:RadPageView>

                    <telerik:RadPageView ID="RPView4" runat="server">
                        <div style="width: 95%; margin-left: 2%; margin-right: 2%;">
                            <table style="width: 100%;">
                                <thead>
                                    <tr>
                                        <td width="100%"></td>
                                    </tr>
                                </thead>
                                <tbody>

                                    <%--Pregunta 91--%>

                                    <tr>
                                        <td>
                                            <label class="labelPregunta"  id="lblD" runat="server">91.  I ___ a three-month Spanish course but I ___ the language very well.</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta1Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                                    Text="a) had already done/didn´t speak" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta1Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                                    Text="b) had already done/hadn´t spoken" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta1Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                                    Text="c) did/hadn´t already spoken" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta1Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                                    Text="d) did/didn´t speak" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 92--%>
                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >92. There´s only ____ excitement in the city.</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta2Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                                    Text="a) a few" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta2Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                                    Text="b) many" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta2Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                                    Text="c) much" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta2Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta2"
                                                    Text="d) a little" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--Pregunta 93--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >93. I´ve never ___ across the Atlantic before.</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta3Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                                    Text="a) fly" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta3Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                                    Text="b) flying" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta3Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                                    Text="c) flown" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta3Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta3"
                                                    Text="d) flew" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--94--%>

                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >94. New housing plans ___.</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta4Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                                    Text="a) have agreed" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta4Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                                    Text="b) have been agreed" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta4Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                                    Text="c) have agree" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta4Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta4"
                                                    Text="d) agreed" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 95--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >95. The computer company is opening a new office. They ___ about fifty more staff.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta5Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                                    Text="a) should" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta5Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                                    Text="b) need" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta5Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                                    Text="c) ought" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta5Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta5"
                                                    Text="d) must" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 96--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >96. I think you ___ to wear your new suit with a white shirt.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta6Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                                    Text="a) must" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta6Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                                    Text="b) ought" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta6Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                                    Text="c) should" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta6Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta6"
                                                    Text="d) could" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 97--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >97. When I saw the film I was ___ .</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta7Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                                    Text="a) frighten" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta7Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                                    Text="b) frightened" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta7Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                                    Text="c) frightening" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta7Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta7"
                                                    Text="d) frightens" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 98--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >98.  The Olympic Games ought not to be ___ in the same city twice.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta8Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                                    Text="a) held" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta8Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                                    Text="b) shown" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta8Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                                    Text="c) taken" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta8Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta8"
                                                    Text="d) sent" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 99--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >99. I think she´s going to ___ up. She should see a psychotherapist.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta9Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                                    Text="a) break" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta9Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                                    Text="b) grow" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta9Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                                    Text="c) crack" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta9Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta9"
                                                    Text="d) give" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 100--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >100. We want to focus people´s attention ___ these problems.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta10Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                                    Text="a) on" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta10Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                                    Text="b) in" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta10Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                                    Text="c) from" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta10Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta10"
                                                    Text="d) by" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 101--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >101. If you ___ Prince Charles, what would you say to him?</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta11Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                                    Text="a) met" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta11Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                                    Text="b) meet" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta11Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                                    Text="c) will meet" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta11Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta11"
                                                    Text="d) had met" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 102--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >102. A: Did you make that dress yourself?
                                                <br />
                                                B: NO. I ___ by a tailor.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta12Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                                    Text="a) did it" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta12Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                                    Text="b) had it done" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta12Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                                    Text="c) have it done" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta12Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta12"
                                                    Text="d) done it" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 103--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >103. I like England but I can´t get used to ___ on the left.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta13Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                                    Text="a)drive" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta13Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                                    Text="b) drove" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta13Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                                    Text="c) driving" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta13Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta13"
                                                    Text="d) drives" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>


                                    <%--Pregunta 104--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >104. When I was 18 my parents wouldn´t ___ me stay out late.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta14Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                                    Text="a) let" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta14Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                                    Text="b) allow" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta14Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                                    Text="c) have" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta14Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta14"
                                                    Text="d) make" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 105--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >105. Gullit can communicate well. He´s very ___ .</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta15Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                                    Text="a) humor" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta15Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                                    Text="b) articulate" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta15Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                                    Text="c) outspoken" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta15Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta15"
                                                    Text="d) casual" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 106--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >106. He doesn´t want people round him all the time. Sometimes he´s quite a ___ person. </label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta16Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                                    Text="a) cosmopolitan" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta16Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                                    Text="b) moved" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta16Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                                    Text="c) relaxed" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta16Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta16"
                                                    Text="d) private" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 107--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >107. I didn´t enjoy the film. ____ </label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta17Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                                    Text="a) So did I" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta17Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                                    Text="b) Neither did I" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta17Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                                    Text="c) So I did" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta17Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta17"
                                                    Text="d) Neither I did" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 108--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >108. You are not ___ to bring this into the country.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta18Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                                    Text="a) can" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta18Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                                    Text="b) permission" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta18Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                                    Text="c) allowed" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta18Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta18"
                                                    Text="d) let" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 109--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >109. If the student ___ a better essay last term. I ___ him a higher mark.</labe></td>
                                    </tr> 

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta19Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                                    Text="a) writes/wiil give" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta19Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                                    Text="b) wrote/would give" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta19Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                                    Text="c) had written/would have given" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta19Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta19"
                                                    Text="d)writes/give" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 110--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >110. Then man ran onto the ice. He ___ careful.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta20Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                                    Text="a)should be" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta20Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                                    Text="b)should been" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta20Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                                    Text="c) should have been" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta20Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta20"
                                                    Text="d) shouldn´t be" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 111--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >111. Stevie Wonder ___ pop songs since the 1960s.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta21Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21"
                                                    Text="a) has sung" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta21Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21"
                                                    Text="b) has been sung" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta21Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21"
                                                    Text="c) has been singing" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta21Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta21"
                                                    Text="d) has singing" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 112--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta">112. I like my bank manager. She´s very ____ to people in business.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta22Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22"
                                                    Text="a) ambitious" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta22Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22"
                                                    Text="b) sympathetic" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta22Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22"
                                                    Text="c) tough" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta22Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta22"
                                                    Text="d) careful" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 113--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >113. They ___ him with murdering his wife.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta23Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23"
                                                    Text="a) arrested" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta23Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23"
                                                    Text="b) alleged" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta23Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23"
                                                    Text="c) sentenced" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta23Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta23"
                                                    Text="d) charged" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>


                                    <%--Pregunta 114--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >114. I´m not enjoying the work but I´ll just have to ___ it.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta24Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24"
                                                    Text="a) get over" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta24Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24"
                                                    Text="b) get back" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta24Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24"
                                                    Text="c) get used to" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta24Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta24"
                                                    Text="d) get on well with" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 115--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >115. He´s seriously ill. I hope he ___ .</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta25Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta25"
                                                    Text="a) pulls through" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta25Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta25"
                                                    Text="b) pulls together" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta25Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta25"
                                                    Text="c) pulls out" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta25Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta25"
                                                    Text="d) pulls up" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 116--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >116. I thought you and  Janet had ___ up.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta26Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta26"
                                                    Text="a) picked" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta26Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta26"
                                                    Text="b) broken" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta26Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta26"
                                                    Text="c) taken" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta26Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta26"
                                                    Text="d) pulled" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 117--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >117. I have got used to working with computers now. There are so many things we ___ do more quickly and efficiently now than before.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta27Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta27"
                                                    Text="a) might" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta27Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta27"
                                                    Text="b) could" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta27Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta27"
                                                    Text="c) would" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta27Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta27"
                                                    Text="d) can" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 118--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >118. My sister would like ___ dinner for us tomorrow.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta28Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta28"
                                                    Text="a) to cook" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta28Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta28"
                                                    Text="b) cook" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta28Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta28"
                                                    Text="c) cooks" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta28Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta28"
                                                    Text="d) cooking" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 119--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >119. He looks after the children and she is the ___ .</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta29Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta29"
                                                    Text="a) affluent" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta29Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta29"
                                                    Text="b) accentric" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta29Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta29"
                                                    Text="c) breadwinner" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta29Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta29"
                                                    Text="d) mortgage" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>

                                    <%--Pregunta 30--%>


                                    <tr>
                                        <td>
                                            <label class="labelPregunta" >120. I will take the new job ___ . I can choose who works with me.</label></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta30Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta30"
                                                    Text="a) unless" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta30Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta30"
                                                    Text="b) depends" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta30Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta30"
                                                    Text="c) provided that" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>

                                            <div class="ctrlBasico">
                                                <telerik:RadButton ID="DPregunta30Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta30"
                                                    Text="d) only" Skin="Metro" OnClientClicking="valueChanged" CssClass="Contenedor4">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </div>
                                        </td>
                                    </tr>


                                    <%--End Test--%>
                                </tbody>
                            </table>
                        </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>

    <div style="clear: both; height: 10px;"></div>

    <div class="DivMoveLeft" id="cronometro" runat="server">
        <div class="Cronometro">Tiempo restante <span id="time">15:00</span></div>
    </div>

    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnTerminar" runat="server" OnClientClicking="close_window" OnClick="btnTerminar_Click" Text="Siguiente" AutoPostBack="true"></telerik:RadButton>
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
