<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaResultadosEntrevista.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaResultadosEntrevista" %>

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

        td {
            padding: 5px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">

            var vPruebaEstatus = "";

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


                    function CloseTest() {
                        GetRadWindow().close();
                    }


                    function ConfirmarEliminarRespuestas(sender, args) {
                        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                            if (shouldSubmit) {
                                this.click();
                            }
                        });
                        radconfirm("Este proceso borrará las respuestas de la prueba, ¿Deseas continuar?", callBackFunction, 400, 150, null, "Eliminar respuestas");
                        args.set_cancel(true);
                    }


        </script>
    </telerik:RadCodeBlock>

    <label name="" id="lbltitulo" class="labelTitulo">Factores adicionales</label>

    <div style="height: calc(100% - 100px);">

        <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Horizontal">
            <telerik:RadPane ID="rpnOpciones" runat="server" Height="30" Width="100%" Scrolling="None">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="RSPHelp" runat="server" Title="Instrucciones" Width="100%" Height="240">
                        <p style="margin: 10px;">
                            <strong>Instrucciones:</strong>
                            <label id="Label1" runat="server">Una vez realizada la entrevista de competencias, deberás calificar los siguientes aspectos de la persona evaluada, contesta de manera objetiva y considera las evidencias de desempeño a fin de que la calificación emitida sea lo más precisa posible.</label>
                        </p>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
            <telerik:RadPane ID="rpnPrueba" runat="server">
                <div style="clear: both; height: 10px"></div>
                <telerik:RadTabStrip ID="tbresultados" runat="server" SelectedIndex="0" MultiPageID="mpgresultados" AutoPostBack="false" PerTabScrolling="true">
                    <Tabs>
                        <telerik:RadTab Text="Comunicación verbal"></telerik:RadTab>
                        <telerik:RadTab Text="Comunicación no verbal"></telerik:RadTab>
                        <telerik:RadTab Text="Seguridad en si mismo"></telerik:RadTab>
                        <telerik:RadTab Text="Enfoque a resultados"></telerik:RadTab>
                        <telerik:RadTab Text="Manejo del conflicto"></telerik:RadTab>
                        <telerik:RadTab Text="Carisma"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="mpgresultados" runat="server" SelectedIndex="0" Height="70%" Width="100%">
                    <telerik:RadPageView ID="rpvcomunicacionverbal" runat="server" Height="100%">

                        <div style="padding: 5px">

                            <div style="border: 1px solid #ddd">
                                <p>
                                    <strong>Definición del factor</strong><br />
                                    Capacidad para expresar adecuadamente de manera verbal ideas o pensamientos de una manera lógica y concisa con un adecuado manejo del lenguaje.
                                </p>
                            </div>
                            <div style="clear: both; height: 20px"></div>

                            <div>
                                <label>Por favor seleccione uno de los siguientes textos para la prueba:</label>
                            </div>

                            <div style="clear: both; height: 20px"></div>

                            <table width="100%" style="border: 1px solid #ddd">
                                <colgroup>
                                    <col width="88%" />
                                    <col width="12%" />
                                </colgroup>
                                <tr>
                                    <td>
                                        <p>
                                            La persona no tiene la capacidad para comunicar de manera ordenada e inteligible sus ideas. Es difícil entender el objetivo de su comunicación, ya sea por nerviosismo o porque carece de los medios necesarios para poder establecer una comunicación adecuada.
                                        </p>
                                    </td>
                                    <td style="text-align: center">
                                        <telerik:RadButton RenderMode="Lightweight" ID="rbrespuesta1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Radio"
                                            Text="" Skin="Metro" Value="1">
                                            <%--  <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconHeight="0" PrimaryIconWidth="0" SecondaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconHeight="0" PrimaryIconWidth="0" SecondaryIconCssClass="rbToggleRadio"
                                                    SecondaryIconRight="0"></telerik:RadButtonToggleState>
                                            </ToggleStates>--%>
                                        </telerik:RadButton>

                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <p>
                                            La persona muestra dificultad para comunicar adecuadamente sus ideas, sin embargo cuando se trata de ideas sencillas puede lograr comunicarlas razonablemente bien. Su manejo del lenguaje es pobre y no siempre utiliza las palabras adecuadas para expresarse de manera precisa.
                                        </p>
                                    </td>
                                    <td style="text-align: center">

                                        <telerik:RadButton RenderMode="Lightweight" ID="rbrespuesta2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Radio"
                                            Text="" Skin="Metro" Value="2">
                                            <%-- <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconHeight="0" 0="PrimaryIconWidth" SecondaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconHeight="0" PrimaryIconWidth="0" SecondaryIconCssClass="rbToggleRadio"
                                                    SecondaryIconRight="0"></telerik:RadButtonToggleState>
                                            </ToggleStates>--%>
                                        </telerik:RadButton>

                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <p>
                                            La persona muestra una capacidad media para comunicar sus ideas, pudiendo hacerlo adecuadamente con un manejo del lenguaje adecuado aunque con ciertas limitaciones, sobre todo cuando se trata de expresar ideas complejas.
                                        </p>
                                    </td>
                                    <td style="text-align: center">

                                        <telerik:RadButton RenderMode="Lightweight" ID="rbrespuesta3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Radio"
                                            Text="" Skin="Metro" Value="3">
                                            <%--   <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconHeight="0" PrimaryIconWidth="0" SecondaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconHeight="0" PrimaryIconWidth="0" SecondaryIconCssClass="rbToggleRadio"
                                                    SecondaryIconRight="0"></telerik:RadButtonToggleState>
                                            </ToggleStates>--%>
                                        </telerik:RadButton>

                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <p>
                                            La persona muestra una buena capacidad para comunicar de manera clara y fluida sus ideas, utiliza bien el lenguaje, el tono y el volumen de voz para lograr que su discurso sea adecuado y logre comunicar aquello que desea.
                                        </p>
                                    </td>
                                    <td style="text-align: center">

                                        <telerik:RadButton RenderMode="Lightweight" ID="rbrespuesta4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Radio"
                                            Text="" Skin="Metro" Value="4">
                                            <%--  <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconHeight="0" PrimaryIconWidth="0" SecondaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconHeight="0" PrimaryIconWidth="0" SecondaryIconCssClass="rbToggleRadio"
                                                    SecondaryIconRight="0"></telerik:RadButtonToggleState>
                                            </ToggleStates>--%>
                                        </telerik:RadButton>

                                    </td>
                                </tr>

                            </table>
                        </div>

                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpvcomunicacionnoverbal" runat="server" Height="100%">

                        <div style="padding: 5px">

                            <div style="border: 1px solid #ddd">
                                <p>
                                    <strong>Definición del factor</strong><br />
                                    Capacidad para comunicarse efectivamente de manera no verbal, utilizando recursos expresivos como gesticulación, postura y ademanes.
                                </p>
                            </div>
                            <div style="clear: both; height: 20px"></div>

                            <div>
                                <label>Por favor seleccione uno de los siguientes textos para la prueba:</label>
                            </div>

                            <div style="clear: both; height: 20px"></div>

                            <table width="100%" style="border: 1px solid #ddd">
                                <colgroup>
                                    <col width="90%" />
                                    <col width="10%" />
                                </colgroup>
                                <tr>
                                    <td>
                                        <p>
                                            La persona no tiene la capacidad para utilizar adecuadamente otros recursos expresivos, puede presentar problemas para establecer contacto visual con la persona con la que se comunica, su postura es rígida, agresiva o temerosa, carece de capacidad para usar ademanes que refuercen las ideas que esta expresando verbalmente.
                                        </p>
                                    </td>
                                    <td style="text-align: center">
                                        <telerik:RadButton RenderMode="Lightweight" ID="rbrespuesta5" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Radio1"
                                            Text="" Skin="Metro" Value="1">
                                            <%-- <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconHeight="0" PrimaryIconWidth="0" SecondaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconHeight="0" PrimaryIconWidth="0" SecondaryIconCssClass="rbToggleRadio"
                                                    SecondaryIconRight="0"></telerik:RadButtonToggleState>
                                            </ToggleStates>--%>
                                        </telerik:RadButton>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p>
                                            La persona muestra dificultad para utilizar adecuadamente otros recursos expresivos, sin embargo trata de apoyar su expresión verbal con ademanes y expresiones gestuales, aunque lo hace de manera pobre y limitada.
                                        </p>
                                    </td>
                                    <td style="text-align: center">

                                        <telerik:RadButton RenderMode="Lightweight" ID="rbrespuesta6" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Radio1"
                                            Text="" Skin="Metro" Value="2">
                                            <%--   <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconHeight="0" PrimaryIconWidth="0" SecondaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconHeight="0" PrimaryIconWidth="0" SecondaryIconCssClass="rbToggleRadio"
                                                    SecondaryIconRight="0"></telerik:RadButtonToggleState>
                                            </ToggleStates>--%>
                                        </telerik:RadButton>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p>
                                            La persona muestra una capacidad media para utilizar adecuadamente otros recursos expresivos, establece satisfactoriamente contacto visual y puede apoyar su discurso con expresiones gestuales y con ademanes que ayudan a fortalecer su discurso.
                                        </p>
                                    </td>
                                    <td style="text-align: center">

                                        <telerik:RadButton RenderMode="Lightweight" ID="rbrespuesta7" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Radio1"
                                            Text="" Skin="Metro" Value="3">
                                            <%-- <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconHeight="0" PrimaryIconWidth="0" SecondaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconHeight="0" PrimaryIconWidth="0" SecondaryIconCssClass="rbToggleRadio"
                                                    SecondaryIconRight="0"></telerik:RadButtonToggleState>
                                            </ToggleStates>--%>
                                        </telerik:RadButton>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p>
                                            La persona muestra una buena capacidad para utilizar otros recursos expresivos. Se muestra cómoda y segura durante la exposición de sus argumentos, pudiendo apoyar estos de manera efectiva con gesticulación y ademanes que fortalecen su discurso. Establece contacto visual con el entrevistador, se mantiene y se muestra firme durante sus exposiciones.
                                        </p>
                                    </td>
                                    <td style="text-align: center">
                                        <telerik:RadButton RenderMode="Lightweight" ID="rbrespuesta8" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Radio1"
                                            Text="" Skin="Metro" Value="4">
                                            <%--  <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconHeight="0" PrimaryIconWidth="0" SecondaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconHeight="0" PrimaryIconWidth="0" SecondaryIconCssClass="rbToggleRadio"
                                                    SecondaryIconRight="0"></telerik:RadButtonToggleState>
                                            </ToggleStates>--%>
                                        </telerik:RadButton>

                                    </td>
                                </tr>

                            </table>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpvSeguridad" runat="server" Height="100%">
                        <div style="padding: 5px">
                            <div style="border: 1px solid #ddd">
                                <p>
                                    <strong>Definición del factor</strong><br />
                                    Es el sentimiento valorativo del ser, de quienes somos, del conjunto de rasgos corporales, mentales y espirituales que configuran nuestra persona.
                                </p>
                            </div>
                            <div style="clear: both; height: 20px"></div>
                            <div>
                                <label>Por favor seleccione uno de los siguientes textos para la prueba:</label>
                            </div>
                            <div style="clear: both; height: 20px"></div>
                            <table width="100%" style="border: 1px solid #ddd">
                                <colgroup>
                                    <col width="90%" />
                                    <col width="10%" />
                                </colgroup>
                                <tr>
                                    <td>
                                        <p>
                                            La persona se muestra insegura, sumisa, suele callar, habla en voz baja, se muestra nerviosa, lleva la cara baja y evita el contacto ocular. Expresa poco o nulo sentido de vida, asume la opinión de los demás y oculta la suya. Antepone a los demás que a él y ve a los demás como superiores. Teme expresar sus sentimientos y deseos y no afronta el conflicto.  Se culpabiliza a sí mismo de sus errores.
                                        </p>
                                    </td>
                                    <td style="text-align: center">
                                        <telerik:RadButton RenderMode="Lightweight" ID="btnSeguridadInsegura" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Seguridad"
                                            Text="" Skin="Metro" Value="1">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p>
                                            La persona muestra una actitud agresiva en sus rasgos físicos, postura, movimientos al sentarse, al caminar, al saludar y en su tono de voz. Antepone siempre el yo y ve a los demás como inferiores. Impone su opinión e ignora a los demás. Oculta sus emociones o las exagera. Crítica siempre negativa. Impone sus derechos sin tomar en cuenta a los demás. Muestra exceso de confianza y nunca asume sus errores.
                                        </p>
                                    </td>
                                    <td style="text-align: center">
                                        <telerik:RadButton RenderMode="Lightweight" ID="btnSeguridadAgresiva" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Seguridad"
                                            Text="" Skin="Metro" Value="2">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p>
                                            La persona se muestra segura en su voz, tono y modulación; en su imagen exterior, los gestos y la indumentación. Es importante él o ella pero también los demás y los ve a su igual. Expone su opinión y escucha a los demás. Expresa sentimientos autenticos. Realiza crítica  constructiva. Exige sus derechos con justicia. Muestra autoconfianza y asume sus errores. 
                                        </p>
                                    </td>
                                    <td style="text-align: center">
                                        <telerik:RadButton RenderMode="Lightweight" ID="btnSeguridadMediaSegura" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Seguridad"
                                            Text="" Skin="Metro" Value="3">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p>
                                            La persona se muestra segura, da una imagen de una persona congruente y autentica. Se comunica abierta, directa, franca y adcuadamente. Se autoevalúa objetivamente, se conoce a sí misma y se acepta y valora incondicionalmente. Expresa lo que piensa, siente y quiere. Reconoce de manera real sus fortalezas y limitaciones propias y al mismo tiempo se acepta como valiosa sin condiciones o reservas. Pone en práctica sus ideas para llevar a cabo sus objetivos, posee gran confianza en sí misma y nunca se da por vencida. Valora su trabajo como necesario e importante.
                                        </p>
                                    </td>
                                    <td style="text-align: center">
                                        <telerik:RadButton RenderMode="Lightweight" ID="btnSeguridadSegura" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Seguridad"
                                            Text="" Skin="Metro" Value="4">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpvEnfoque" runat="server" Height="100%">
                        <div style="padding: 5px">
                            <div style="border: 1px solid #ddd">
                                <p>
                                    <strong>Definición del factor</strong><br />
                                    Capacidad para actuar en base al logro de resultados, fijando metas desafíantes por encima de los estándares, mejorando y manteniendo altos niveles de rendimiento, en el marco de las estrategias personales y de la institución para la que colabore.
                                </p>
                            </div>
                            <div style="clear: both; height: 20px"></div>
                            <div>
                                <label>Por favor seleccione uno de los siguientes textos para la prueba:</label>
                            </div>
                            <div style="clear: both; height: 20px"></div>
                            <table width="100%" style="border: 1px solid #ddd">
                                <colgroup>
                                    <col width="90%" />
                                    <col width="10%" />
                                </colgroup>
                                <tr>
                                    <td>
                                        <p>
                                            La persona muestra una actitud de "niño" esperando que se le diga que hacer. Intenta realizar bien o correctamente el trabajo, pero expresa frustración y si las cosas no salen se excusa, busca culpables y flojea. No tiene objetivos personales.
                                        </p>
                                    </td>
                                    <td style="text-align: center">
                                        <telerik:RadButton RenderMode="Lightweight" ID="btnEnfoque1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Enfoque"
                                            Text="" Skin="Metro" Value="1">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p>
                                            La persona trabaja para alcanzar los estánderes definidos por sus superiores, en los tiempos previstos y con los recursos que se les asignan. Sólo en algunas ocasiones logra actuar de manera eficiente frente a los obstaculos o imprevistos. Tiene objetivos personales pero no son claros, ni estipula tiempo para su realización.
                                        </p>
                                    </td>
                                    <td style="text-align: center">
                                        <telerik:RadButton RenderMode="Lightweight" ID="btnEnfoque2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Enfoque"
                                            Text="" Skin="Metro" Value="2">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p>
                                            La persona muestra un entendimiento claro de sus objetivos personales y laborales. Logra resultados y también ayuda a otros a logarlos. Entiende el valor que aporta, la necesidad de las personas que se benefician de lo que él hace y toma medidas para brindarles satisfacción y se mantiene productivo. Apoya a otros bajo la premisa que todo lo que sume es valido y necesario. 
                                        </p>
                                    </td>
                                    <td style="text-align: center">
                                        <telerik:RadButton RenderMode="Lightweight" ID="btnEnfoque3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Enfoque"
                                            Text="" Skin="Metro" Value="3">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p>
                                            La persona muestra una actitud "adulta" que hace que mejores resultados  sucedan en su vida y para la institución en la que trabaje. Sale de su zona comoda para ir siempre un paso más adelante en el camino de los objetivos fijados. Contribuye con otras áreas en el alineamiento de sus objetivos a los definidos por la institución. Se preocupa por el resultado  de otros. Aporta ideas novedosas y soluciones incluso frente a problemas complejos y escenarios cambiantes, aporta soluciones de alto valor agregado para su persona y la institución en la que labore.
                                        </p>
                                    </td>
                                    <td style="text-align: center">
                                        <telerik:RadButton RenderMode="Lightweight" ID="btnEnfoque4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Enfoque"
                                            Text="" Skin="Metro" Value="4">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpvConflicto" runat="server" Height="100%">
                        <div style="padding: 5px">
                            <div style="border: 1px solid #ddd">
                                <p>
                                    <strong>Definición del factor</strong><br />
                                    Situación en la que dos o más partes creen que lo quiere una parte es incompatible con lo que desea la otra.
                                </p>
                            </div>
                            <div style="clear: both; height: 20px"></div>
                            <div>
                                <label>Por favor seleccione uno de los siguientes textos para la prueba:</label>
                            </div>
                            <div style="clear: both; height: 20px"></div>
                            <table width="100%" style="border: 1px solid #ddd">
                                <colgroup>
                                    <col width="90%" />
                                    <col width="10%" />
                                </colgroup>
                                <tr>
                                    <td>
                                        <p>
                                            La persona muestra subjetividad en la percepción de las cosas. Es pretensiosa y no acepta a otros sin juzgarlos, crea problemas en ella misma y con los demás. Depende tanto de otras personas o las hace depender de ellas, que terminan por estorbarse entre sí, derivando grandes conflictos, caos y una lucha interna. Expresa sentimientos y pensamientos en forma agresiva, deshonesta, inapropiada e inoportuna. Defiende lo suyo pero sin respetar a los demás.  Ante el conflicto muestra frustración,impotencia, enojo, hostilidad, pierde el control y explota.
                                        </p>
                                    </td>
                                    <td style="text-align: center">
                                        <telerik:RadButton RenderMode="Lightweight" ID="btnConflicto1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Conflicto"
                                            Text="" Skin="Metro" Value="1">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p>
                                            La persona muestra subjetividad en la percepción de las cosas. La información que maneja es incompleta, opina y solo conoce una parte de los hechos. Su comunicación es distorsionada al descifrar el mensaje. Se muestra lenta, idealista, desordenada e intransigente. Busca dominar y forzar a la otra persona persona a perder. Ante el conflicto muestra ansiedad, opresión, preocupación, dolor de cabeza, baja la cabeza y queda inhibida y bloqueada. 
                                        </p>
                                    </td>
                                    <td style="text-align: center">
                                        <telerik:RadButton RenderMode="Lightweight" ID="btnConflicto2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Conflicto"
                                            Text="" Skin="Metro" Value="2">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p>
                                            La persona muestra objetividad en la percepción de las cosas. La información que maneja es completa y opina solo cuando conoce los hechos. Su comunicación es abierta y objetiva al descifrar el mensaje. Se muestra rápida, realista, ordenada y tolerante. Ante el conflicto reconoce y defiende sus derechos, expresa sus emociones en forma directa y respetuosa, honesta, oportuna y apropiadamente. Controla sus emociones.
                                        </p>
                                    </td>
                                    <td style="text-align: center">
                                        <telerik:RadButton RenderMode="Lightweight" ID="btnConflicto3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Conflicto"
                                            Text="" Skin="Metro" Value="3">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p>
                                            La persona muestra objetividad en la percepción de las cosas. Conoce y maneja sus propios sentimientos, interpreta y enfrenta los sentimientos de los demás. Muestra satisfacción y tiene hábitos mentales que favorecen su productividad. Sabe controlar su impulsividad, da y recibe respeto. Ante el conflicto deja un espacio entre ella y el conflicto; guarda silencio, no muestra reacciones ante lo que el otro dice, parafrasea a la otra persona " a ver si le entiendo, lo que usted quiere decir", busca ser proactivo y logra un entendimiento.
                                        </p>
                                    </td>
                                    <td style="text-align: center">
                                        <telerik:RadButton RenderMode="Lightweight" ID="btnConflicto4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Conflicto"
                                            Text="" Skin="Metro" Value="4">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpvCarisma" runat="server" Height="100%">
                        <div style="padding: 5px">
                            <div style="border: 1px solid #ddd">
                                <p>
                                    <strong>Definición del factor</strong><br />
                                    Habilidad para influír o atraer a otras personas. Se refiere a la cualidad de ciertas personas de motivar con facilidad la atención y la admiración de otros gracias a una cualidad "magnetica" de personalidad o de apariencia.
                                </p>
                            </div>
                            <div style="clear: both; height: 20px"></div>
                            <div>
                                <label>Por favor seleccione uno de los siguientes textos para la prueba:</label>
                            </div>
                            <div style="clear: both; height: 20px"></div>
                            <table width="100%" style="border: 1px solid #ddd">
                                <colgroup>
                                    <col width="90%" />
                                    <col width="10%" />
                                </colgroup>
                                <tr>
                                    <td>
                                        <p>
                                            La persona provoca sentimientos de rechazo o ánimo adverso. Muestra desinterés por la persona que tiene al frente, mira al techo, a las manos o a un objeto mientras conversa.  Frunce el seño o la boca. Da la espalda al momento de terminar con la coversación.
                                        </p>
                                    </td>
                                    <td style="text-align: center">
                                        <telerik:RadButton RenderMode="Lightweight" ID="btnCarisma1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Carisma"
                                            Text="" Skin="Metro" Value="1">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p>
                                            La persona mantiene una sonrisa tenue y permanente. Cruza los brazos y comete ciertas fallas de modales de forma liberada <b>y…… va solo a lo suyo</b>. No esta comoda, es poco accesible, suele ser tosca al expresarse, da respuestas cortantes y se muestra un tanto arrogante.
                                        </p>
                                    </td>
                                    <td style="text-align: center">
                                        <telerik:RadButton RenderMode="Lightweight" ID="btnCarisma2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Carisma"
                                            Text="" Skin="Metro" Value="2">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p>
                                            La persona muestra una imagen limpia, saludable, postura corporal abierta, manos lejos de la cara cuando esta hablando. Se mantiene derecha, relajada, manos apartadas con las palmas adelante o hacia arriba. Hace saber a la persona que tiene frente que ella importa y que disfruta estando con ella. Desarrolla una sonrisa genuina, asiente con la cabeza cuando éstas hablan. Da impresión de ser gente de confianza, amable, espiritual y alegra a los demás.
                                        </p>
                                    </td>
                                    <td style="text-align: center">
                                        <telerik:RadButton RenderMode="Lightweight" ID="btnCarisma3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Carisma"
                                            Text="" Skin="Metro" Value="3">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p>
                                            La persona transmite <b>con su sola postura su seguridad, la fuerza interna y su actitud</b>. Sabe como vestir de forma elegante y de acuerdo a la ocasión. Va más allá del statu quo, marca la diferencia, es controvertido, novedoso, elocuente e intuitivo. Es líder, se mueve alrededor de un grupo o espacio para parecer entusiasta, se inclina levemente hacia delante y mira para todos las partes del grupo o personas que le rodean. Su mensaje es claro, fluído, enérgetico y articulado. Es optimista, todo lo que dice o hace tiene un propósito y es una persona éxitosa.
                                        </p>
                                    </td>
                                    <td style="text-align: center">
                                        <telerik:RadButton RenderMode="Lightweight" ID="btnCarisma4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                            AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Carisma"
                                            Text="" Skin="Metro" Value="4">
                                        </telerik:RadButton>
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
    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnTerminar" runat="server" OnClientClicking="close_window" OnClick="btnTerminar_Click" AutoPostBack="true"></telerik:RadButton>
        </div>
        <%--                     <div class="ctrlBasico">
                  <telerik:RadButton ID="btnEliminar" runat="server"  Text="Eliminar" AutoPostBack="true" Visible="false" OnClientClicking="ConfirmarEliminarRespuestas" OnClick="btnEliminar_Click"></telerik:RadButton>
             </div>--%>
    </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
