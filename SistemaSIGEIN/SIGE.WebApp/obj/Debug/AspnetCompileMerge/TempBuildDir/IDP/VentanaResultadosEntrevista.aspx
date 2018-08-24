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


        </script>
    </telerik:RadCodeBlock>

    <label name="" id="lbltitulo" class="labelTitulo">Resultados de la entrevista</label>

    <div style="height: calc(100% - 100px);">

        <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Horizontal">
            <telerik:RadPane ID="rpnOpciones" runat="server" Height="30" Width="100%" Scrolling="None">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="RSPHelp" runat="server" Title="Instrucciones" Width="100%" Height="240">
                        <p style="margin: 10px;">
                            <strong>Instrucciones:</strong>
                           <label id="Label1" runat="server"> Una vez realizada la entrevista de competencias, deberás calificar los siguientes aspectos de la persona evaluada, contesta de manera objetiva y considera las evidencias de desempeño a fin de que la calificación emitida sea lo más precisa posible.</label>
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
                    </Tabs>
                </telerik:RadTabStrip>

                <telerik:RadMultiPage ID="mpgresultados" runat="server" SelectedIndex="0" Height="70%" Width="100%">
                    <telerik:RadPageView ID="rpvcomunicacionverbal" runat="server" Height="100%">

                        <div style="padding: 5px">

                            <div style="border: 1px solid #ddd">
                                <p>
                                    <strong>Definición del factor</strong>
                                    Capacidad para expresar adecuadamente de manera verbal ideas o pensamientos de una manera lógica y concisa con un adecuado manejo del lenguaje
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
                                    <strong>Definición del factor</strong>
                                    Capacidad para comunicarse efectivamente de manera no verbal, utilizando recursos expresivos como gesticulación, postura y ademanes
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
                </telerik:RadMultiPage>


            </telerik:RadPane>




        </telerik:RadSplitter>

    </div>

    <div style="clear: both; height: 10px;"></div>
    <div class="DivBtnTerminarDerecha ">
        <telerik:RadButton ID="btnTerminar" runat="server" OnClientClicking="close_window" OnClick="btnTerminar_Click" Text="Terminar" AutoPostBack="true"></telerik:RadButton>
    </div>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
