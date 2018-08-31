<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaOrtografiaManual.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaOrtografiaManual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <style id="MyCss" type="text/css">
        .bottomRadMask {
            margin-bottom: 3px;
            padding-bottom: 10px;
        }
    </style>
    <style id="Style1" type="text/css">
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

        label {
        font-weight:bold !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnTerminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnTerminar" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">

            function mensajePruebaTerminada() {
                var btn = $find("<%=btnTerminar.ClientID%>");
                btn.click();
            }

            function close_window(sender, args) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        var btn = $find("<%=btnTerminar.ClientID%>");
                        btn.click();
                        // window.close();
                    }
                });
                var text = "¿Estás seguro que deseas terminar tu prueba?";
                radconfirm(text, callBackFunction, 400, 150, null, "");
                args.set_cancel(true);
            }


            function WinClose() {
                //window.close();
                GetRadWindow().close();
            }
            function CloseTest() {
                //window.close();
                GetRadWindow().close();
            }

        </script>
    </telerik:RadCodeBlock>
    <label name="" id="lbltitulo" class="labelTitulo">Ortografía</label>

    <div class="BorderRadioComponenteHTML" style="width: 100%; height: calc(100% - 100px);">

        <div class="ctrlBasico" style=" padding-top:5px;" >
            <div class="ctrlBasico" style="padding-left: 5%;">
                <label id="lblmensaje" name="" >Por favor ingrese los datos para la prueba:</label>
            </div>

            <div class="ctrlBasico" style="width: 100%;">
                <div class="ctrlBasico" style="padding-left: 5%; width: 30%;">
                    <label name="" runat="server" id="lblOrtografia1" visible="false">Ortografía I (Comunicación escrita )</label>
                </div>

                <div class="ctrlBasico">
                    <telerik:RadNumericTextBox CssClass="PreguntaNo1" Visible="false" runat="server" ID="txtOrtografia1" Width="100px" MinValue="0" MaxValue="40" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadNumericTextBox>
                </div>
            </div>

            <div class="ctrlBasico" style="width: 100%;">
                <div class="ctrlBasico" style="padding-left: 5%; width: 30%;">
                    <label name="" runat="server" id="lblOrtografia2" visible="false">Ortografía 2:</label>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadNumericTextBox CssClass="PreguntaNo1" Visible="false" runat="server" ID="txtOrtografia2" Width="100px" MinValue="0" MaxValue="25" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadNumericTextBox>
                </div>
            </div>

            <div class="ctrlBasico" style="width: 100%;">
                <div class="ctrlBasico" style="padding-left: 5%; width: 30%;">
                    <label name="" runat="server" id="lblOrtografia3" visible="false">Ortografía 3 (Acentos):</label>
                </div>

                <div class="ctrlBasico">
                    <telerik:RadNumericTextBox CssClass="PreguntaNo1" Visible="false" runat="server" ID="txtOrtografia3" Width="100px" MinValue="0" MaxValue="18" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" Style="text-align: center"></telerik:RadNumericTextBox>
                </div>
            </div>

            <div class="ctrlBasico" style="width: 100%; padding-left: 5%;">
                
            <label>Captura el número de aciertos obtenidos.</label>
            </div>
        </div>
             </div>
        <div style="clear: both; height: 10px;"></div>

        <div class="DivBtnTerminarDerecha">
            <telerik:RadButton ID="btnTerminar" runat="server" OnClick="btnTerminar_Click" Text="Terminar" AutoPostBack="true"></telerik:RadButton>
        </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>

</asp:Content>
