<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaIntegracionMedioManual.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaIntegracionMedioManual" %>

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
        @media print {
      body, html, #wrapper {
          width: 100%;
      }
    </style>

<script type="text/javascript">

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="splHelp" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">
            //CODIGO JS
            var vFgEnValidacion = false;

            function validarSerie(sender, args) {

                if (!vFgEnValidacion) {
                    vFgEnValidacion = true;
                    var vNewValue = sender._displayText;
                    var vId = sender.get_id();
                    var vClassName = sender._textBoxElement.className;//document.getElementById(vId).className;
                    var vtxtSelected = document.getElementById(vId);
                    var res = vClassName.replace("riEnabled", "").replace("riHover", "");
                    var x = document.getElementsByClassName(res);
                    if (vtxtSelected.value != "") {
                        var i = 0;
                        for (i = 0; i < x.length; i++) {
                            //if ((x[i].value == vNewValue && x[i].id != vId) || vtxtSelected.value == "0" || vtxtSelected.value > 7) {
                            if ((x[i].value == vNewValue && x[i].id != vId)  || vtxtSelected.value > 7) {
                                vtxtSelected.focus();
                                vtxtSelected.style.borderColor = 'red';
                                vtxtSelected.style.borderWidth = '1px';
                                vtxtSelected.value = "";
                                break;
                            }
                        }
                    }
                    vFgEnValidacion = false;
                }

            }

            function CloseTest() {
                GetRadWindow().close();

            }

        </script>
    </telerik:RadCodeBlock>

    <label name="" id="lbltitulo" class="labelTitulo">Adaptación al medio</label>

    <div style="height: calc(100% - 100px);">

        <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Horizontal">
       <%--     <telerik:RadPane ID="rpnOpciones" runat="server" Height="30" Width="100%" Scrolling="None">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="RSPHelp" runat="server" Title="Instrucciones" Width="100%" Height="100">
                        <p style="margin: 10px;">
                            Ingresa el orden en que se seleccionaron las tarjetas en cada serie, iniciando desde 0 hasta 7.
                        </p>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>--%>

            <telerik:RadPane ID="rpnPrueba" runat="server">
                
                <div>
                    <%--<div class="ctrlBasico" id="divLabelMensaje" style="padding-left: 5%;">
                        <label id="lblmensaje" name="" >Por favor ingrese los datos para la prueba:</label>
                    </div>--%>

                    <div style="clear:both;height:30px"></div>

                    <div class="ctrlBasico"  style="padding-left: 5%;">
                        <div>
                            <label>Serie 1:</label>
                            <div style="clear:both"></div>
                            <div class="ctrlBasico">
                                <%--<telerik:RadTextBox runat="server" ID="txtserie1_1" InputType="Number" Width="62px" />--%>
                                <telerik:RadNumericTextBox ID="txtserie_1_1" CssClass="pregunta1 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarSerie" MaxLength="1" MaxValue="7" MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="62px"></telerik:RadNumericTextBox>
                            </div>
                            <div class="ctrlBasico">
                                <%--<telerik:RadTextBox runat="server" ID="txtserie1_2" InputType="Number" Width="62px" />--%>
                                <telerik:RadNumericTextBox ID="txtserie_1_2" CssClass="pregunta1 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarSerie" MaxLength="1" MaxValue="7" MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="62px"></telerik:RadNumericTextBox>
                            </div>
                            <div class="ctrlBasico">
                                <%--<telerik:RadTextBox runat="server" ID="txtserie1_3" InputType="Number" Width="62px" />--%>
                                <telerik:RadNumericTextBox ID="txtserie_1_3" CssClass="pregunta1 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarSerie" MaxLength="1" MaxValue="7" MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="62px"></telerik:RadNumericTextBox>
                            </div>
                            <div class="ctrlBasico">
                                <%--<telerik:RadTextBox runat="server" ID="txtserie1_4" InputType="Number" Width="62px" />--%>
                                <telerik:RadNumericTextBox ID="txtserie_1_4" CssClass="pregunta1 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarSerie" MaxLength="1" MaxValue="7" MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="62px"></telerik:RadNumericTextBox>
                            </div>
                            <div class="ctrlBasico">
                                <%--<telerik:RadTextBox runat="server" ID="txtserie1_5" InputType="Number" Width="62px" />--%>
                                <telerik:RadNumericTextBox ID="txtserie_1_5" CssClass="pregunta1 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarSerie" MaxLength="1" MaxValue="7" MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="62px"></telerik:RadNumericTextBox>
                            </div>
                            <div class="ctrlBasico">
                                <%--<telerik:RadTextBox runat="server" ID="txtserie1_6" InputType="Number" Width="62px" />--%>
                                <telerik:RadNumericTextBox ID="txtserie_1_6" CssClass="pregunta1 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarSerie" MaxLength="1" MaxValue="7" MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="62px"></telerik:RadNumericTextBox>
                            </div>
                            <div class="ctrlBasico">
                                <%--<telerik:RadTextBox runat="server" ID="txtserie1_7" InputType="Number" Width="62px" />--%>
                                <telerik:RadNumericTextBox ID="txtserie_1_7" CssClass="pregunta1 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarSerie" MaxLength="1" MaxValue="7" MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="62px"></telerik:RadNumericTextBox>
                            </div>
                            <div class="ctrlBasico">
                                <%--<telerik:RadTextBox runat="server" ID="txtserie1_8" InputType="Number" Width="62px" />--%>
                                <telerik:RadNumericTextBox ID="txtserie_1_8" CssClass="pregunta1 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarSerie" MaxLength="1" MaxValue="7" MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="62px"></telerik:RadNumericTextBox>
                            </div>
                            <div style="clear:both"></div>
                        </div>
                    </div>
                    <div style="clear:both;height:30px"></div>
                    <div class="ctrlBasico"  style="padding-left: 5%;">
                        <div>
                            <label>Serie 2:</label>
                            <div style="clear:both"></div>
                            <div class="ctrlBasico">
                                <%--<telerik:RadTextBox runat="server" ID="txtserie2_1" InputType="Number" Width="62px" />--%>
                                <telerik:RadNumericTextBox ID="txtserie_2_1" CssClass="pregunta2 Contenedor"  AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarSerie" MaxLength="1" MaxValue="7" MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="62px"></telerik:RadNumericTextBox>
                            </div>
                            <div class="ctrlBasico">
                                <%--<telerik:RadTextBox runat="server" ID="txtserie2_2" InputType="Number" Width="62px" />--%>
                                <telerik:RadNumericTextBox ID="txtserie_2_2" CssClass="pregunta2 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarSerie" MaxLength="1" MaxValue="7" MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="62px"></telerik:RadNumericTextBox>
                            </div>
                            <div class="ctrlBasico">
                                <%--<telerik:RadTextBox runat="server" ID="txtserie2_3" InputType="Number" Width="62px" />--%>
                                <telerik:RadNumericTextBox ID="txtserie_2_3" CssClass="pregunta2 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarSerie" MaxLength="1" MaxValue="7" MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="62px"></telerik:RadNumericTextBox>
                            </div>
                            <div class="ctrlBasico">
                                <%--<telerik:RadTextBox runat="server" ID="txtserie2_4" InputType="Number" Width="62px" />--%>
                                <telerik:RadNumericTextBox ID="txtserie_2_4" CssClass="pregunta2 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarSerie" MaxLength="1" MaxValue="7" MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="62px"></telerik:RadNumericTextBox>
                            </div>
                            <div class="ctrlBasico">
                                <%--<telerik:RadTextBox runat="server" ID="txtserie2_5" InputType="Number" Width="62px" />--%>
                                <telerik:RadNumericTextBox ID="txtserie_2_5" CssClass="pregunta2 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarSerie" MaxLength="1" MaxValue="7" MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="62px"></telerik:RadNumericTextBox>
                            </div>
                            <div class="ctrlBasico">
                                <%--<telerik:RadTextBox runat="server" ID="txtserie2_6" InputType="Number" Width="62px" />--%>
                                <telerik:RadNumericTextBox ID="txtserie_2_6" CssClass="pregunta2 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarSerie" MaxLength="1" MaxValue="7" MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="62px"></telerik:RadNumericTextBox>
                            </div>
                            <div class="ctrlBasico">
                                <%--<telerik:RadTextBox runat="server" ID="txtserie2_7" InputType="Number" Width="62px" />--%>
                                <telerik:RadNumericTextBox ID="txtserie_2_7" CssClass="pregunta2 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarSerie" MaxLength="1" MaxValue="7" MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="62px"></telerik:RadNumericTextBox>
                            </div>
                            <div class="ctrlBasico">
                                <%--<telerik:RadTextBox runat="server" ID="txtserie2_8" InputType="Number" Width="62px" />--%>
                                <telerik:RadNumericTextBox ID="txtserie_2_8" CssClass="pregunta2 Contenedor" AllowOutOfRangeAutoCorrect="false" ClientEvents-OnBlur="validarSerie" MaxLength="1" MaxValue="7" MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-DecimalSeparator=" " runat="server" Width="62px"></telerik:RadNumericTextBox>
                            </div>
                            <div style="clear:both"></div>
                        </div>
                    </div>
                    <div style="clear:both"></div>
                </div>   

            </telerik:RadPane>

            </telerik:RadSplitter>
    </div>

    <div style="clear: both; height: 10px;"></div>
    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnTerminar" runat="server" Text="Guardar" AutoPostBack="true" OnClick="btnTerminar_Click" UseSubmitBehavior="false"></telerik:RadButton>
        </div>
         <div class="ctrlBasico">
             <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" AutoPostBack="true" OnClientClicking="ConfirmarEliminarPrueba" OnClick="btnEliminar_Click" Visible="true"></telerik:RadButton>
         </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnEliminarBateria" runat="server" Text="Eliminar batería" AutoPostBack="true" OnClientClicking="ConfirmarEliminarRespuestas" OnClick="btnEliminarBateria_Click" Visible="true"></telerik:RadButton>
         </div>
    </div>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
