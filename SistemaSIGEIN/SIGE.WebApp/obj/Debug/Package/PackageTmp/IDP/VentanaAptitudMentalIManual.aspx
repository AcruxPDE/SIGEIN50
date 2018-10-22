<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaAptitudMentalIManual.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaAptitudMentalIManual" %>

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

        tr, td {
            padding: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramActualizaciones" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtapartado1_4">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblapartado1_2" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtapartado2_4">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblapartado2_2" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtapartado3_4">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblapartado3_2" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtapartado4_4">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblapartado4_2" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtapartado5_4">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblapartado5_2" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtapartado6_4">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblapartado6_2" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtapartado7_4">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblapartado7_2" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtapartado8_4">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblapartado8_2" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtapartado9_4">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblapartado9_2" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtapartado10_4">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblapartado10_2" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">

            //function Operacion(sender, args) {
            //    var vId = sender.get_id();
            //    var vAciertos = document.getElementById(vId).value;
            //    var vTope = document.getElementById(vId).getAttribute("data-tope");

            //    if (vAciertos > vTope) {
            //        document.getElementById(vId).value = "0";
            //        radalert("El valor está fuera de rango (debe estar entre 0 y " + vTope + ")", 400, 170, "Prueba");
            //    }
            //}

            var vPruebaEstatus = "";
            function close_window(sender, args) {
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

                function CloseTest() {
                    //window.close();
                    GetRadWindow().close();
                }

        </script>
    </telerik:RadCodeBlock>
    <label name="" id="lbltitulo" class="labelTitulo">Aptitud mental I</label>
    <div style="height: calc(100% - 100px);">
        <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Horizontal">
            <%--<telerik:RadPane ID="rpnOpciones" runat="server" Height="30" Width="100%" Scrolling="None">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="RSPHelp" runat="server" Title="Instrucciones" Width="100%" Height="240">
                        <p style="margin: 10px;">
                        </p>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>--%>

            <telerik:RadPane ID="rpnPrueba" runat="server">
                <table width="100%" style="border: 1px solid #ddd">
                    <tr style="text-align: center; font-weight: bold; background: #ddd">
                        <td>
                            <label id="lbltextocab1" name="lbltextocab1">Test aptitud mental</label>
                        </td>
                        <td>
                            <label id="lbltextocab2" name="lbltextocab2">Porcentaje</label>
                        </td>
                        <td>
                            <label id="lbltextocab3" name="lbltextocab3">Tope</label>
                        </td>
                        <td>
                            <label id="lbltextocab4" name="lbltextocab4">Aciertos</label>
                        </td>
                        <td>
                            <label id="lbltextocab5" name="lbltextocab5">Descripción</label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5"></td>
                    </tr>
                    <!-- 1 fila -->
                    <tr>
                        <td>
                            <label id="lblapartado1_1" name="lblapartado1_1">Conocimientos</label>
                        </td>
                        <td style="text-align: center">
                            <label id="lblapartado1_2" runat="server" name="lblapartado1_2" >0.00</label>
                        </td>
                        <td style="text-align: center">
                            <label id="lblapartado1_3" name="lblapartado1_3">16</label>
                        </td>
                        <td style="text-align: center">
                            <telerik:RadNumericTextBox AutoPostBack="true" runat="server" ID="txtapartado1_4" Width="62px"  MinValue="0" MaxValue="16" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" >
                            </telerik:RadNumericTextBox>
                            <%--<telerik:RadTextBox runat="server" InputType="Number" data-tope="16" ID="txtapartado1_4" Width="62px" ClientEvents-OnBlur="Operacion" />--%>
                        </td>
                        <td>
                            <label id="lblapartado1_5" name="lblapartado1_5">Cultura General</label>
                        </td>
                    </tr>
                    <!-- 2 fila -->
                    <tr>
                        <td>
                            <label id="lblapartado2_1" name="lblapartado2_1">Comprensión</label>
                        </td>
                        <td style="text-align: center">
                            <label id="lblapartado2_2" runat="server" name="lblapartado2_2">0.00</label>
                        </td>
                        <td style="text-align: center">
                            <label id="lblapartado2_3" name="lblapartado2_3">22</label>
                        </td>
                        <td style="text-align: center">
                            <telerik:RadNumericTextBox AutoPostBack="true" runat="server" ID="txtapartado2_4" Width="62px"  MinValue="0" MaxValue="22" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" >
                            </telerik:RadNumericTextBox>
                            
                            <%--<telerik:RadNumericTextBox runat="server"   data-tope="22" ID="txtapartado2_4" Width="62px" ClientEvents-OnBlur="Operacion" />--%>
                        </td>
                        <td>
                            <label id="lblapartado2_5" name="lblapartado2_5">Capacidad de juicio</label>
                        </td>
                    </tr>

                    <!-- 3 fila -->
                    <tr>
                        <td>
                            <label id="lblapartado3_1" name="lblapartado3_1">Significados verbales</label>
                        </td>
                        <td style="text-align: center">
                            <label id="lblapartado3_2" runat="server" name="lblapartado3_2">0.00</label>
                        </td>
                        <td style="text-align: center">
                            <label id="lblapartado3_3" name="lblapartado3_3">30</label>
                        </td>
                        <td style="text-align: center">
                             <telerik:RadNumericTextBox AutoPostBack="true" runat="server" ID="txtapartado3_4" Width="62px"  MinValue="0" MaxValue="30" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" >
                            </telerik:RadNumericTextBox>
                            <%--<telerik:RadTextBox runat="server" InputType="Number" data-tope="30" ID="txtapartado3_4" Width="62px" ClientEvents-OnBlur="Operacion" />--%>
                        </td>
                        <td>
                            <label id="lblapartado3_5" name="lblapartado3_5">Capacidad de análisis y síntesis</label>
                        </td>
                    </tr>

                    <!-- 4 fila -->
                    <tr>
                        <td>
                            <label id="lblapartado4_1" name="lblapartado4_1">Selección lógica</label>
                        </td>
                        <td style="text-align: center">
                            <label id="lblapartado4_2" runat="server" name="lblapartado4_2">0.00</label>
                        </td>
                        <td style="text-align: center">
                            <label id="lblapartado4_3" name="lblapartado4_3">18</label>
                        </td>
                        <td style="text-align: center">
                             <telerik:RadNumericTextBox AutoPostBack="true" runat="server" ID="txtapartado4_4" Width="62px"  MinValue="0" MaxValue="18" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" >
                            </telerik:RadNumericTextBox>
                            <%--<telerik:RadTextBox runat="server" InputType="Number" data-tope="18" ID="txtapartado4_4" Width="62px" ClientEvents-OnBlur="Operacion" />--%>
                        </td>
                        <td>
                            <label id="lblapartado4_5" name="lblapartado4_5">Capacidad de abstracción</label>
                        </td>
                    </tr>


                    <!-- 5 fila -->
                    <tr>
                        <td>
                            <label id="lblapartado5_1" name="lblapartado5_1">Aritmética</label>
                        </td>
                        <td style="text-align: center">
                            <label id="lblapartado5_2" runat="server" name="lblapartado5_2">0.00</label>
                        </td>
                        <td style="text-align: center">
                            <label id="lblapartado5_3" name="lblapartado5_3">24</label>
                        </td>
                        <td style="text-align: center">
                            <telerik:RadNumericTextBox AutoPostBack="true" runat="server" ID="txtapartado5_4" Width="62px"  MinValue="0" MaxValue="24" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" >
                            </telerik:RadNumericTextBox>
                            <%--<telerik:RadTextBox runat="server" InputType="Number" data-tope="24" ID="txtapartado5_4" Width="62px" ClientEvents-OnBlur="Operacion" />--%>
                        </td>
                        <td>
                            <label id="lblapartado5_5" name="lblapartado5_5">Capacidad de razonamiento</label>
                        </td>
                    </tr>


                    <!-- 6 fila -->
                    <tr>
                        <td>
                            <label id="lblapartado6_1" name="lblapartado6_1">Juicio práctico</label>
                        </td>
                        <td style="text-align: center">
                            <label id="lblapartado6_2" runat="server" name="lblapartado6_2">0.00</label>
                        </td>
                        <td style="text-align: center">
                            <label id="lblapartado6_3" name="lblapartado6_3">20</label>
                        </td>
                        <td style="text-align: center">
                             <telerik:RadNumericTextBox AutoPostBack="true" runat="server" ID="txtapartado6_4" Width="62px"  MinValue="0" MaxValue="20" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" >
                            </telerik:RadNumericTextBox>
                            <%--<telerik:RadTextBox runat="server" InputType="Number" data-tope="20" ID="txtapartado6_4" Width="62px" ClientEvents-OnBlur="Operacion" />--%>
                        </td>
                        <td>
                            <label id="lblapartado6_5" name="lblapartado6_5">Sentido común</label>
                        </td>
                    </tr>


                    <!-- 7 fila -->
                    <tr>
                        <td>
                            <label id="lblapartado7_1" name="lblapartado7_1">Analogías</label>
                        </td>
                        <td style="text-align: center">
                            <label id="lblapartado7_2" runat="server" name="lblapartado7_2">0.00</label>
                        </td>
                        <td style="text-align: center">
                            <label id="lblapartado7_3" name="lblapartado7_3">20</label>
                        </td>
                        <td style="text-align: center">
                              <telerik:RadNumericTextBox AutoPostBack="true" runat="server" ID="txtapartado7_4" Width="62px"  MinValue="0" MaxValue="20" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" >
                            </telerik:RadNumericTextBox>
                            <%--<telerik:RadTextBox runat="server" InputType="Number" data-tope="20" ID="txtapartado7_4" Width="62px" ClientEvents-OnBlur="Operacion" />--%>
                        </td>
                        <td>
                            <label id="lblapartado7_5" name="lblapartado7_5">Pensamiento organizado</label>
                        </td>
                    </tr>

                    <!-- 8 fila -->
                    <tr>
                        <td>
                            <label id="lblapartado8_1" name="lblapartado8_1">Ordenamiento de frases</label>
                        </td>
                        <td style="text-align: center">
                            <label id="lblapartado8_2" runat="server" name="lblapartado8_2">0.00</label>
                        </td>
                        <td style="text-align: center">
                            <label id="lblapartado8_3" name="lblapartado8_3">17</label>
                        </td>
                        <td style="text-align: center">
                            <telerik:RadNumericTextBox AutoPostBack="true" runat="server" ID="txtapartado8_4" Width="62px"  MinValue="0" MaxValue="17" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" >
                            </telerik:RadNumericTextBox>
                            <%--<telerik:RadTextBox runat="server" InputType="Number" data-tope="17" ID="txtapartado8_4" Width="62px" ClientEvents-OnBlur="Operacion" />--%>
                        </td>
                        <td>
                            <label id="lblapartado8_5" name="lblapartado8_5">Capacidad de planeación</label>
                        </td>
                    </tr>

                    <!-- 9 fila -->
                    <tr>
                        <td>
                            <label id="lblapartado9_1" name="lblapartado9_1">Clasificación</label>
                        </td>
                        <td style="text-align: center">
                            <label id="lblapartado9_2" runat="server" name="lblapartado9_2">0.00</label>
                        </td>
                        <td style="text-align: center">
                            <label id="lblapartado9_3" name="lblapartado9_3">18</label>
                        </td>
                        <td style="text-align: center">
                             <telerik:RadNumericTextBox AutoPostBack="true" runat="server" ID="txtapartado9_4" Width="62px"  MinValue="0" MaxValue="18" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" >
                            </telerik:RadNumericTextBox>
                            <%--<telerik:RadTextBox runat="server" InputType="Number" data-tope="18" ID="txtapartado9_4" Width="62px" ClientEvents-OnBlur="Operacion" />--%>
                        </td>
                        <td>
                            <label id="lblapartado9_5" name="lblapartado9_5">Capacidad de discriminación</label>
                        </td>
                    </tr>

                    <!-- 10 fila -->
                    <tr>
                        <td>
                            <label id="lblapartado10_1" name="lblapartado10_1">Seriación</label>
                        </td>
                        <td style="text-align: center">
                            <label id="lblapartado10_2" runat="server" name="lblapartado10_2">0.00</label>
                        </td>
                        <td style="text-align: center">
                            <label id="lblapartado10_3" name="lblapartado10_3">22</label>
                        </td>
                        <td style="text-align: center">
                               <telerik:RadNumericTextBox AutoPostBack="true" runat="server" ID="txtapartado10_4" Width="62px"  MinValue="0" MaxValue="22" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" >
                            </telerik:RadNumericTextBox>
                            <%--<telerik:RadTextBox runat="server" InputType="Number" data-tope="22" ID="txtapartado10_4" Width="62px" ClientEvents-OnBlur="Operacion" />--%>
                        </td>
                        <td>
                            <label id="lblapartado10_5" name="lblapartado10_5">Capacidad de dedución</label>
                        </td>
                    </tr>


                </table>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>

    <div style="clear: both; height: 10px;"></div>
    <div class="DivBtnTerminarDerecha ">
        <telerik:RadButton ID="btnTerminar" runat="server" OnClientClicking="close_window" OnClick="btnTerminar_Click" Text="Guardar" AutoPostBack="true"></telerik:RadButton>
    </div>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
