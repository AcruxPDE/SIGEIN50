<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaRedaccionManual.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaRedacionManual" %>

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
            padding:5px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">
            //CODIGO JS
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
                    radconfirm(text, callBackFunction, 400, 160, null, "");
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
                var oWnd = radalert("Usted ha terminado su prueba exitosamente o el tiempo de aplicación de la prueba ha concluido. <br> Cuando esté listo para pasar a la siguiente prueba, por favor haga clic en el botón 'Siguiente' más abajo <br>Recuerde que no es posible volver a ingresar la prueba previa; si intenta hacerlo por medio del botón del navegador, la aplicación no te lo permitirá: se generará un error y el intento quedará registrado", 400, 300, "");
                oWnd.add_close(WinClose);
            }

            function CloseTest() {
                GetRadWindow().close();
            }


            function GetRadWindow() {
                var oWindow = null;
                if 
                (window.radWindow) oWindow = window.radWindow;
                else
                    if (window.frameElement.radWindow)
                        oWindow = window.frameElement.radWindow;
                return oWindow;
            }

            function closeWindow() {
                GetRadWindow().close();
            }
        </script>
    </telerik:RadCodeBlock>
         <label name="" id="lbltitulo" class="labelTitulo">Redacción</label>
    <div style="height: calc(100% - 60px); padding:10px;">
        <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Horizontal">
            <telerik:RadPane ID="rpnPrueba" runat="server">
                    <div style="border: 1px solid #ddd">
                            <div style="font-weight:bold;">Definición del factor</div>
                             <div class="JustificarTexto">
                            Capacidad para expresar de manera escrita ideas o pensamientos que se expresan en una forma adecuada gramaticalmente, que cumplan con las reglas ortográficas y que tengan un adecuado manejo del lenguaje.
                        </div>
                    </div>
                    <div style="clear: both; height: 10px"></div>

                    <div>
                        <label>Por favor seleccione uno de los siguientes textos para la prueba:</label>
                    </div>

                    <div style="clear: both; height: 10px"></div>

                    <table width="100%" style="border:1px solid #ddd">
                        <colgroup>
                            <col  width="90%" />
                            <col  width="10%" />
                        </colgroup>
                        <tr>
                            <td>
                                     <div class="JustificarTexto">
                                    La persona no tiene la capacidad para comunicar de manera ordenada e inteligible sus ideas. Es difícil entender el objetivo de su comunicación, presenta errores ortográficos y gramaticales. Carece de los medios necesarios para expresarse por escrito de manera correcta.
                                      </div>
                            </td>
                            <td style="text-align:center">
                               
                                    <telerik:RadButton RenderMode="Lightweight" ID="rbrespuesta1" runat="server" ButtonType="ToggleButton"
                                        ToggleType="Radio" GroupName="Radio" Text="" AutoPostBack="false">
                                    </telerik:RadButton>
                               <%-- <telerik:RadButton RenderMode="Lightweight" ID="rbrespuesta1" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Radio"
                                    Text=""  Skin="Metro">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState PrimaryIconHeight="0" PrimaryIconWidth="0" SecondaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState PrimaryIconHeight="0" PrimaryIconWidth="0" SecondaryIconCssClass="rbToggleRadio"
                                            SecondaryIconRight="0"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>--%>
            
                            </td>
                        </tr>

                        <tr>
                            <td>
                                     <div class="JustificarTexto">
                                    La persona muestra dificultad para comunicar adecuadamente sus ideas, sin embargo cuando se trata de ideas sencillas puede lograr comunicarlas razonablemente bien. Su manejo de las reglas ortográficas, gramaticales y de puntuación es regular. El uso del lenguaje es pobre y no siempre utiliza las palabras adecuadas para expresarse de manera precisa.
                                      </div>
                            </td>
                            <td style="text-align:center">

                                 <telerik:RadButton RenderMode="Lightweight" ID="rbrespuesta2" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Radio"
                                    Text=""  Skin="Metro">
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
                                     <div class="JustificarTexto">
                                    La persona muestra una capacidad media para comunicar sus ideas, pudiendo hacerlo adecuadamente con un manejo del lenguaje correcto aunque con ciertas limitaciones, sobre todo cuando se trata de expresar ideas complejas. Su ortografía, gramática y puntuación son aceptables.
                                </div>
                                          </p>
                            </td>
                            <td style="text-align:center">

                                <telerik:RadButton RenderMode="Lightweight" ID="rbrespuesta3" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Radio"
                                    Text=""  Skin="Metro">
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
                                     <div class="JustificarTexto">
                                    La persona muestra una buena capacidad para comunicar de manera clara y fluida sus ideas, selecciona adecuadamente las palabras y frases para que expresen el significado correcto. No deja lugar a dudas con respecto a su significado. Hace un uso correcto de la gramática, puntuación y ortografía. Utiliza bien el lenguaje.
                                </div>
                                          </p>
                            </td>
                            <td style="text-align:center">

                                <telerik:RadButton RenderMode="Lightweight" ID="rbrespuesta4" runat="server" ButtonType="ToggleButton" ToggleType="Radio"
                                    AutoPostBack="false" BorderWidth="0" BackColor="transparent" GroupName="Radio"
                                    Text=""  Skin="Metro">
                                    <%--<ToggleStates>
                                        <telerik:RadButtonToggleState PrimaryIconHeight="0" PrimaryIconWidth="0" SecondaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState PrimaryIconHeight="0" PrimaryIconWidth="0" SecondaryIconCssClass="rbToggleRadio"
                                            SecondaryIconRight="0"></telerik:RadButtonToggleState>
                                    </ToggleStates>--%>
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <div style="clear: both;"></div>
    <div class="divControlDerecha" style="padding-right:10px;">
        <telerik:RadButton ID="btnTerminar" runat="server" OnClientClicking="close_window" OnClick="btnTerminar_Click" Text="Guardar" AutoPostBack="true"></telerik:RadButton>
    </div>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
