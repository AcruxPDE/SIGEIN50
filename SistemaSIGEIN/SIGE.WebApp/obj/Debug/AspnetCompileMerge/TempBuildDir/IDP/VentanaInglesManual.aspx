<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaInglesManual.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaInglesManual" %>
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
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">            

            function CloseTest() {
                //window.close();
                GetRadWindow().close();
            }
           
        </script>
    </telerik:RadCodeBlock>

    <label name="" id="lbltitulo" class="labelTitulo">Inglés</label>

    <div style="height: calc(100% - 100px);">

        <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Horizontal">
     <%--       <telerik:RadPane ID="rpnOpciones" runat="server" Height="30" Width="100%" Scrolling="None">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="RSPHelp" runat="server" Title="Instrucciones" Width="100%" Height="240">
                        <p style="margin: 10px;">
                           Captura el número de aciertos obtenidos en cada una de las etapas de la prueba Inglés
                        </p>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>--%>

            <telerik:RadPane ID="rpnPrueba" runat="server">

                <div style="padding:5px">
                    
                    <label>Por favor ingrese los datos para la prueba:</label>
                    <div style="clear:both;height:10px"></div>

                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label>Sección 1:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadNumericTextBox runat="server" Width="62px" ID="txtRespuesta1" MinValue="0" MaxValue="30" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div style="clear:both;height:20px"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label>Sección 2:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadNumericTextBox runat="server" Width="62px" ID="txtRespuesta2" MinValue="0" MaxValue="30" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div style="clear:both;height:20px"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label>Sección 3:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadNumericTextBox runat="server" Width="62px" ID="txtRespuesta3" MinValue="0" MaxValue="30" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div style="clear:both;height:20px"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label>Sección 4:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadNumericTextBox runat="server" Width="62px" ID="txtRespuesta4" MinValue="0" MaxValue="30" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                        </div>
                    </div>

                    <div style="clear:both;height:20px"></div>
                    <label>Captura el número de aciertos obtenidos en cada una de las etapas de la prueba de inglés</label>

                </div>

            </telerik:RadPane>

        </telerik:RadSplitter>
    </div>

    <div style="clear: both; height: 10px;"></div>
    <div class="DivBtnTerminarDerecha ">
        <telerik:RadButton ID="btnTerminar" runat="server" Text="Terminar" AutoPostBack="true" OnClick="btnTerminar_Click"></telerik:RadButton>
    </div>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
