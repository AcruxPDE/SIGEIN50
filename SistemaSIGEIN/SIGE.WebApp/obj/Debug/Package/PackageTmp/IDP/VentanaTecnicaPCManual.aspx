<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaTecnicaPCManual.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaTecnicaPCManual" %>

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

    <label name="" id="lbltitulo" class="labelTitulo">Técnica PC</label>

    <div style="height: calc(100% - 100px);">

        <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Horizontal">
        <%--    <telerik:RadPane ID="rpnOpciones" runat="server" Height="30" Width="100%" Scrolling="None">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="RSPHelp" runat="server" Title="Instrucciones" Width="100%" Height="240">
                        <p style="margin: 10px;">
                            <!--  TEXTO  -->
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
                            <label>Comunicación</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadNumericTextBox runat="server" Width="62px" ID="txtrespuesta1" MinValue="0" MaxValue="20" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>                            
                        </div>
                    </div>
                    <div style="clear:both;height:20px"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label>Software</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadNumericTextBox runat="server" Width="62px" ID="txtrespuesta2" MinValue="0" MaxValue="24" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div style="clear:both;height:20px"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label>Internet</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadNumericTextBox runat="server" Width="62px" ID="txtrespuesta3" MinValue="0" MaxValue="16" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div style="clear:both;height:20px"></div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label>Hardware</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadNumericTextBox runat="server" Width="62px" ID="txtrespuesta4" MinValue="0" MaxValue="24" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                        </div>
                    </div>

                    <div style="clear:both;height:20px"></div>
                    <label>Captura el número de aciertos obtenidos en cada una de las pruebas de técnica PC</label>

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
