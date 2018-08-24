<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaAptitudMentalI.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaAptitudMental1" %>
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
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

         <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
                 <telerik:AjaxSetting AjaxControlID="btnTerminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje"   LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>


     <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">
            vPruebaEstatus = "";

            window.onload = function (sender, args) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        var segundos = '<%=this.vTiempoPrueba%>';
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
                var text = "SERIE I<br>Instrucciones: Selecciona la opción correspondiente a la palabra que complete correctamente la oración  <br>Ejemplo: <br>El iniciador de nuestra guerra de independencia fue: <br>( ) Morelos ( ) Zaragoza ( ) Iturbide (•) Hidalgo";
                radconfirm(text, callBackFunction, 400, 300, null, "Ayuda General");
                 };

                function close_window(sender, args) {
                    if (vPruebaEstatus != "Terminado") {
                        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                            if (shouldSubmit) {
                                var btn = $find("<%=btnTerminar.ClientID%>");
                                btn.click();
                        }
                    });

                    var text = "¿Estas seguro que deseas terminar tu prueba?";
                    radconfirm(text, callBackFunction, 400, 160, null, "");
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
                var oWnd = radalert("Usted ha terminado su prueba exitosamente o el tiempo de aplicación de la prueba ha concluido. <br> Cuando esté listo para pasar a la siguiente prueba, por favor haga clic en el botón 'Siguiente' más abajo <br>Recuerde que no es posible volver a ingresar la prueba previa; si intenta hacerlo por medio del botón del navegador, la aplicación no te lo permitirá: se generará un error y el intento quedará registrado", 400, 300, "");
                oWnd.add_close(WinClose);
            }

            function CloseTest() {
                window.close();
            }

            function prueba()
            {
                var multiPage = $find("<%=mpgActitudMentalI.ClientID %>");
                var i;
                var position = multiPage.get_selectedIndex();
                i = position + 1;
            
                    var div = document.getElementById('texto');
                    switch (i) {
                        case 1:
                            console.info(div);
                            div.innerHTML = '<p style="margin: 10px;">SERIE II<br />' +
                            'Instrucciones: Lee cada cuestión y anota la letra correspondiente a la mejor respuesta tal como lo muestra el ejemplo. <br />'+
                            '<b>Ejemplo:</b><br />'+
                            '¿Por qué compramos relojes? Porque:<br />'+
                            '( ) Nos gusta oirlos sonar<br />' +
                            '( ) Tiene manecillas<br />' +
                            '(*) Nos indican las horas'+
                            '</p>';
                            break;
                        case 2:
                            div.innerHTML = ' <p style="margin: 10px;"><b>SERIE III</b><br />' +
                            'Instrucciones: Cuando dos palabras signifiquen lo mismo, selecciona la opción &quot;I&quot; de igual. Cuando signifiquen lo opuesto selecciona la letra &quot;O&quot;' +
                            '<br />' +
                            '<b>Ejemplo:</b><br />' +
                            'Tirar-Arrojar <br />' +
                            '(*) I ( ) O <br />' +
                            'Norte-Sur <br />' +
                            '( ) I (*) O '+
                            '</p>';
                            break;
                        case 3:
                            div.innerHTML = ' <p style="margin: 10px;"> <b>SERIE IV</b><br />' +
                            'Instrucciones: Anote en la hoja de respuestas las letras correspondientes a las DOS palabras que indican algo que SIEMPRE tiene el sujeto.'+
                            'ANOTA SOLAMENTE DOS para cada renglón<br />'+
                            '<br />'+
                            '<b>Ejemplo:</b><br />'+
                            'Un hombre tiene siempre:<br />'+
                            '[*] Cuerpo [ ] Gorra [ ] Guantes [&#149;] Boca [ ] Dinero'+
                            '</p>';
                            break;
                        case 4:
                            div.innerHTML = '<p style="margin: 10px;"> <b>SERIE V</b><br />' +
                            'Instrucciones: Encuentra las respuestas lo más pronto posible y escríbelas en el espacio para captura. Puedes utilizar una hoja en blanco para hacer las operaciones.</p>';
                            break;
                        case 5:
                            div.innerHTML =
                            ' <p style="margin: 10px;"><b>SERIE VI</b><br />' +
                            'Instrucciones: Señala &quot;Si&quot; o &quot;No&quot; para cada caso.<br />'+
                            '<br />'+
                            '<b>Ejemplo:</b><br />'+
                            'Si - No'+
                            'Se hace el carbón de madera? <br />'+
                             '(*)( )  <br />'+
                            '¿Tienen todos los hombres 1.70 de altura?  <br />'+
                            '( )(*)  </p>';
                            break;
                        case 6:
                            div.innerHTML =
                            '    <p style="margin: 10px;"><b>SERIE VII</b><br />' +
                            'Instrucciones: Selecciona la letra correspondiente a la palabra que complete correctamente la oración'+
                            '<br />'+
                            '<br /><b>Ejemplo:</b><br />'+
                            'El OIDO es a OIR como el OJO es a:<br />'+
                            '( ) Mesa (*) Ver ( ) Mano ( ) Jugar<br />'+
                            'El SOMBRERO es a la CABEZA como el ZAPATO es a:<br />'+
                            '( ) Brazo ( ) Abrigo (*) Pie ( ) Pierna  </p>';
                            break;
                        case 7:
                            div.innerHTML =
                            '<p style="margin: 10px;">' +
                            '<b>SERIE VIII</b><br />'+
                            'Instrucciones: Las palabras de cada una de las oraciones siguientes están mezcladas.Ordena cada una de las oraciones. Si el significado de la oración es VERDADERO selecciona la opcion &quot;V&quot;; si el significado de la oración es FALSO, selecciona la opción &quot;F&quot;. <br />'+
                            '<br />'+
                            '<b>Ejemplo:</b><br />'+
                            'V  <br/>' +
                            'F'+
                            'oir son los para oídos <br />'+
                            '(*)'+
                            '( )  <br/>' +
                            'comer para pólvora la buena es <br/>' +
                            '( ) '+
                            '(*)  </p>';
                            break;
                        case 8:
                            div.innerHTML =
                                             ' <p style="margin: 10px;"><b>SERIE IX</b><br />' +
                            'Instrucciones: Selecciona la opción con la palabra que no corresponde con las demás del renglón.<br />'+
                            '<br /><b>Ejemplo:</b><br />'+
                            '( ) Bala ( ) Cañón ( ) Pistola ( ) Espada (*) Lápiz<br />'+
                            '( ) Canadá (*) Sonora ( ) China ( ) India ( ) Francia<br />';
                            break;
                        case 9:
                            div.innerHTML =
                            '<b>SERIE X</b><br />'+
                            'Instrucciones: En cada renglón procura encontrar cómo están hechas las series; después, escribe en los espacios en blanco los DOS números que deban seguir en cada serie<br />'
                            '<br /><b>Ejemplo:</b><br />' +
                             '5 10 15 20  <br />' +
                             '25  <br />' +
                             '30  <br />' +
                              '20 18 16 14 12 <br />' +
                             '10  <br />' +
                             '8  </p>';

                            var btn = $find("<%=btnTerminar.ClientID %>");
                            btn.set_text("Guardar");
                            break;
                        default:
                            break;
                    }
                    multiPage.set_selectedIndex(i);
            }

        </script>
    </telerik:RadCodeBlock>


     <h4>Aptitud mental I</h4>

    <div style="height: calc(100% - 100px);">

        <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Horizontal">

            <telerik:RadPane ID="rpnAyudaTexto" runat="server" Height="30" Width="100%" Scrolling="None">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="RSPHelp" runat="server" Title="Ayuda" Width="100%" Height="200">
                         <div id="texto">
                        <p style="margin: 10px;">
                            SERIE I  <br />Instrucciones: Selecciona la opción correspondiente a la palabra que complete correctamente la oración<br /> Ejemplo:<br />El iniciador de nuestra guerra de independencia fue:<br />( ) Morelos ( ) Zaragoza ( ) Iturbide (•) Hidalgo																																			
                        </p>
                             </div>
                    </telerik:RadSlidingPane>

                </telerik:RadSlidingZone>

            </telerik:RadPane>

            <telerik:RadPane ID="radPanelPreguntas" runat="server"  >

            <telerik:RadTabStrip ID="tbActitudMentalI" runat="server" SelectedIndex="0" MultiPageID="mpgActitudMentalI">
                <Tabs>
                    <telerik:RadTab Text="1" Visible="false"></telerik:RadTab>
                    <telerik:RadTab Text="2" Visible="false"></telerik:RadTab>
                    <telerik:RadTab Text="3" Visible="false"></telerik:RadTab>
                    <telerik:RadTab Text="4" Visible="false"></telerik:RadTab>
                    <telerik:RadTab Text="5" Visible="false"></telerik:RadTab>
                    <telerik:RadTab Text="6" Visible="false"></telerik:RadTab>
                    <telerik:RadTab Text="7" Visible="false"></telerik:RadTab>
                    <telerik:RadTab Text="8" Visible="false"></telerik:RadTab>
                    <telerik:RadTab Text="9" Visible="false"></telerik:RadTab>
                    <telerik:RadTab Text="10" Visible="false"></telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>

            <div style="height: 20px"></div>
            <telerik:RadMultiPage ID="mpgActitudMentalI" runat="server" SelectedIndex="0" Height="100%">
                <telerik:RadPageView ID="RPView1" runat="server">
                        <div style="width:95%; margin-left:2%; margin-right:2%;">
                <table style="width: 100%;" >
                    <thead>
                         <tr>
                                 <td width="100%"></td>
                            
                         </tr>
                    </thead>
                     <tbody>
                          <tr > 
                              <td colspan="4"><label>1. La gasolina se extrae de:</label></td>
                               
                          </tr>
                          <tr> 
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta1Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta1"
                                        Text="Granos" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta1Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta1"
                                        Text="Petroléo" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta1Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta1"
                                        Text="Trementina" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta1Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta1"
                                        Text="Semillas" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--Pregunta 2--%>
                            
                           <tr > <td colspan="4"><label>2. Una tonelada tiene:</label></td></tr>
                          <tr> 
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta2Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta2"
                                        Text="1,000 kilogramos" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta2Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta2"
                                        Text="2,000 kilogramos" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta2Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta2"
                                        Text="3,000 kilogramos" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta2Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta2"
                                        Text="4,000 kilogramos" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--Pregunta 3--%>

                           <tr > <td colspan="4"><label>3. La mayoria de nuestras exportaciones salen de:</label></td></tr>
                          <tr> 
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta3Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta3"
                                        Text="Mazatlán" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta3Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta3"
                                        Text="Acapulco" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta3Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta3"
                                        Text="Progreso" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta3Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta3"
                                        Text="Veracruz" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--Pregunta 4--%>

                           <tr > <td colspan="4"><label>4. El nervio óptico sirve para:</label></td></tr>
                          <tr> 
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta4Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta4"
                                        Text="Ver" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta4Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta4"
                                        Text="Oír" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta4Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta4"
                                        Text="Probar" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta4Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta4"
                                        Text="Sentir" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--Pregunta 5--%>

                         
                           <tr> <td colspan="4"><label>5. El café es una especie de:</label></td></tr>
                          <tr> 
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta5Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta5"
                                        Text="Corteza" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta5Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta5"
                                        Text="Fruto" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta5Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta5"
                                        Text="Hojas" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta5Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta5"
                                        Text="Raíz" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--Pregunta 6--%>

                         
                           <tr> <td colspan="4"><label>6. El jamón es carne de:</label></td></tr>
                          <tr> 
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta6Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta6"
                                        Text="Carnero" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta6Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta6"
                                        Text="Vaca" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta6Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta6"
                                        Text="Gallina" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta6Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta6"
                                        Text="Cerdo" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--Pregunta 7--%>

                         
                           <tr> <td colspan="4"><label>7. La laringe esta en:</label></td></tr>
                          <tr> 
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta7Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta7"
                                        Text="Abdomen" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta7Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta7"
                                        Text="Cabeza" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta7Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta7"
                                        Text="Garganta" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta7Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta7"
                                        Text="Espalda" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--Pregunta 8--%>

                         
                           <tr> <td colspan="4"><label>8. La guillotina causa:</label></td></tr>
                          <tr> 
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta8Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta8"
                                        Text="Muerte" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta8Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta8"
                                        Text="Enfermedad" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta8Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta8"
                                        Text="Fiebre" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta8Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta8"
                                        Text="Malestar" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--Pregunta 9--%>

                         
                           <tr> <td colspan="4"><label>9. La grúa se usa para:</label></td></tr>
                          <tr> 
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta9Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta9"
                                        Text="Perforar" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta9Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta9"
                                        Text="Cortar" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta9Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta9"
                                        Text="Levantar" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta9Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta9"
                                        Text="Exprimir" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--Pregunta 10--%>

                         
                           <tr> <td colspan="4"><label>10. Una figura de seis lados se llama:</label></td></tr>
                          <tr> 
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta10Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta10"
                                        Text="Pentagono" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta10Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta10"
                                        Text="Paralelogramo" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta10Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta10"
                                        Text="Hexágono" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta10Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta10"
                                        Text="Trapecio" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--Pregunta 11--%>

                         
                           <tr> <td colspan="4"><label>11. El kilowatt mide:</label></td></tr>
                          <tr> 
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta11Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta11"
                                        Text="Lluvia" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta11Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta11"
                                        Text="Viento" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta11Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta11"
                                        Text="Electricidad" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta11Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta11"
                                        Text="Presión" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--Pregunta 12--%>

                         
                           <tr> <td colspan="4"><label>12. La pauta se usa en:</label></td></tr>
                          <tr> 
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta12Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta12"
                                        Text="Agrícultura" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta12Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta12"
                                        Text="Música" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta12Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta12"
                                        Text="Fotografía" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta12Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta12"
                                        Text="Estenografía" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--Pregunta 13--%>

                         
                           <tr > <td colspan="4"><label>13. Las esmeraldas son:</label></td></tr>
                          <tr> 
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta13Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta13"
                                        Text="Azules" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta13Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta13"
                                        Text="Verdes" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta13Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta13"
                                        Text="Rojas" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta13Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta13"
                                        Text="Amarillas" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--Pregunta 14--%>
                         
                           <tr> <td colspan="4"><label>14. El metro es aproxímadamente igual a:</label></td></tr>
                          <tr> 
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta14Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta14"
                                        Text="Pie" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta14Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta14"
                                        Text="Pulgada" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta14Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta14"
                                        Text="Yarda" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta14Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta14"
                                        Text="Milla" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--Pregunta 15--%>
                         
                           <tr> <td colspan="4"><label>15. La esponjas se obtienen de:</label></td></tr>
                          <tr> 
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta15Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta15"
                                        Text="Animales" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta15Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta15"
                                        Text="Yerbas" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta15Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta15"
                                        Text="Bosques" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta15Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta15"
                                        Text="Minas" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--Pregunta 16--%>
                         
                           <tr> <td colspan="4"><label>16. Fraude es un término usado en:</label></td></tr>
                          <tr> 
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta16Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta16"
                                        Text="Medicina" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta16Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta16"
                                        Text="Teología" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta16Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta16"
                                        Text="Leyes" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="APregunta16Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnAPregunta16"
                                        Text="Pedagogía" Skin="Metro">
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
                     </div>
                    </telerik:RadPageView>

                <telerik:RadPageView ID="RPView2" runat="server">
                                     <div style="width:95%; margin-left:2%; margin-right:2%;">
                <table style="width: 100%;" >
                    <thead>
                         <tr>
                                 <td width="100%"></td>
                          
                         </tr>
                    </thead>
                     <tbody>
                          <tr > 
                              <td colspan="4"><label>1. Si la tierra estuviera más cerca del sol:</label></td>
                               
                          </tr>
                          <tr> 
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta1Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta1"
                                        Text="Las estrellas desaparecerían" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta1Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta1"
                                        Text="Los meses serían más largos" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta1Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta1"
                                        Text="La tierra estaría más caliente" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>

                          </tr>

                         <%--Pregunta 2--%>
                            
                           <tr > <td colspan="4"><label>2. Los rayos de una rueda están frecuentemente hechos de nogal porque:</label></td></tr>
                          <tr> 
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta2Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta2"
                                        Text="El nogal es fuerte" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta2Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta2"
                                        Text="Se corta fácilmente" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta2Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta2"
                                        Text="Coge bien la pintura" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                              </td>
                          </tr>

                         <%--Pregunta 3--%>

                           <tr > <td colspan="4"><label>3.  Un tren se detiene con más dificultad que un automóvil porque:</label></td></tr>
                          <tr> 
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta3Resp" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta3"
                                        Text="Tiene más ruedas" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta3Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta3"
                                        Text="Es más pesado" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta3Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta3"
                                        Text="Sus frenos no son buenos" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>

                          </tr>

                         <%--Pregunta 4--%>

                           <tr > <td colspan="4"><label>4. El dicho &quot;A golpecitos se derriba un roble&quot; quiere decir:</label></td></tr>
                          <tr> 
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta4Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta4"
                                        Text="Que los robles son más débiles" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta4Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta4"
                                        Text="Que son mejores los golpes pequeños" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta4Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta4"
                                        Text="Que el esfuerzo constante logra resultados sorprendentes" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>

                          </tr>

                         <%--Pregunta 5--%>

                         
                           <tr> <td colspan="4"><label>5. El dicho &quot;Una olla vigilada nunca hierve&quot; quiere decir:</label></td></tr>
                          <tr> 
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta5Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta5"
                                        Text="Que no debemos vigilarla cuando está en el fuego" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta5Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta5"
                                        Text="Que tarda en hervir" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta5Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta5"
                                        Text="Que el tiempo se alarga cuando esperamos algo" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>

                          </tr>

                         <%--Pregunta 6--%>

                         
                           <tr> <td colspan="4"><label>6. El dicho &quot;Siembra pasto mientras haya sol&quot; quiere decir:</label></td></tr>
                          <tr> 
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta6Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta6"
                                        Text="Que el pasto se siembra en verano" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta6Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta6"
                                        Text="Que debemos aprovechar nuestras oportunidades" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta6Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta6"
                                        Text="Que el pasto no debe cortarse en la noche" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>

                          </tr>

                         <%--Pregunta 7--%>

                         
                           <tr> <td colspan="4"><label>7. El dicho &quot;Zapatero a tus zapatos&quot; quiere decir:</label></td></tr>
                          <tr> 
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta7Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta7"
                                        Text="Que un zapatero no debe abandonar sus zapatos" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta7Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta7"
                                        Text="Que los zapateros no deben estar ociosos" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta7Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta7"
                                        Text="Que debemos trabajar en lo que podemos hacer mejor" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>

                          </tr>

                         <%--Pregunta 8--%>

                         
                           <tr> <td colspan="4"><label>8. El dicho &quot;La cuña para que apriete tiene que ser del mismo palo&quot; quiere decir:</label></td></tr>
                          <tr> 
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta8Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta8"
                                        Text="Que el palo sirve para apretar" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta8Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta8"
                                        Text="Que las cuñas siempre son de madera" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta8Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta8"
                                        Text="Que exigen más las personas que nos conocen" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>

                          </tr>

                         <%--Pregunta 9--%>

                         
                           <tr> <td colspan="4"><label>9. Un acorazado de acero flota porque:</label></td></tr>
                          <tr> 
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta9Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta9"
                                        Text="La máquina lo hace flotar" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta9Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta9"
                                        Text="Porque tiene grandes espacios huecos" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta9Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta9"
                                        Text="Contiene algo de madera" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>

                          </tr>

                         <%--Pregunta 10--%>

                         
                           <tr> <td colspan="4"><label>10. Las plumas de las alas ayudan al pájaro a volar porque:</label></td></tr>
                          <tr> 
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta10Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta10"
                                        Text="Las alas ofrecen una amplia superficie ligera" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta10Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta10"
                                        Text="Mantienen el aire fuera del cuerpo" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta10Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta10"
                                        Text="Disminuye su peso" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>

                          </tr>

                         <%--Pregunta 11--%>

                         
                           <tr> <td colspan="4"><label>11. El dicho &quot;Una golondrina no hace verano&quot quiere decir:</label></td></tr>
                          <tr> 
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta11Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta11"
                                        Text="Que las golondrinas regresan" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta11Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta11"
                                        Text="Que un simple dato no es suficiente" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="BPregunta11Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnBPregunta11"
                                        Text="Que los pájaros se agregan a nuestros placeres del verano" Skin="Metro">
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
                     </div>
       

                </telerik:RadPageView>
                
                <telerik:RadPageView ID="RPView3" runat="server">
                            <div style="width:95%; margin-left:2%; margin-right:2%;">
                <table style="width: 100%;" >
                    <thead>
                         <tr>
                                 <td width="100%"></td>
                         </tr>
                    </thead>
                     <tbody>
                          <tr > 
                              <td >
                                  <div class="ctrlBasico"><label style="width:180px;">1. Salado - dulce</label></div>
                                         <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta1Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta1"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta1Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta1"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          
                          </tr>

                         <%--Pregunta 2--%>
                            
                         <tr > 
                              <td >
                                   <div class="ctrlBasico">
                                  <label style="width:180px;">2. Alegrarse - regocijarse</label>
                                       </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta2Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta2"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta2Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta2"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--Pregunta 3--%>

                         <tr > 
                              <td >
                                   <div class="ctrlBasico">
                                  <label style="width:180px;">3. Mayor - menor</label>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta3Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta3"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta3Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta3"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--Pregunta 4--%>

                         <tr > 
                              <td >
                                   <div class="ctrlBasico">
                                  <label style="width:180px;">4. Sentarse - pararse</label>
                                </div>

                                  <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta4Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta4"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta4Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta4"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                       </td>
                          </tr>

                         <%--Pregunta 5--%>

                         <tr > 
                              <td >
                                   <div class="ctrlBasico">
                                  <label style="width:180px;">5. Desperdiciar - aprovechar</label>
                                </div>

                                  <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta5Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta5"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta5Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta5"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                       </td>
                          </tr>

                         <%--Pregunta 6--%>

                         <tr > 
                              <td >
                                   <div class="ctrlBasico">
                                  <label style="width:180px;">6. Conceder - negar</label>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta6Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta6"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>


                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta6Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta6"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                       </td>
                          </tr>

                         <%--Pregunta 7--%>

                         <tr > 
                              <td >
                                   <div class="ctrlBasico">
                                  <label style="width:180px;">7. Tónico - estimulante</label>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta7Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta7"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta7Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta7"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                       </td>
                          </tr>

                         <%--Pregunta 8--%>

                         <tr > 
                              <td >
                                   <div class="ctrlBasico">
                                  <label style="width:180px;">8. Rebajar - denigrar</label>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta8Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta8"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta8Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta8"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                       </td>
                          </tr>

                         <%--Pregunta 9--%>

                         <tr > 
                              <td >
                                   <div class="ctrlBasico">
                                  <label style="width:180px;">9. Prohibir - permitir</label>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta9Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta9"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta9Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta9"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                       </td>
                          </tr>

                         <%--Pregunta 10--%>

                         <tr > 
                              <td >
                                   <div class="ctrlBasico">
                                  <label style="width:180px;">10. Osado - audaz</label>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta10Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta10"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta10Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta10"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                       </td>
                          </tr>

                         <%--Pregunta 11--%>

                         <tr > 
                              <td >
                                   <div class="ctrlBasico">
                                  <label style="width:180px;">11. Arrebatado - prudente</label>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta11Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta11"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta11Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta11"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                       </td>
                          </tr>

                         <%--Pregunta 12--%>

                         <tr > 
                              <td >
                                   <div class="ctrlBasico">
                                  <label style="width:180px;">12. Obtuso - agudo</label>
                                </div>
                                   
                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta12Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta12"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>    

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta12Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta12"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                       </td>
                          </tr>

                         <%--Pregunta 13--%>

                         <tr > 
                              <td >
                                   <div class="ctrlBasico">                                  
                                  <label style="width:180px;">13. Inepto - experto</label>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta13Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta13"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta13Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta13"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                       </td>
                          </tr>

                         <%--Pregunta 14--%>

                         <tr > 
                              <td >
                                   <div class="ctrlBasico">                                  
                                  <label style="width:180px;">14. Esquivar - rehuir</label>
                                </div>
                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta14Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta14"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta14Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta14"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                       </td>
                          </tr>

                         <%--Pregunta 15--%>

                         <tr > 
                              <td >
                                   <div class="ctrlBasico">
                                  <label style="width:180px;">15. Rebelarse - someterse</label>
                                </div>


                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta15Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta15"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>


                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta15Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta15"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                       </td>
                          </tr>

                         <%--Pregunta 16 Observacion BRINCO--%>
                         <tr > 
                              <td >
                                   <div class="ctrlBasico">
                                  <label style="width:180px;">16. Monotonía - variedad</label>
                                </div>
                                       
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta16Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta16"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta16Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta16"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                       </td>
                          </tr>

                         <%--Pregunta 17--%>
                         <tr > 
                              <td >
                                   <div class="ctrlBasico">
                                  <label style="width:180px;">17. Confortar - consolar</label>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta17Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta17"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta17Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta17"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                       </td>
                          </tr>

                         <%--Pregunta 18--%>
                         <tr > 
                              <td >
                                   <div class="ctrlBasico">
                                  <label style="width:180px;">18. Expeler - retener</label>
                                </div>
                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta18Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta18"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div> 

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta18Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta18"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                      </td>
                               
                          </tr>

                         <%--Pregunta 19--%>
                         <tr > 
                              <td >
                                   <div class="ctrlBasico">
                                  <label style="width:180px;">19. Dócil - sumiso</label>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta19Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta19"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta19Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta19"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                       </td>
                          </tr>

                         <%--Pregunta 20--%>
                         <tr > 
                              <td >
                                   <div class="ctrlBasico">
                                  <label style="width:180px;">20. Transitorio - permanente</label>
                                </div>


                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta20Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta20"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta20Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta20"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                       </td>
                               
                          </tr>

                         <%--Pregunta 21--%>
                         <tr > 
                              <td >
                                   <div class="ctrlBasico">
                                  <label style="width:180px;">21. Seguridad - riesgo</label>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta21Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta21"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta21Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta21"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                       </td>
                          </tr>


                         <%--Pregunta 22--%>

                         <tr > 
                              <td >
                                   <div class="ctrlBasico">
                                  <label style="width:180px;">22. Aprobar - objetar</label>
                                </div>
                                   
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta22Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta22"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                  
                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta22Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta22"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>    
                                       </td>
                          </tr>

                         <%--Pregunta 23--%>

                         <tr > 
                              <td >
                                  
                                   <div class="ctrlBasico">
                                  <label style="width:180px;">23. Expeler - arrojar</label>
                                </div>
                                   
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta23Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta23"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                  
                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta23Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta23"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>    
                                       </td>
                          </tr>

                         <%--Pregunta 24--%>

                         <tr > 
                              <td >
                                   <div class="ctrlBasico">
                                  <label style="width:180px;">24. Engaño - impostura</label>
                                </div>
                                       
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta24Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta24"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta24Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta24"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                       </td>
                          </tr>

                         <%--Pregunta 25--%>

                         <tr > 
                              <td >
                                  
                                   <div class="ctrlBasico">
                                  <label style="width:180px;">25. Mitigar - apaciguar</label>
                                </div>
                                   
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta25Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta25"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>    

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta25Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta25"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                       </td>
                               
                          </tr>

                         <%--Pregunta 26--%>

                         <tr > 
                              <td >
                                  
                                   <div class="ctrlBasico">
                                  <label style="width:180px;">26. Incitar - aplacar</label>
                                </div>
                                       
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta26Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta26"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta26Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta26"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                       </td>
                          </tr>

                         <%--Pregunta 2--%>

                         <tr > 
                              <td >
                                   <div class="ctrlBasico">
                                  <label style="width:180px;">27. Reverencia - veneración</label>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta27Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta27"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta27Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta27"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                       </td>
                               
                          </tr>

                         <%--Pregunta 28--%>

                         <tr > 
                              <td >
                                   <div class="ctrlBasico">
                                  <label style="width:180px;">28. Sobriedad - frugalidad</label>
                                </div>
                               
                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta28Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta28"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                  
                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta28Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta28"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>        
                                       </td>
                          </tr>

                         <%--Pregunta 29--%>

                         <tr > 
                              <td >
                                  
                                   <div class="ctrlBasico">                                  
                                  <label style="width:180px;">29. Aumentar - menguar</label>
                                </div>
                                   
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta29Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta29"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                  
                                  
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta29Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta29"
                                        Text="O" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>    
                                       </td>
                               
                          </tr>

                         <%--Pregunta 30--%>

                         <tr > 
                              <td >
                                   <div class="ctrlBasico">
                                  <label style="width:180px;">30. Incitar - instigar</label>
                                </div>
                                   
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta30Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta30"
                                        Text="I" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>  

                                       
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="CPregunta30Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnCPregunta30"
                                        Text="O" Skin="Metro">
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
                     </div>
                </telerik:RadPageView>

                <telerik:RadPageView ID="RPView4" runat="server">
                         <div style="width:95%; margin-left:2%; margin-right:2%;">
                <table style="width: 100%;" >
                    <thead>
                         <tr>
                                 <td width="100%"></td>
                         </tr>
                    </thead>
                     <tbody>
                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="lblPreg1" runat="server" style="font-weight:bold;">1. Un CÍRCULO tiene siempre:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta1Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta1"
                                        Text="Altura" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta1Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta1"
                                        Text="Circunferencia" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta1Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta1"
                                        Text="Latitud" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta1Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta1"
                                        Text="Longitud" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                                                      <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta1Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta1"
                                        Text="Radio" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>


                         
                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label1" runat="server" style="font-weight:bold;">2. Un PÁJARO tiene siempre:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta2Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta2"
                                        Text="Huesos" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta2Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta2"
                                        Text="Huevos" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta2Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta2"
                                        Text="Pico" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta2Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta2"
                                        Text="Nido" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                                                      <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta2Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta2"
                                        Text="Canto" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                         
                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label2" runat="server" style="font-weight:bold;">3. La MÚSICA tiene siempre:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta3Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta3"
                                        Text="Oyente" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta3Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta3"
                                        Text="Piano" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta3Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta3"
                                        Text="Ritmo" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta3Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta3"
                                        Text="Sonido" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                                                      <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta3Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta3"
                                        Text="Volín" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                         
                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label3" runat="server" style="font-weight:bold;">4. Un BANQUETE tiene siempre:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta4Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta4"
                                        Text="Alimentos" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta4Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta4"
                                        Text="Música" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta4Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta4"
                                        Text="Personas" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta4Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta4"
                                        Text="Discursos" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                                                      <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta4Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta4"
                                        Text="Anfitrión" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                         
                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label4" runat="server" style="font-weight:bold;">5. Un CABALLO tiene siempre:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta5Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta5"
                                        Text="Arnés" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta5Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta5"
                                        Text="Cascos" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta5Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta5"
                                        Text="Herraduras" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta5Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta5"
                                        Text="Establo" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                                                      <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta5Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta5"
                                        Text="Cola" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                         
                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label5" runat="server" style="font-weight:bold;">6. Un JUEGO tiene siempre:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta6Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta6"
                                        Text="Cartas" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta6Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta6"
                                        Text="Multas" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta6Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta6"
                                        Text="Jugadores" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta6Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta6"
                                        Text="Castigos" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                                                      <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta6Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta6"
                                        Text="Reglas" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                         
                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label6" runat="server" style="font-weight:bold;">7. Un OBJETO tiene siempre:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta7Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta7"
                                        Text="Calor" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta7Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta7"
                                        Text="Tamaño" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta7Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta7"
                                        Text="Sabor" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta7Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta7"
                                        Text="Valor" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                                                      <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta7Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta7"
                                        Text="Peso" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                         
                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label7" runat="server" style="font-weight:bold;">8. Una CONVERSACIÓN tiene siempre:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta8Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta8"
                                        Text="Acuerdos" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta8Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta8"
                                        Text="Personas" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta8Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta8"
                                        Text="Preguntas" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta8Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta8"
                                        Text="Ingenio" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                                                      <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta8Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta8"
                                        Text="Palabras" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                         
                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label8" runat="server" style="font-weight:bold;">9. Una DEUDA implica siempre:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta9Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta9"
                                        Text="Acreedor" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta9Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta9"
                                        Text="Deudor" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta9Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta9"
                                        Text="Interés" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta9Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta9"
                                        Text="Hipoteca" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                                                      <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta9Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta9"
                                        Text="Pago" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                         
                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label9" runat="server" style="font-weight:bold;">10. Un CIUDADANO tiene siempre:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta10Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta10"
                                        Text="País" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta10Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta10"
                                        Text="Ocupación" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta10Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta10"
                                        Text="Derechos" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta10Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta10"
                                        Text="Propiedad" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                                                      <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta10Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta10"
                                        Text="Voto" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                         
                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label10" runat="server" style="font-weight:bold;">11. Un MUSEO tiene siempre:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta11Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta11"
                                        Text="Animales" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta11Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta11"
                                        Text="Orden" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta11Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta11"
                                        Text="Colecciones" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta11Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta11"
                                        Text="Minerales" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                                                      <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta11Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta11"
                                        Text="Visitantes" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                         
                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label11" runat="server" style="font-weight:bold;">12. Un COMPROMISO implica siempre:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta12Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta12"
                                        Text="Obligación" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta12Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta12"
                                        Text="Acuerdo" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta12Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta12"
                                        Text="Amistad" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta12Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta12"
                                        Text="Respeto" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                                                      <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta12Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta12"
                                        Text="Satisfacción" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                         
                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label12" runat="server" style="font-weight:bold;">13. Un BOSQUE tiene siempre:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta13Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta13"
                                        Text="Animales" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta13Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta13"
                                        Text="Flores" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta13Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta13"
                                        Text="Sombra" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta13Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta13"
                                        Text="Maleza" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                                                      <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta13Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta13"
                                        Text="Árboles" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                         
                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label13" runat="server" style="font-weight:bold;">14. Los OBSTÁCULOS tienen siempre:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta14Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta14"
                                        Text="Dificultad" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta14Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta14"
                                        Text="Desaliento" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta14Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta14"
                                        Text="Fracaso" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta14Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta14"
                                        Text="Impedimento" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                                                      <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta14Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta14"
                                        Text="Estímulo" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                         
                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label14" runat="server" style="font-weight:bold;">15. El ABORRECIMIENTO tiene siempre:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta15Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta15"
                                        Text="Aversión" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta15Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta15"
                                        Text="Desagrado" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta15Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta15"
                                        Text="Temor" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta15Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta15"
                                        Text="Ira" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                                                      <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta15Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta15"
                                        Text="Timidez" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                         
                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label15" runat="server" style="font-weight:bold;">16. Una REVISTA tiene siempre:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta16Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta16"
                                        Text="Anuncios" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta16Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta16"
                                        Text="Papel" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta16Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta16"
                                        Text="Fotografías" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta16Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta16"
                                        Text="Grabados" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                                                      <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta16Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta16"
                                        Text="Impresión" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                         
                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label16" runat="server" style="font-weight:bold;">17. La CONTROVERSIA implica siempre:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta17Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta17"
                                        Text="Argumentos" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta17Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta17"
                                        Text="Desacuerdos" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta17Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta17"
                                        Text="Aversión" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta17Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta17"
                                        Text="Gritos" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                                                      <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta17Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta17"
                                        Text="Derrota" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                         
                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label17" runat="server" style="font-weight:bold;">18. Un BARCO tiene siempre:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta18Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta18"
                                        Text="Maquinaria" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta18Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta18"
                                        Text="Cañones" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta18Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta18"
                                        Text="Quilla" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta18Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta18"
                                        Text="Timón" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                                                      <div class="ctrlBasico">
                                    <telerik:RadButton ID="DPregunta18Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnDPregunta18"
                                        Text="Velas" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>
                          </tbody> 
                     </table>
                              </div>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RPView5" runat="server">
                    <div style="width:95%; margin-left:2%; margin-right:2%;">
                <table style="width: 100%;" >
                    <thead>
                         <tr>
                                 <td width="100%"></td>
                         </tr>
                    </thead>
                     <tbody>
                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label18" runat="server" style="font-weight:bold;">1.   A 2 por 5 centavos, ¿Cuántos lápices pueden comprarse con 50 centavos?</label>
                                </div>
                                           <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="EtxtPreg1Resp1" runat="server" Width="100"  DecimalDigits="2" ></telerik:RadNumericTextBox>
                                </div>
                                    </td>
                               </tr>
                               

                         <%--PREGUNTA 2--%>


                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label19" runat="server" style="font-weight:bold;">2.  ¿Cuántas horas tardará un automóvil en recorrer 660 kilómetros a la velocidad de 60 kilómetros por hora?</label>
                                </div>
                                           <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="EtxtPreg2Resp11" runat="server" Width="100"  DecimalDigits="2" ></telerik:RadNumericTextBox>
                                </div>
                                    </td>
                               </tr>
                               

                         <%--PREGUNTA 2--%>

                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label20" runat="server" style="font-weight:bold;">3. Si un hombre gana $20.00 diarios y gasta $14.00, ¿Cuántos días tardará en ahorrar $300.00?</label>
                                </div>
                                           <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="EtxtPreg3Resp12" runat="server" Width="100"  DecimalDigits="2" ></telerik:RadNumericTextBox>
                                </div>
                                    </td>
                               </tr>
                               

                         <%--PREGUNTA 2--%>

                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label21" runat="server" style="font-weight:bold;">4. Si dos pasteles cuestan $600.00, ¿Cuántos pesos cuesta la sexta parte de un pastel?</label>
                                </div>
                                           <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="EtxtPreg4Resp13" runat="server" Width="100"  DecimalDigits="2" ></telerik:RadNumericTextBox>
                                </div>
                                    </td>
                               </tr>
                               

                         <%--PREGUNTA 2--%>

                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label22" runat="server" style="font-weight:bold;">5. ¿Cuántas veces más es 2 x 3 x 4 x 6 que 3 x 4?</label>
                                </div>
                                           <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="EtxtPreg5Resp14" runat="server" Width="100"  DecimalDigits="2" ></telerik:RadNumericTextBox>
                                </div>
                                    </td>
                               </tr>
                               

                         <%--PREGUNTA 2--%>

                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label23" runat="server" style="font-weight:bold;">6. ¿Cuánto es el 16 porciento de $120?</label>
                                </div>
                                           <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="EtxtPreg6Resp15" runat="server" Width="100"  DecimalDigits="2" ></telerik:RadNumericTextBox>
                                </div>
                                    </td>
                               </tr>
                               

                         <%--PREGUNTA 2--%>

                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label24" runat="server" style="font-weight:bold;">7. ¿El cuatro porciento de $1,000.00 es igual al ocho porciento de qué cantidad?</label>
                                </div>
                                           <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="EtxtPreg7Resp16" runat="server" Width="100"  DecimalDigits="2" ></telerik:RadNumericTextBox>
                                </div>
                                    </td>
                               </tr>
                               

                         <%--PREGUNTA 2--%>

                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label25" runat="server" style="font-weight:bold;">8. La capacidad de un refrigerador rectangular es de 48 metros cúbicos. Si tiene seis metros de largo por cuatro de ancho, ¿Cuál es su altura?</label>
                                </div>
                                           <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="EtxtPreg8Resp17" runat="server" Width="100"  DecimalDigits="2" ></telerik:RadNumericTextBox>
                                </div>
                                    </td>
                               </tr>
                               

                         <%--PREGUNTA 2--%>

                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label26" runat="server" style="font-weight:bold;">9. Si 7 hombres hacen un pozo de 40 metros en 2 días, ¿Cuántos hombres se necesitan para hacerlo en medio día?</label>
                                </div>
                                           <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="EtxtPreg9Resp18" runat="server" Width="100"  DecimalDigits="2" ></telerik:RadNumericTextBox>
                                </div>
                                    </td>
                               </tr>
                               

                         <%--PREGUNTA 2--%>

                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label27" runat="server" style="font-weight:bold;">10. A tiene $180.00, B tiene 2/3 de lo que tiene A, y C 1/2 de lo que tiene B. ¿Cuánto tienen todos juntos?</label>
                                </div>
                                           <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="EtxtPreg10Resp19" runat="server" Width="100"  DecimalDigits="2" ></telerik:RadNumericTextBox>
                                </div>
                                    </td>
                               </tr>
                               

                         <%--PREGUNTA 2--%>

                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label28" runat="server" style="font-weight:bold;">11. Si un hombre corre 100 metros en 10 segundos, ¿Cuántos metros recorrerá como promedio en 1/5 de segundo?</label>
                                </div>
                                           <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="EtxtPreg11Resp110" runat="server" Width="100"  DecimalDigits="2" ></telerik:RadNumericTextBox>
                                </div>
                                    </td>
                               </tr>
                               

                         <%--PREGUNTA 2--%>

                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label29" runat="server" style="font-weight:bold;">12. Un hombre gasta 1/4 de su sueldo en casa y alimentos y 4/8 en otros gastos. ¿Qué tanto porciento de su sueldo ahorra?</label>
                                </div>
                                           <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="EtxtPreg12Resp111" runat="server" Width="100"  DecimalDigits="2" ></telerik:RadNumericTextBox>
                                </div>
                                    </td>
                               </tr>

                         <%--END TEST--%>
                          </tbody>
                     </table>
                         </div>

                </telerik:RadPageView>
                
                <telerik:RadPageView ID="RPView6" runat="server">

                         <div style="width:95%; margin-left:2%; margin-right:2%;">
                <table style="width: 100%;" >
                    <thead>
                         <tr>
                                 <td width="100%"></td>
                         </tr>
                    </thead>
                     <tbody>
                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label30" runat="server" style="font-weight:bold;">1. ¿La higiene es escencial para la salud?</label>
                                </div>
                                           <div class="ctrlBasico">
                                 <telerik:RadButton ID="FtxtPreg1Resp1" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                         <%--PREGUNTA 2--%>


                           <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label31" runat="server" style="font-weight:bold;">2. ¿Los taquígrafos usan el microscopio?</label>
                                </div>
                                           <div class="ctrlBasico">
                                 <telerik:RadButton ID="FtxtPreg2Resp1" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                         <%--PREGUNTA 2--%>

                           <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label32" runat="server" style="font-weight:bold;">3. ¿Los tiranos son justos con sus inferiores?</label>
                                </div>
                                           <div class="ctrlBasico">
                                 <telerik:RadButton ID="FtxtPreg3Resp1" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                         <%--PREGUNTA 2--%>

                           <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label33" runat="server" style="font-weight:bold;">4. ¿Las personas desamparadas están sujetas con frecuencia a la caridad?</label>
                                </div>
                                           <div class="ctrlBasico">
                                 <telerik:RadButton ID="FtxtPreg4Resp1" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                         <%--PREGUNTA 2--%>

                           <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label34" runat="server" style="font-weight:bold;">5. ¿Las personas venerables son por lo común respetadas?</label>
                                </div>
                                           <div class="ctrlBasico">
                                 <telerik:RadButton ID="FtxtPreg5Resp1" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                         <%--PREGUNTA 2--%>

                           <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label35" runat="server" style="font-weight:bold;">6. ¿Es el escorbuto un medicamento?</label>
                                </div>
                                           <div class="ctrlBasico">
                                 <telerik:RadButton ID="FtxtPreg6Resp1" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                         <%--PREGUNTA 2--%>

                           <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label36" runat="server" style="font-weight:bold;">7. ¿Es la amonestación una clase de instrumento musical?</label>
                                </div>
                                           <div class="ctrlBasico">
                                 <telerik:RadButton ID="FtxtPreg7Resp1" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                         <%--PREGUNTA 2--%>

                           <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label37" runat="server" style="font-weight:bold;">8. ¿Son los colores opacos preferidos para las banderas nacionales?</label>
                                </div>
                                           <div class="ctrlBasico">
                                 <telerik:RadButton ID="FtxtPreg8Resp1" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                         <%--PREGUNTA 2--%>

                           <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label38" runat="server" style="font-weight:bold;">9. ¿Las cosas misteriosas son a veces pavorosas?</label>
                                </div>
                                           <div class="ctrlBasico">
                                 <telerik:RadButton ID="FtxtPreg9Resp1" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                         <%--PREGUNTA 2--%>

                           <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label39" runat="server" style="font-weight:bold;">10. ¿Personas conscientes cometen alguna vez errores?</label>
                                </div>
                                           <div class="ctrlBasico">
                                 <telerik:RadButton ID="FtxtPreg10Resp1" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                         <%--PREGUNTA 2--%>

                           <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label40" runat="server" style="font-weight:bold;">11. ¿Son carnívoros los carneros?</label>
                                </div>
                                           <div class="ctrlBasico">
                                 <telerik:RadButton ID="FtxtPreg11Resp1" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                         <%--PREGUNTA 2--%>

                           <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label41" runat="server" style="font-weight:bold;">12. ¿Se dan asignaturas a los caballos?</label>
                                </div>
                                           <div class="ctrlBasico">
                                 <telerik:RadButton ID="FtxtPreg12Resp1" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                         <%--PREGUNTA 2--%>

                           <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label42" runat="server" style="font-weight:bold;">13. ¿Las cartas anónimas llevan alguna vez firma de quien las escribe?</label>
                                </div>
                                           <div class="ctrlBasico">
                                 <telerik:RadButton ID="FtxtPreg13Resp1" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                         <%--PREGUNTA 2--%>

                           <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label43" runat="server" style="font-weight:bold;">14. ¿Son discontinuos los sonidos intermitentes?</label>
                                </div>
                                           <div class="ctrlBasico">
                                 <telerik:RadButton ID="FtxtPreg14Resp1" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                         <%--PREGUNTA 2--%>

                           <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label44" runat="server" style="font-weight:bold;">15. ¿Las enfermedades estimulan el buen juicio?</label>
                                </div>
                                           <div class="ctrlBasico">
                                 <telerik:RadButton ID="FtxtPreg15Resp1" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                         <%--PREGUNTA 2--%>

                           <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label45" runat="server" style="font-weight:bold;">16. ¿Son siempre perversos los hechos premeditados?</label>
                                </div>
                                           <div class="ctrlBasico">
                                 <telerik:RadButton ID="FtxtPreg16Resp1" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                         <%--PREGUNTA 2--%>

                           <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label46" runat="server" style="font-weight:bold;">17. ¿El contacto social tiende a reducir la timidez?</label>
                                </div>
                                           <div class="ctrlBasico">
                                 <telerik:RadButton ID="FtxtPreg17Resp1" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                         <%--PREGUNTA 2--%>

                           <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label47" runat="server" style="font-weight:bold;">18. ¿Son enfermas las personas que tienen mal carácter?</label>
                                </div>
                                           <div class="ctrlBasico">
                                 <telerik:RadButton ID="FtxtPreg18Resp1" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                         <%--PREGUNTA 2--%>

                           <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label48" runat="server" style="font-weight:bold;">19. ¿Se caracteriza generalmente el rencor por la persistencia?</label>
                                </div>
                                           <div class="ctrlBasico">
                                 <telerik:RadButton ID="FtxtPreg19Resp1" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                         <%--PREGUNTA 2--%>

                           <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label49" runat="server" style="font-weight:bold;">20. ¿Meticuloso quiere decir lo mismo que cuidadoso?</label>
                                </div>
                                           <div class="ctrlBasico">
                                 <telerik:RadButton ID="FtxtPreg20Resp1" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                         <%--END TEST--%>

                          </tbody>
                     </table>
                              </div>
                   </telerik:RadPageView>

                <telerik:RadPageView ID="RPView7" runat="server">
                           <div style="width:95%; margin-left:2%; margin-right:2%;">
                <table style="width: 100%;" >
                    <thead>
                         <tr>
                                 <td width="100%"></td>
                         </tr>
                    </thead>
                     <tbody>
                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label50" runat="server" style="font-weight:bold;">1. ABRIGO es a USAR como el PAN es a:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg1Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Comer" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg1Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Hambre" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg1Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Agua" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg1Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Cocinar" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>


                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label51" runat="server" style="font-weight:bold;">2. SEMANA es a MES como MES es a:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg2Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Año" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg2Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Hora" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg2Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Minuto" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg2Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Siglo" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>
                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label52" runat="server" style="font-weight:bold;">3. LEÓN es a ANIMAL como ROSA es a:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg3Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Olor" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg3Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Hoja" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg3Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Planta" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg3Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Espina" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label53" runat="server" style="font-weight:bold;">4. LIBERTAD es a INDEPENDIENCIA como CAUTIVERIO es a:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg4Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Negro" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg4Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Esclavitud" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg4Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Libre" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg4Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Sufrir" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label54" runat="server" style="font-weight:bold;">5. DECIR es a DIJO como ESTAR es a:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg5Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Cantar" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg5Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Estuvo" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg5Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Hablando" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg5Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Cantó" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                      
                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label55" runat="server" style="font-weight:bold;">6. LUNES es a MARTES como VIERNES es a:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg6Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Semana" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg6Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Jueves" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg6Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Día" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg6Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Sábado" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label56" runat="server" style="font-weight:bold;">7. PLOMO es a PESADO como CORCHO es a:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg7Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Botella" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg7Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Peso" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg7Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Ligero" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg7Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Flotar" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                      
                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label57" runat="server" style="font-weight:bold;">8. ÉXITO es a ALEGRÍA como FRACASO es a:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg8Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Tristeza" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg8Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Suerte" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg8Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Fracasar" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg8Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Trabajo" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label58" runat="server" style="font-weight:bold;">9. GATO es a TIGRE como PERRO es a:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg9Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Lobo" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg9Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Ladrido" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg9Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Mordida" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg9Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Agarrar" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label59" runat="server" style="font-weight:bold;">10. 4 es a 16 como 5 es a:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg10Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="7" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg10Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="45" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg10Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="35" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg10Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="25" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label60" runat="server" style="font-weight:bold;">11. LLORAR es a REIR como TRISTE es a:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg11Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Muerte" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg11Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Alegre" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg11Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Mortaja" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg11Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Doctor" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label61" runat="server" style="font-weight:bold;">12. VENENO es a MUERTE como ALIMENTO es a:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg12Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Comer" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg12Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Pájaro" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg12Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Vida" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg12Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Malo" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label62" runat="server" style="font-weight:bold;">13. 1 es a 3 como 9 es a:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg13Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="18" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg13Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="27" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg13Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="36" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg13Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="45" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label63" runat="server" style="font-weight:bold;">14. ALIMENTO es a HAMBRE como AGUA es a:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg14Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Beber" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg14Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Claro" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg14Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Sed" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg14Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Puro" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label64" runat="server" style="font-weight:bold;">15. AQUÍ es a ALLÍ como ÉSTE es a:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg15Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Estos" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg15Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Aquel" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg15Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Ese" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg15Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Entonces" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label65" runat="server" style="font-weight:bold;">16. TIGRE es a PELO como TRUCHA es a:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg16Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Agua" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg16Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Pez" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg16Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Escama" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg16Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Nadar" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label66" runat="server" style="font-weight:bold;">17. PERVERTIDO es a DEPRAVADO como INCORRUPTO es a:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg17Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Patria" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg17Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Honrado" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg17Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Canción" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg17Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Estudio" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label67" runat="server" style="font-weight:bold;">18. B es a D como segundo es a:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg18Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Tercero" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg18Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Último" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg18Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Cuarto" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg18Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Poste" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label68" runat="server" style="font-weight:bold;">19. ESTADO es a GOBERNADOR como EJÉRCITO es a:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg19Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Marina" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg19Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Soldado" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg19Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="General" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg19Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Sargento" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label69" runat="server" style="font-weight:bold;">20. SUJETO es a PREDICADO como NOMBRE es a:</label>
                                </div>
                                    </td>
                               </tr>
                               <tr>
                               <td>
                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg20Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Pronombre" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg20Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Adverbio" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                                                      <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg20Resp3300" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Verbo" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>


                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="GbtnPreg20Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="Adjetivo" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                              </td>
                          </tr>

                         <%--PREGUNTA 2--%>

                          </tbody>
                     </table>
                                </div>

                </telerik:RadPageView>
                <telerik:RadPageView ID="RPView8" runat="server">

                         <div style="width:95%; margin-left:2%; margin-right:2%;">
                <table style="width: 100%;" >
                    <thead>
                         <tr>
                                 <td width="100%"></td>
                         </tr>
                    </thead>
                     <tbody>
                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label70" runat="server" style="font-weight:bold;">1. con crecen los niños edad la</label>
                                </div>

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg1Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="V" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg1Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="F" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label71" runat="server" style="font-weight:bold;">2. buena mar beber el para agua de es</label>
                                </div>

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg2Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="V" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg2Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="F" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label72" runat="server" style="font-weight:bold;">3. lo es paz la guerra opuesto la a</label>
                                </div>

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg3Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="V" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg3Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="F" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label73" runat="server" style="font-weight:bold;">4. caballos automóvil un que caminan los despacio más</label>
                                </div>

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg4Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="V" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg4Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="F" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label74" runat="server" style="font-weight:bold;">5. consejo a veces es buen seguir un difícil</label>
                                </div>

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg5Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="V" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg5Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="F" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label75" runat="server" style="font-weight:bold;">6. cuatrocientas todos páginas contienen libros los</label>
                                </div>

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg6Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="V" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg6Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="F" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label76" runat="server" style="font-weight:bold;">7. crecen las que fresas el más roble</label>
                                </div>

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg7Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="V" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg7Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="F" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label77" runat="server" style="font-weight:bold;">8. verdadera comparada no puede amistad ser</label>
                                </div>

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg8Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="V" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg8Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="F" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label78" runat="server" style="font-weight:bold;">9. envidia la perjudiciales gula son y la</label>
                                </div>

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg9Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="V" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg9Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="F" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label79" runat="server" style="font-weight:bold;">10. nunca acciones premiadas las deben buenas ser</label>
                                </div>

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg10Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="V" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg10Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="F" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label80" runat="server" style="font-weight:bold;">11. exteriores engañan nunca apariencias las nos</label>
                                </div>

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg11Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="V" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg11Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="F" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label81" runat="server" style="font-weight:bold;">12. nunca es hombre las que acciones demuestran un lo</label>
                                </div>

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg12Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="V" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg12Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="F" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label82" runat="server" style="font-weight:bold;">13. ciertas siempre muerte de causan clases enfermedades</label>
                                </div>

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg13Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="V" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg13Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="F" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label83" runat="server" style="font-weight:bold;">14. odio indeseables aversión sentimientos el son y la</label>
                                </div>

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg14Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="V" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg14Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="F" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label84" runat="server" style="font-weight:bold;">15. frecuentemente por juzgar podemos acciones hombres nosotros sus a los</label>
                                </div>

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg15Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="V" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg15Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="F" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>

                         
                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label85" runat="server" style="font-weight:bold;">16. una es sábana sarapes tan nunca los calientes como</label>
                                </div>

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg16Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="V" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg16Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="F" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>

                          <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label86" runat="server" style="font-weight:bold;">17. nunca que descuidados los tropiezan son</label>
                                </div>

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg17Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="V" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="HbtnPreg17Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnPregunta1"
                                        Text="F" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--END TEST--%>
                          </tbody>
                     </table>
                              </div>

                      
                </telerik:RadPageView>
                <telerik:RadPageView ID="RPView9" runat="server">
                    
                         <div style="width:95%; margin-left:2%; margin-right:2%;">
                <table style="width: 100%;" >
                    <thead>
                         <tr>
                                 <td width="100%"></td>
                         </tr>
                    </thead>
                     <tbody>

                                    <tr>
                                         <td>
        <div class="ctrlBasico">
                                        <label name="" id="Label87" runat="server" style="font-weight:bold;">1.</label>
                                </div>
                                         </td>
                                    </tr>
                               <tr>

                                    <td>
                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg1Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta1"
                                        Text="Saltar" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg1Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta1"
                                        Text="Correr" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg1Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta1"
                                        Text="Brincar" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg1Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta1"
                                        Text="Pararse" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg1Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta1"
                                        Text="Caminar" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>


                          <tr>
                               <td>
                                         <div class="ctrlBasico">
                                        <label name="" id="Label88" runat="server" style="font-weight:bold;">2.</label>
                                </div>
                               </td>
                          </tr>
                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg2Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta2"
                                        Text="Monarquía" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg2Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta2"
                                        Text="Comunista" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg2Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta2"
                                        Text="Demócrata" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg2Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta2"
                                        Text="Anarquista" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg2Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta2"
                                        Text="Católico" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>


                          <tr>
                               <td>
       <div class="ctrlBasico">
                                        <label name="" id="Label89" runat="server" style="font-weight:bold;">3.</label>
                                </div>
                               </td>
                          </tr>
                               <tr>
                                    <td>
                                 

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg3Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta3"
                                        Text="Muerte" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg3Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta3"
                                        Text="Duelo" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg3Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta3"
                                        Text="Paseo" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg3Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta3"
                                        Text="Pobreza" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg3Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta3"
                                        Text="Tristeza" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>

                          <tr>
                               <td>
                                       <div class="ctrlBasico">
                                        <label name="" id="Label90" runat="server" style="font-weight:bold;">4.</label>
                                </div>
                               </td>
                          </tr>
                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg4Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta4"
                                        Text="Carpintero" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg4Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta4"
                                        Text="Doctor" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg4Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta4"
                                        Text="Abogado" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg4Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta4"
                                        Text="Ingeniero" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg4Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta4"
                                        Text="Profesor" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>

                          <tr>
                               <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label91" runat="server" style="font-weight:bold;">5.</label>
                                </div>
                               </td>
                          </tr>
                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg5Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta5"
                                        Text="Cama" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg5Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta5"
                                        Text="Silla" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg5Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta5"
                                        Text="Plato" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg5Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta5"
                                        Text="Sofá" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg5Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta5"
                                        Text="Mesa" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>

                          <tr>
                               <td>
     <div class="ctrlBasico">
                                        <label name="" id="Label92" runat="server" style="font-weight:bold;">6.</label>
                                </div>
                               </td>
                          </tr>
                               <tr>
                                    <td>
                                   

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg6Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta6"
                                        Text="Francisco" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg6Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta6"
                                        Text="Santiago" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg6Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta6"
                                        Text="Juan" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg6Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta6"
                                        Text="Sara" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg6Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta6"
                                        Text="Guillermo" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>

                          <tr>
                               <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label93" runat="server" style="font-weight:bold;">7.</label>
                                </div>
                               </td>
                          </tr>
                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg7Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta7"
                                        Text="Duro" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg7Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta7"
                                        Text="Áspero" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg7Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta7"
                                        Text="Liso" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg7Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta7"
                                        Text="Suave" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg7Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta7"
                                        Text="Dulce" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>
                          <tr>
                               <td>
     <div class="ctrlBasico">
                                        <label name="" id="Label94" runat="server" style="font-weight:bold;">8.</label>
                                </div>
                               </td>
                          </tr>
                         
                               <tr>
                                    <td>
                                   

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg8Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta8"
                                        Text="Digestión" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg8Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta8"
                                        Text="Oído" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg8Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta8"
                                        Text="Vista" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg8Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta8"
                                        Text="Olfato" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg8Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta8"
                                        Text="Tacto" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>

                          <tr>
                               <td>
                                      <div class="ctrlBasico">
                                        <label name="" id="Label95" runat="server" style="font-weight:bold;">9.</label>
                                </div>
                               </td>
                          </tr>
                               <tr>
                                    <td>
                                     

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg9Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta9"
                                        Text="Automóvil" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg9Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta9"
                                        Text="Bicicleta" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg9Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta9"
                                        Text="Guayín" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg9Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta9"
                                        Text="Telégrafo" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg9Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta9"
                                        Text="Tren" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>
                          <tr>
                               <td>
     <div class="ctrlBasico">
                                        <label name="" id="Label96" runat="server" style="font-weight:bold;">10.</label>
                                </div>
                               </td>
                          </tr>
                               <tr>
                                    <td>
                                   

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg10Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta10"
                                        Text="Abajo" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg10Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta10"
                                        Text="Acá" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg10Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta10"
                                        Text="Reciente" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg10Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta10"
                                        Text="Arriba" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg10Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta10"
                                        Text="Allá" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>
                         
                          <tr>
                               <td>
        <div class="ctrlBasico">
                                        <label name="" id="Label97" runat="server" style="font-weight:bold;">11.</label>
                                </div>
                               </td>
                          </tr>
                               <tr>
                                    <td>
                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg11Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta11"
                                        Text="Hidalgo" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg11Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta11"
                                        Text="Morelos" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg11Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta11"
                                        Text="Bravo" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg11Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta11"
                                        Text="Matamoros" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg11Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta11"
                                        Text="Bolívar" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>
                         
                          <tr>
                               <td>
                                           <div class="ctrlBasico">
                                        <label name="" id="Label98" runat="server" style="font-weight:bold;">12.</label>
                                </div>
                               </td>
                          </tr>
                               <tr>
                                    <td>
                                

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg12Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta12"
                                        Text="Danés" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg12Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta12"
                                        Text="Galgo" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg12Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta12"
                                        Text="Bulldog" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg12Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta12"
                                        Text="Pekinés" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg12Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta12"
                                        Text="Leghorn" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>
                         
                          <tr>
                               <td>
     <div class="ctrlBasico">
                                        <label name="" id="Label99" runat="server" style="font-weight:bold;">13.</label>
                                </div>
                               </td>
                          </tr>
                               <tr>
                                    <td>
                                   

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg13Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta13"
                                        Text="Tela" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg13Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta13"
                                        Text="Algodón" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg13Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta13"
                                        Text="Lino" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg13Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta13"
                                        Text="Seda" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg13Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta13"
                                        Text="Lana" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>
                         
                          <tr>
                               <td>
        <div class="ctrlBasico">
                                        <label name="" id="Label100" runat="server" style="font-weight:bold;">14.</label>
                                </div>
                               </td>
                          </tr>
                               <tr>
                                    <td>
                                

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg14Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta14"
                                        Text="Ira" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg14Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta14"
                                        Text="Odio" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg14Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta14"
                                        Text="Alegría" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg14Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta14"
                                        Text="Piedad" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg14Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta14"
                                        Text="Razonamiento" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>
                         
                          <tr>
                               <td>
                                        <div class="ctrlBasico">
                                        <label name="" id="Label101" runat="server" style="font-weight:bold;">15.</label>
                                </div>
                               </td>
                          </tr>
                               <tr>
                                
                                       <td>

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg15Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta15"
                                        Text="Edison" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg15Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta15"
                                        Text="Franklin" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg15Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta15"
                                        Text="Marconi" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg15Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta15"
                                        Text="Fulton" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg15Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta15"
                                        Text="Shakespeare" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>
                         
                          <tr>
                               <td>
      <div class="ctrlBasico">
                                        <label name="" id="Label102" runat="server" style="font-weight:bold;">16.</label>
                                </div>
                               </td>
                          </tr>
                               <tr>
                                    <td>
                                  

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg16Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta16"
                                        Text="Mariposa" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg16Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta16"
                                        Text="Halcón" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg16Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta16"
                                        Text="Avestruz" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg16Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta16"
                                        Text="Petirrojo" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg16Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta16"
                                        Text="Golondrina" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>
                         
                          <tr>
                               <td>
                                          <div class="ctrlBasico">
                                        <label name="" id="Label103" runat="server" style="font-weight:bold;">17.</label>
                                </div>
                               </td>
                          </tr>
                               <tr>
                                    <td>
                                 

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg17Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta17"
                                        Text="Dar" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg17Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta17"
                                        Text="Prestar" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg17Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta17"
                                        Text="Perder" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg17Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta17"
                                        Text="Ahorrar" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg17Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta17"
                                        Text="Derrochar" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                            <%--PREGUNTA 2--%>
                         
                          <tr>
                               <td>
                                         <div class="ctrlBasico">
                                        <label name="" id="Label104" runat="server" style="font-weight:bold;">18.</label>
                                </div>
                               </td>
                          </tr>
                               <tr>
                                    <td>
                                  

                                        <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg18Resp1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta18"
                                        Text="Australia" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg18Resp2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta18"
                                        Text="Cuba" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg18Resp3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta18"
                                        Text="Córcega" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                   <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg18Resp4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta18"
                                        Text="Irlanda" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>

                                          <div class="ctrlBasico">
                                    <telerik:RadButton ID="IbtnPreg18Resp5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                        AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="RbtnIPregunta18"
                                        Text="España" Skin="Metro">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                                    </td>
                               </tr>
                        
                            <%--END TEST--%>
                          </tbody>
                     </table>
                              </div>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RPView10" runat="server">
                    <div style="width:95%; margin-left:2%; margin-right:2%;">
                <table style="width: 100%;" >
                    <thead>
                         <tr>
                                 <td width="100%"></td>
                         </tr>
                    </thead>
                     <tbody>
                               <tr>
                                    <td>
                                  
                                           <div class="ctrlBasico">
                                        <label name="" id="Label105" runat="server" style="font-weight:bold;">1.</label>
                                </div>
                                         </td>
                                         </tr>
                               <tr>
                                         <td>
                                           <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox12" Value="8" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox13" Value="7" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox14" Value="6" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox15" Value="5" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox16" Value="4" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox17" Value="3" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="JbtnPreg1Resp1"  runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="JbtnPreg1Resp2"  runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                    </td>
                               </tr>

                         <%--PREGUNTA 2--%>

                               <tr>
                                    <td>
                                  
                                           <div class="ctrlBasico">
                                        <label name="" id="Label106" runat="server" style="font-weight:bold;">2.</label>
                                </div>
                                         </td>
                                         </tr>
                               <tr>
                                         <td>
                                           <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox20" Value="3" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox21" Value="8" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox22" Value="13" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox23" Value="18" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox24" Value="23" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox25" Value="28" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="JbtnPreg2Resp1"  runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="JbtnPreg2Resp2"  runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                    </td>
                               </tr>

                         <%--PREGUNTA 2--%>


                               <tr>
                                    <td>
                                  
                                           <div class="ctrlBasico">
                                        <label name="" id="Label107" runat="server" style="font-weight:bold;">3.</label>
                                </div>
                                         </td>
                                         </tr>
                               <tr>
                                         <td>
                                           <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox28" Value="1" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox29" Value="2" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox30" Value="4" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox31" Value="8" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox32" Value="16" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox33" Value="32" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="JbtnPreg3Resp1"  runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="JbtnPreg3Resp2"  runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                    </td>
                               </tr>

                         <%--PREGUNTA 2--%>


                               <tr>
                                    <td>
                                  
                                           <div class="ctrlBasico">
                                        <label name="" id="Label108" runat="server" style="font-weight:bold;">4.</label>
                                </div>
                                         </td>
                                         </tr>
                               <tr>
                                         <td>
                                           <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox36" Value="8" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox37" Value="8" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox38" Value="6" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox39" Value="6" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox40" Value="4" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox41" Value="4" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="JbtnPreg4Resp1"  runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="JbtnPreg4Resp2"  runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                    </td>
                               </tr>

                         <%--PREGUNTA 2--%>


                               <tr>
                                    <td>
                                  
                                           <div class="ctrlBasico">
                                        <label name="" id="Label109" runat="server" style="font-weight:bold;">5.</label>
                                </div>
                                         </td>
                                         </tr>
                               <tr>
                                         <td>
                                           <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox44" Value="11" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox45" Value="12" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox46" Value="12" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox47" Value="12" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox48" Value="12" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox49" Value="12" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="JbtnPreg5Resp1"  runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="JbtnPreg5Resp2"  runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                    </td>
                               </tr>

                         <%--PREGUNTA 2--%>


                               <tr>
                                    <td>
                                  
                                           <div class="ctrlBasico">
                                        <label name="" id="Label110" runat="server" style="font-weight:bold;">6.</label>
                                </div>
                                         </td>
                                         </tr>
                               <tr>
                                         <td>
                                           <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox52" Value="8" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox53" Value="9" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox54" Value="12" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox55" Value="13" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox56" Value="16" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox57" Value="17" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="JbtnPreg6Resp1"  runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="JbtnPreg6Resp2"  runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                    </td>
                               </tr>

                         <%--PREGUNTA 2--%>

                               <tr>
                                    <td>
                                  
                                           <div class="ctrlBasico">
                                        <label name="" id="Label111" runat="server" style="font-weight:bold;">7.</label>
                                </div>
                                         </td>
                                         </tr>
                               <tr>
                                         <td>
                                           <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox60" Value="16" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox61" Value="8" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox62" Value="4" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox63" Value="2" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox64" Value="1" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox65" Value="12" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="JbtnPreg7Resp1"  runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="JbtnPreg7Resp2"  runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                    </td>
                               </tr>

                         <%--PREGUNTA 2--%>


                               <tr>
                                    <td>
                                  
                                           <div class="ctrlBasico">
                                        <label name="" id="Label112" runat="server" style="font-weight:bold;">8.</label>
                                </div>
                                         </td>
                                         </tr>
                               <tr>
                                         <td>
                                           <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox68" Value="31" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox69" Value="40" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox70" Value="49" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox71" Value="58" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox72" Value="67" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox73" Value="76" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="JbtnPreg8Resp1"  runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="JbtnPreg8Resp2"  runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                    </td>
                               </tr>

                         <%--PREGUNTA 2--%>


                               <tr>
                                    <td>
                                  
                                           <div class="ctrlBasico">
                                        <label name="" id="Label113" runat="server" style="font-weight:bold;">9.</label>
                                </div>
                                         </td>
                                         </tr>
                               <tr>
                                         <td>
                                           <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox76" Value="3" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox77" Value="5" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox78" Value="4" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox79" Value="6" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox80" Value="5" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox81" Value="7" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="JbtnPreg9Resp1"  runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="JbtnPreg9Resp2"  runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                    </td>
                               </tr>

                         <%--PREGUNTA 2--%>


                               <tr>
                                    <td>
                                  
                                           <div class="ctrlBasico">
                                        <label name="" id="Label114" runat="server" style="font-weight:bold;">10.</label>
                                </div>
                                         </td>
                                         </tr>
                               <tr>
                                         <td>
                                           <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox84" Value="7" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox85" Value="11" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox86" Value="15" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox87" Value="16" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox88" Value="20" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox89" Value="25" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                                   <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox92" Value="29" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="JbtnPreg10Resp1"  runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="JbtnPreg10Resp2"  runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                    </td>
                               </tr>

                         <%--PREGUNTA 2--%>
                         

                               <tr>
                                    <td>
                                  
                                           <div class="ctrlBasico">
                                        <label name="" id="Label115" runat="server" style="font-weight:bold;">11.</label>
                                </div>
                                         </td>
                                         </tr>
                               <tr>
                                         <td>
                                           <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox93" Value="0.4" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox94" Value="0.2" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox95" Value="1" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="RadNumericTextBox96" Value="5" ReadOnly="true" runat="server" Width="100"  NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="JbtnPreg11Resp1"  runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                                  <div class="ctrlBasico">
                                     <telerik:RadNumericTextBox ID="JbtnPreg11Resp2"  runat="server" Width="100" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" style="text-align:center" ></telerik:RadNumericTextBox>
                                </div>
                                    </td>
                               </tr>

                         <%--END TEST--%>

                          </tbody>
                     </table>
                         </div>
                </telerik:RadPageView>
            </telerik:RadMultiPage>


<%--             --%>
            </telerik:RadPane>

        </telerik:RadSplitter>

    </div>

    <div style="clear: both; height: 10px;"></div>

    <div class="DivMoveLeft">
        <div class="Cronometro">Tiempo restante <span id="time">15:00</span></div>
    </div>
    
    <div class="DivBtnTerminarDerecha">
        <%--<telerik:RadButton ID="RadButton407" runat="server" OnClientClicking="close_window" OnClick="btnTerminar_Click" Text="Siguiente" AutoPostBack="false" ></telerik:RadButton>--%>
        <telerik:RadButton ID="btnTerminar" runat="server" OnClientClicked="prueba" OnClick="btnTerminar_Click" Text="Siguiente" AutoPostBack="false" ></telerik:RadButton>
    </div>
 
       <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
