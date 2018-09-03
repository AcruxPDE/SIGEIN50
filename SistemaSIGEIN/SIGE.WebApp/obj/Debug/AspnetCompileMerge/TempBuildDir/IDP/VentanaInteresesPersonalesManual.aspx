<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaInteresesPersonalesManual.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaInteresesPersonalesManual" %>
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

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">

        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnTerminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>


    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">

            var vPruebaEstatus = "";
            var vFgEnValidacion = false;
            var a = new Array();

            function close_window(sender, args) {
                if (vPruebaEstatus != "Terminado") {
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
            }

            function validarCampos(sender, args) {
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

                            if ((x[i].value == vNewValue && x[i].id != vId) || vtxtSelected.value == "0" || vtxtSelected.value > 6) {
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

            //function validarCampos(sender, args) {
            //    var vOldValue = args.get_oldValue();
            //    var vNewValue = args.get_newValue();

            //    var vId = sender.get_id();
            //    var vClassName = document.getElementById(vId).className;
            //    var x = document.getElementsByClassName(vClassName);

            //    var i = 0;
            //    for (i = 0; i < x.length; i++) {
            //        if (x[i].value == vNewValue) {
            //            x[i].value = vOldValue;
            //            break;
            //        }
            //        else {
            //        }
            //    }
            //}

            function CloseTest() {
                GetRadWindow().close();
                //window.close();
            }

        </script>
    </telerik:RadCodeBlock>
     <label name="" id="lbltitulo" class="labelTitulo">Intereses personales</label>

    <div style="height: calc(100% - 120px);">
        <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Horizontal">

         <%-- <telerik:RadPane ID="rpnOpciones" runat="server" Height="30" Width="100%" Scrolling="None">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="RSPHelp" runat="server" Title="Ayuda" Width="100%" Height="240">
                        <p style="margin: 10px;">																													
                        </p>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>--%>


            <telerik:RadPane ID="rpnGridSolicitudes" runat="server">
                <table style="width: 90%; margin-left: 5%; margin-right: 5%;">
                    <thead>
                        <tr>
                            <td width="100%"></td>

                        </tr>
                    </thead>
                    <tbody>

                        <tr>
                             <td>
                                <div class="ctrlBasico">
                                <label name="" runat="server" id="lblGrupo1" style="width:100px; font-weight:bold;">Grupo 01:</label>
                                </div>
                             </td>
                        </tr>
                        <tr>
                            <td>
                                    <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="1:" CssClass="pregunta1" runat="server" ID="radTxtPreg1Resp1" Width="120px" MinValue="1" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="1:" CssClass="pregunta1 Contenedor" runat="server" ID="radTxtPreg1Resp1" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="2:" CssClass="pregunta1" runat="server" ID="radTxtPreg1Resp2" Width="120px" MinValue="1" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="2:" CssClass="pregunta1 Contenedor" runat="server" ID="radTxtPreg1Resp2" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>


                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="3:" CssClass="pregunta1" runat="server" ID="radTxtPreg1Resp3" Width="120px" MinValue="1" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="3:" CssClass="pregunta1 Contenedor" runat="server" ID="radTxtPreg1Resp3" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="4:" CssClass="pregunta1" runat="server" ID="radTxtPreg1Resp4" Width="120px" MinValue="1" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="4:" CssClass="pregunta1 Contenedor" runat="server" ID="radTxtPreg1Resp4" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="5:" CssClass="pregunta1" runat="server" ID="radTxtPreg1Resp5" Width="120px" MinValue="1" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="5:" CssClass="pregunta1 Contenedor" runat="server" ID="radTxtPreg1Resp5" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="6:" CssClass="pregunta1" runat="server" ID="radTxtPreg1Resp6" Width="120px" MinValue="1" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="6:" CssClass="pregunta1 Contenedor" runat="server" ID="radTxtPreg1Resp6" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>
                                </td>
                        </tr>
                        <%--Renglon 2--%>


                        <tr>
                             <td>
                                <div class="ctrlBasico">
                                <label name="" runat="server" id="Label1" style="width:100px; font-weight:bold;">Grupo 02:</label>
                                </div>
                             </td>
                        </tr>
                        <tr>
                            <td>
                                    <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="1:" CssClass="pregunta2" runat="server" ID="radTxtPreg2Resp1" Width="120px"  MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="1:" CssClass="pregunta2 Contenedor" runat="server" ID="radTxtPreg2Resp1" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="2:" CssClass="pregunta2" runat="server" ID="radTxtPreg2Resp2" Width="120px"  MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="2:" CssClass="pregunta2 Contenedor" runat="server" ID="radTxtPreg2Resp2" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="3:" CssClass="pregunta2" runat="server" ID="radTxtPreg2Resp3" Width="120px"  MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="3:" CssClass="pregunta2 Contenedor" runat="server" ID="radTxtPreg2Resp3" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="4:" CssClass="pregunta2" runat="server" ID="radTxtPreg2Resp4" Width="120px"  MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="4:" CssClass="pregunta2 Contenedor" runat="server" ID="radTxtPreg2Resp4" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="5:" CssClass="pregunta2" runat="server" ID="radTxtPreg2Resp5" Width="120px"  MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="5:" CssClass="pregunta2 Contenedor" runat="server" ID="radTxtPreg2Resp5" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="6:" CssClass="pregunta2" runat="server" ID="radTxtPreg2Resp6" Width="120px"  MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="6:" CssClass="pregunta2 Contenedor" runat="server" ID="radTxtPreg2Resp6" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>
                                </td>
                        </tr>
                        <%--Renglon 3--%>
                        <tr>
                             <td>
                                <div class="ctrlBasico">
                                <label name="" runat="server" id="Label2" style="width:100px; font-weight:bold;">Grupo 03:</label>
                                </div>
                             </td>
                        </tr>
                        <tr>
                            <td>
                                    <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="1:" CssClass="pregunta3" runat="server" ID="radTxtPreg3Resp1" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="1:" CssClass="pregunta3 Contenedor" runat="server" ID="radTxtPreg3Resp1" Width="120px" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="2:" CssClass="pregunta3" runat="server" ID="radTxtPreg3Resp2" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="2:" CssClass="pregunta3 Contenedor" runat="server" ID="radTxtPreg3Resp2" Width="120px" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>


                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="3:" CssClass="pregunta3" runat="server" ID="radTxtPreg3Resp3" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="3:" CssClass="pregunta3 Contenedor" runat="server" ID="radTxtPreg3Resp3" Width="120px" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="4:" CssClass="pregunta3" runat="server" ID="radTxtPreg3Resp4" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="4:" CssClass="pregunta3 Contenedor" runat="server" ID="radTxtPreg3Resp4" Width="120px" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="5:" CssClass="pregunta3" runat="server" ID="radTxtPreg3Resp5" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="5:" CssClass="pregunta3 Contenedor" runat="server" ID="radTxtPreg3Resp5" Width="120px" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="6:" CssClass="pregunta3" runat="server" ID="radTxtPreg3Resp6" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="6:" CssClass="pregunta Contenedor3" runat="server" ID="radTxtPreg3Resp6" Width="120px" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>
                                </td>
                        </tr>
                        <%--Renglon 4--%>

                        <tr>
                             <td>
                                <div class="ctrlBasico">
                                <label name="" runat="server" id="Label3" style="width:100px; font-weight:bold;">Grupo 04:</label>
                                </div>
                             </td>
                        </tr>
                        <tr>
                            <td>
                                    <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="1:" CssClass="pregunta4" runat="server" ID="radTxtPreg4Resp1" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="1:" CssClass="pregunta4 Contenedor" runat="server" ID="radTxtPreg4Resp1" Width="120px" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="2:" CssClass="pregunta4" runat="server" ID="radTxtPreg4Resp2" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="2:" CssClass="pregunta4 Contenedor" runat="server" ID="radTxtPreg4Resp2" Width="120px" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>


                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="3:" CssClass="pregunta4" runat="server" ID="radTxtPreg4Resp3" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="3:" CssClass="pregunta4 Contenedor" runat="server" ID="radTxtPreg4Resp3" Width="120px" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="4:" CssClass="pregunta4" runat="server" ID="radTxtPreg4Resp4" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="4:" CssClass="pregunta4 Contenedor" runat="server" ID="radTxtPreg4Resp4" Width="120px" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="5:" CssClass="pregunta4" runat="server" ID="radTxtPreg4Resp5" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="5:" CssClass="pregunta4 Contenedor" runat="server" ID="radTxtPreg4Resp5" Width="120px" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="6:" CssClass="pregunta4" runat="server" ID="radTxtPreg4Resp6" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="6:" CssClass="pregunta4 Contenedor" runat="server" ID="radTxtPreg4Resp6" Width="120px" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>
                                </td>
                        </tr>
                        <%--Renglon 5--%>

                        <tr>
                             <td>
                                <div class="ctrlBasico">
                                <label name="" runat="server" id="Label4" style="width:100px; font-weight:bold;">Grupo 05:</label>
                                </div>
                             </td>
                        </tr>
                        <tr>
                            <td>
                                    <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="1:" CssClass="pregunta5" runat="server" ID="radTxtPreg5Resp1" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="1:" CssClass="pregunta5 Contenedor" runat="server" ID="radTxtPreg5Resp1" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="2:" CssClass="pregunta5" runat="server" ID="radTxtPreg5Resp2" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="2:" CssClass="pregunta5 Contenedor" runat="server" ID="radTxtPreg5Resp2" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>


                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="3:" CssClass="pregunta5" runat="server" ID="radTxtPreg5Resp3" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="3:" CssClass="pregunta5 Contenedor" runat="server" ID="radTxtPreg5Resp3" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="4:" CssClass="pregunta5" runat="server" ID="radTxtPreg5Resp4" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="4:" CssClass="pregunta5 Contenedor" runat="server" ID="radTxtPreg5Resp4" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="5:" CssClass="pregunta5" runat="server" ID="radTxtPreg5Resp5" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="5:" CssClass="pregunta5 Contenedor" runat="server" ID="radTxtPreg5Resp5" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="6:" CssClass="pregunta5" runat="server" ID="radTxtPreg5Resp6" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="6:" CssClass="pregunta5 Contenedor" runat="server" ID="radTxtPreg5Resp6" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>
                                </td>
                        </tr>
                        <%--Renglon 6--%>
                        <tr>
                             <td>
                                <div class="ctrlBasico">
                                <label name="" runat="server" id="Label5" style="width:100px; font-weight:bold;">Grupo 06:</label>
                                </div>
                             </td>
                        </tr>
                        <tr>
                            <td>
                                    <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="1:" CssClass="pregunta6" runat="server" ID="radTxtPreg6Resp1" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="1:" CssClass="pregunta6 Contenedor" runat="server" ID="radTxtPreg6Resp1" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="2:" CssClass="pregunta6" runat="server" ID="radTxtPreg6Resp2" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="2:" CssClass="pregunta6 Contenedor" runat="server" ID="radTxtPreg6Resp2" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>


                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="3:" CssClass="pregunta6" runat="server" ID="radTxtPreg6Resp3" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="3:" CssClass="pregunta6 Contenedor" runat="server" ID="radTxtPreg6Resp3" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="4:" CssClass="pregunta6" runat="server" ID="radTxtPreg6Resp4" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="4:" CssClass="pregunta6 Contenedor" runat="server" ID="radTxtPreg6Resp4" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="5:" CssClass="pregunta6" runat="server" ID="radTxtPreg6Resp5" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="5:" CssClass="pregunta6 Contenedor" runat="server" ID="radTxtPreg6Resp5" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">

                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="6:" CssClass="pregunta6" runat="server" ID="radTxtPreg6Resp6" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="6:" CssClass="pregunta6 Contenedor" runat="server" ID="radTxtPreg6Resp6" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>
                                </td>
                        </tr>
                        <%--Renglon 7--%>
                        <tr>
                             <td>
                                <div class="ctrlBasico">
                                <label name="" runat="server" id="Label6" style="width:100px; font-weight:bold;">Grupo 07:</label>
                                </div>
                             </td>
                        </tr>
                        <tr>
                            <td>
                                    <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="1:" CssClass="pregunta7" runat="server" ID="radTxtPreg7Resp1" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="1:" CssClass="pregunta7 Contenedor" runat="server" ID="radTxtPreg7Resp1" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="2:" CssClass="pregunta7" runat="server" ID="radTxtPreg7Resp2" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="2:" CssClass="pregunta7 Contenedor" runat="server" ID="radTxtPreg7Resp2" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>


                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="3:" CssClass="pregunta7" runat="server" ID="radTxtPreg7Resp3" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="3:" CssClass="pregunta7 Contenedor" runat="server" ID="radTxtPreg7Resp3" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="4:" CssClass="pregunta7" runat="server" ID="radTxtPreg7Resp4" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="4:" CssClass="pregunta7 Contenedor" runat="server" ID="radTxtPreg7Resp4" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="5:" CssClass="pregunta7" runat="server" ID="radTxtPreg7Resp5" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="5:" CssClass="pregunta7 Contenedor" runat="server" ID="radTxtPreg7Resp5" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="6:" CssClass="pregunta7" runat="server" ID="radTxtPreg7Resp6" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="6:" CssClass="pregunta7 Contenedor" runat="server" ID="radTxtPreg7Resp6" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>
                                </td>
                        </tr>
                        <%--Renglon 8--%>
                        <tr>
                             <td>
                                <div class="ctrlBasico">
                                <label name="" runat="server" id="Label7" style="width:100px; font-weight:bold;">Grupo 08:</label>
                                </div>
                             </td>
                        </tr>
                        <tr>
                            <td>
                                    <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="1:" CssClass="pregunta8" runat="server" ID="radTxtPreg8Resp1" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="1:" CssClass="pregunta8 Contenedor" runat="server" ID="radTxtPreg8Resp1" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="2:" CssClass="pregunta8" runat="server" ID="radTxtPreg8Resp2" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="2:" CssClass="pregunta8 Contenedor" runat="server" ID="radTxtPreg8Resp2" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>


                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="3:" CssClass="pregunta8" runat="server" ID="radTxtPreg8Resp3" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="3:" CssClass="pregunta8 Contenedor" runat="server" ID="radTxtPreg8Resp3" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="4:" CssClass="pregunta8" runat="server" ID="radTxtPreg8Resp4" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="4:" CssClass="pregunta8 Contenedor" runat="server" ID="radTxtPreg8Resp4" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="5:" CssClass="pregunta8" runat="server" ID="radTxtPreg8Resp5" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="5:" CssClass="pregunta8 Contenedor" runat="server" ID="radTxtPreg8Resp5" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="6:" CssClass="pregunta8" runat="server" ID="radTxtPreg8Resp6" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="6:" CssClass="pregunta8 Contenedor" runat="server" ID="radTxtPreg8Resp6" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>
                                </td>
                        </tr>
                        <%--Renglon 9--%>
                        <tr>
                             <td>
                                <div class="ctrlBasico">
                                <label name="" runat="server" id="Label8" style="width:100px; font-weight:bold;">Grupo 09:</label>
                                </div>
                             </td>
                        </tr>
                        <tr>
                            <td>
                                    <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="1:" CssClass="pregunta9" runat="server" ID="radTxtPreg9Resp1" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="1:" CssClass="pregunta9" runat="server" ID="radTxtPreg9Resp1" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="2:" CssClass="pregunta9" runat="server" ID="radTxtPreg9Resp2" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="2:" CssClass="pregunta9" runat="server" ID="radTxtPreg9Resp2" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>


                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="3:" CssClass="pregunta9" runat="server" ID="radTxtPreg9Resp3" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="3:" CssClass="pregunta9" runat="server" ID="radTxtPreg9Resp3" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="4:" CssClass="pregunta9" runat="server" ID="radTxtPreg9Resp4" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="4:" CssClass="pregunta9" runat="server" ID="radTxtPreg9Resp4" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="5:" CssClass="pregunta9" runat="server" ID="radTxtPreg9Resp5" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="5:" CssClass="pregunta9" runat="server" ID="radTxtPreg9Resp5" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="6:" CssClass="pregunta9" runat="server" ID="radTxtPreg9Resp6" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="6:" CssClass="pregunta9" runat="server" ID="radTxtPreg9Resp6" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>
                                </td>
                        </tr>
                        <%--Renglon 10--%>
                        <tr>
                             <td>
                                <div class="ctrlBasico">
                                <label name="" runat="server" id="Label9" style="width:100px; font-weight:bold;">Grupo 10:</label>
                                </div>
                             </td>
                        </tr>
                        <tr>
                            <td>
                                    <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="1:" CssClass="pregunta10" runat="server" ID="radTxtPreg10Resp1" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="1:" CssClass="pregunta10 Contenedor" runat="server" ID="radTxtPreg10Resp1" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="2:" CssClass="pregunta10" runat="server" ID="radTxtPreg10Resp2" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="2:" CssClass="pregunta10 Contenedor" runat="server" ID="radTxtPreg10Resp2" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="3:" CssClass="pregunta10" runat="server" ID="radTxtPreg10Resp3" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="3:" CssClass="pregunta10 Contenedor" runat="server" ID="radTxtPreg10Resp3" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="4:" CssClass="pregunta10" runat="server" ID="radTxtPreg10Resp4" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="4:" CssClass="pregunta10 Contenedor" runat="server" ID="radTxtPreg10Resp4" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="5:" CssClass="pregunta10" runat="server" ID="radTxtPreg10Resp5" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="5:" CssClass="pregunta10 Contenedor" runat="server" ID="radTxtPreg10Resp5" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>

                                <div class="ctrlBasico">
                                    <%--<telerik:RadNumericTextBox Label="6:" CssClass="pregunta10" runat="server" ID="radTxtPreg10Resp6" Width="120px" MinValue="0" MaxValue="6" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnValueChanging="validarCampos">--%>
                                    <telerik:RadNumericTextBox Label="6:" CssClass="pregunta10 Contenedor" runat="server" ID="radTxtPreg10Resp6" Width="120px" MaxLength="1" MinValue="1" MaxValue="6" NumberFormat-DecimalDigits="0" Style="text-align: center" ClientEvents-OnBlur="validarCampos" AllowOutOfRangeAutoCorrect="false">
                                    </telerik:RadNumericTextBox>
                                </div>
                                </td>
                        </tr>
                        <%--End Test--%>
                    </tbody>
                </table>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>

    <div style="clear: both; height: 10px;"></div>
    
    <div class="divControlDerecha">
        <div class="ctrlBasico">
        <telerik:RadButton ID="btnTerminar" runat="server" OnClientClicking="close_window" OnClick="btnTerminar_Click" Text="Terminar" AutoPostBack="true"></telerik:RadButton>
    </div>
        </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
