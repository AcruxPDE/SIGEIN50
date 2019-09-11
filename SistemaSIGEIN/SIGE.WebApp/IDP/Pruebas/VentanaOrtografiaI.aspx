<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/IDP/Pruebas/Prueba.Master" CodeBehind="VentanaOrtografiaI.aspx.cs" Inherits="SIGE.WebApp.IDP.PruebaOrtografia1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPruebas" runat="server">
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

        .Contenedor
    {
        border-bottom: 3px solid red !important;
        border-top:0px;
        border-right:0px;
        border-left:0px;
        border-radius: 0px 0px;
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

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPruebas" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest" ClientEvents-OnResponseEnd="retorno">
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

            var c = "";
            window.onload = function (sender, args) {
                var tipo = '<%=this.vTipoRevision%>';
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        var segundos = '<%=this.vOrtografia1Seconds%>';     
                        <%--var tipo = '<%=this.vTipoRevision%>';  --%>
                        if (segundos <= 0 && tipo != "REV" && tipo != "EDIT") {
                            $("#seccion1").hide();
                            $("#seccion2").hide();
                            //window.close();                            
                            var idBateria = '<%= vIdBateria%>';
                            var clToken = '<%= vClTokenBateria%>';
                            window.location = "Default.aspx?ty=sig&ID=" + idBateria + "&T=" + clToken;
                        }
                        else if (tipo == "REV") {
                            var btn = $find("<%=btnTerminar.ClientID%>");
                            btn.disabled = true;
                        }
                        else if (tipo == "EDIT") {
                            var btn = $find("<%=btnTerminar.ClientID%>");
                            btn.disabled = true;
                        }
                        else {
                            var ajaxManager = $find("<%=RadAjaxManager1.ClientID%>");
                            ajaxManager.ajaxRequest(null);
                            //var display = document.querySelector('#time');
                            //var contenedor = document.querySelector('.Cronometro');


                            //var vFgCronometro = '<%=MostrarCronometro %>';
                            //if (vFgCronometro == "True") {
                            //    contenedor.style.display = 'block';
                            //}
                            //else {
                            //    contenedor.style.display = 'none';
                            //}

                            //c = Cronometro(segundos, display);

                            var sec1 = document.querySelector('#seccion1');
                            sec1.style.display = 'none';
                            var sec2 = document.querySelector('#seccion2');
                            sec2.style.display = 'none';
                        }
            }
            else {
                var idBateria = '<%= vIdBateria%>';
                var clToken = '<%= vClTokenBateria%>';
                window.location = "Default.aspx?ty=Ini&ID=" + idBateria + "&T=" + clToken;
            }
                });
                if (tipo != "REV" && tipo != "EDIT") {
                    var text = "<label><b>Instrucciones:</b><br/>Escribe en los espacios B, V, C, S o Z, según corresponda. Selecciona las letras del teclado que completen la palabra. Para cambiar de palabra utiliza la tecla Tab. </label>";
                    radconfirm(JustificarTexto(text), callBackFunction, 950, 600, null, "Ortografía I");
                }
            };

            function retorno(sender, args) {
                var segundos = '<%=this.vOrtografia1Seconds%>';
                var display = document.querySelector('#time');
                var contenedor = document.querySelector('.Cronometro');


                var vFgCronometro = '<%=MostrarCronometro %>';
                if (vFgCronometro == "True") {
                    contenedor.style.display = 'block';
                }
                else {
                    contenedor.style.display = 'none';
                }


                c = Cronometro(segundos, display);

                setTimeout(function () {
                    var sec1 = document.querySelector('#seccion1');
                    sec1.style.display = 'block';
                    var sec2 = document.querySelector('#seccion2');
                    sec2.style.display = 'block';
                }, 1000);                
            }

    function mensajePruebaTerminada() {
        var btn = $find("<%=btnTerminar.ClientID%>");
        btn.click();
    }

    function close_window(sender, args) {
        //var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
        //    if (shouldSubmit) {
        //        if (ValidarContendorPreguntas()) {
        //            clearInterval(c);//Se agrega para detener el tiempo del reloj antes de guardar resultados 12/04/2018
        //           var btn = $find("<%=btnTerminar.ClientID%>");
        //            btn.click();
        //            //window.location = "Default.aspx?ty=sig";
        //        }
                // window.close();
        //    }
        //});
        //var text = "¿Estás seguro que deseas terminar tu prueba?";
        //radconfirm(text, callBackFunction, 400, 150, null, "Aviso");
        //args.set_cancel(true);
        if (ValidarContendorPreguntas()) {
            clearInterval(c);
            args.set_cancel(false);
        }
        else {
            args.set_cancel(true);
        }
    }


    function WinClose() {
        //window.close();
        var idBateria = '<%= vIdBateria%>';
        var clToken = '<%= vClTokenBateria%>';
        window.location = "Default.aspx?ty=sig&ID=" + idBateria + "&T=" + clToken;
    }
    function CloseTest() {
        //window.close();
        var idBateria = '<%= vIdBateria%>';
        var clToken = '<%= vClTokenBateria%>';
        window.location = "Default.aspx?ty=sig&ID=" + idBateria + "&T=" + clToken;
    }

    function Close() {
        window.top.location.href = window.top.location.href;//window.close();
    }

    function ValidarContendorPreguntas(sender, args) {
        var flag = true;
        var GNoContestadas = new Array();
        var vContenedor = document.getElementsByClassName("Contenedor");
        var i = 0;
        for (i = 0; i < vContenedor.length; i++) {
            if ((vContenedor[i].control._value) == "") {
                var GrupoNoContestado = document.getElementById(vContenedor[i].id);
                GrupoNoContestado.focus();
                GrupoNoContestado.style.borderWidth = '1px';
                var flag = false;
                break;
            }
        }
        return flag;
    }


    function OpenReport() {
        var vURL = "ReporteadorPruebasIDP.aspx";
        var vTitulo = "Impresión Ortografía I";

        var IdPrueba = '<%= vIdPrueba %>';
        var ClToken = '<%= vClToken %>';



        var windowProperties = {
            width: document.documentElement.clientWidth - 20,
            height: document.documentElement.clientHeight - 20
        };

        vURL = vURL + "?IdPrueba=" + IdPrueba + "&ClToken=" + ClToken + "&ClPrueba=ORTOGRAFIAI";
        var win = window.open(vURL, '_blank');
        win.focus();
        //var wnd = openChildDialog(vURL, "winVistaPrevia", vTitulo, windowProperties);
    }
            function ConfirmarEliminarRespuestas(sender, args) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        this.click();
                    }
                });
                radconfirm("Este proceso borrará las respuestas de todas las pruebas de la batería ¿Desea continuar?", callBackFunction, 400, 180, null, "Aviso");
                args.set_cancel(true);
            }

            function ConfirmarEliminarPrueba(sender, args) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        this.click();
                    }
                });
                radconfirm("Este proceso borrará las respuestas de la prueba seleccionada ¿Desea continuar?", callBackFunction, 400, 180, null, "Aviso");
                args.set_cancel(true);
            }

    </script>
    </telerik:RadCodeBlock>
    <label style="font-size: 21px;">Ortografía I</label>
    <div style="width: 100%; height: calc(100% - 100px); overflow: auto;">
        <div id="seccion1" style="width: 510px; float: left; margin-top: 3%; margin-left: 5%; border: .1px solid; padding-left: 5px">
            <br />
            <label>I.Escribe en los espacios B o V según corresponda:</label><br />
            <div style="height: 20px;"></div>
            <%--         <div style="padding-left:10px; padding-right:10px;">--%>
            <%--            <div id="divA1" style="width: 250px; float: left;">--%>
            <div style="width: 250px; float: left;">
                <label>1. E</label><telerik:RadTextBox ID="pregunta1A1" EnabledStyle-HorizontalAlign="Center"  CssClass="Contenedor" Font-Underline="false" runat="server" Width="22"  Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>asi</label><telerik:RadTextBox ID="pregunta1A2" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" Width="22" BorderStyle="None" Height="20" runat="server" ForeColor="Red" MaxLength="1"></telerik:RadTextBox><label>o</label>
            </div>
            <div style="width: 250px; float: left;">
                <label>2.  </label>
                <telerik:RadTextBox ID="pregunta2A1" runat="server" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" Width="22" Font-Underline="false" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>ulnera</label><telerik:RadTextBox ID="pregunta2A2" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" Width="22" BorderStyle="None" Height="20" runat="server" ForeColor="Red" MaxLength="1"></telerik:RadTextBox><label>le</label>
            </div>
            <%--    <div style="width:100%; float:left">
                <telerik:RadMaskedTextBox  Label="1.-" ID="preguntaA1" runat="server" Mask="Ea\asiao" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning" Width="170px" LabelWidth="30px"  CssClass="Contenedor" >
                </telerik:RadMaskedTextBox>--%>
            <%--<label>2.  </label><telerik:RadTextBox ID="pregunta2A1" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>ulnera</label><telerik:RadTextBox ID="pregunta2A2" Width="22" BorderStyle="None" Height="20" runat="server" ForeColor="Red" MaxLength="1"></telerik:RadTextBox><label>le</label>--%>
            <%--   </div>--%>
            <div style="height: 35px;"></div>
            <div style="width: 250px; float: left;">
                <label>3.  In</label><telerik:RadTextBox ID="pregunta3A1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>enta</label><telerik:RadTextBox ID="pregunta3A2" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>a</label>
            </div>
            <div style="width: 250px; float: left;">
                <label>4.  Her</label><telerik:RadTextBox ID="pregunta4A1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>í</label><telerik:RadTextBox ID="pregunta4A2" EnabledStyle-HorizontalAlign="Center" runat="server" CssClass="Contenedor" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>oro</label>
            </div>
            <div style="height: 35px;"></div>
            <div style="width: 250px; float: left;">
                <label>5.  La</label><telerik:RadTextBox ID="pregunta5A1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>a</label><telerik:RadTextBox ID="pregunta5A2" EnabledStyle-HorizontalAlign="Center" runat="server" Width="22" CssClass="Contenedor" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>le</label>
            </div>
            <div style="width: 250px; float: left;">
                <label>6.  Her</label><telerik:RadTextBox ID="pregunta6A1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label> ir</label>
            </div>
            <div style="height: 35px;"></div>
            <div style="width: 250px; float: left;">
                <label>7.  In</label><telerik:RadTextBox ID="pregunta7A1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>enci</label><telerik:RadTextBox ID="pregunta7A2" EnabledStyle-HorizontalAlign="Center" runat="server" CssClass="Contenedor" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>le</label>
            </div>
            <div style="width: 250px; float: left;">
                <label>8.  Ca</label><telerik:RadTextBox ID="pregunta8A1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>er</label>
            </div>
            <div style="height: 35px;"></div>
            <div style="width: 250px; float: left;">
                <label>9.  Ad</label><telerik:RadTextBox ID="pregunta9A1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>er</label><telerik:RadTextBox ID="pregunta9A2" EnabledStyle-HorizontalAlign="Center" runat="server" Width="22" CssClass="Contenedor" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>io</label>
            </div>
            <div style="width: 250px; float: left;">
                <label>10. Incu</label><telerik:RadTextBox ID="pregunta10A1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>adora</label>
            </div>
            <div style="height: 35px;"></div>
            <div style="width: 250px; float: left;">
                <label>11. A</label><telerik:RadTextBox ID="pregunta11A1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>sol</label><telerik:RadTextBox ID="pregunta11A2" EnabledStyle-HorizontalAlign="Center" runat="server" Width="22" CssClass="Contenedor" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>er</label>
            </div>
            <div style="width: 250px; float: left;">
                <label>12. Lesi</label><telerik:RadTextBox ID="pregunta12A1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>o</label>
            </div>
            <div style="height: 35px;"></div>
            <div style="width: 250px; float: left;">
                <label>13. Repro</label><telerik:RadTextBox ID="pregunta13A1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>a</label><telerik:RadTextBox ID="pregunta13A2" EnabledStyle-HorizontalAlign="Center" runat="server" Width="22" BorderStyle="None" CssClass="Contenedor" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>a</label>
            </div>
            <div style="width: 250px; float: left;">
                <label>14. A</label><telerik:RadTextBox ID="pregunta14A1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>sor</label><telerik:RadTextBox ID="pregunta14A2" EnabledStyle-HorizontalAlign="Center" runat="server" Width="22" BorderStyle="None" Height="20" CssClass="Contenedor" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label> er</label>
            </div>
            <div style="height: 35px;"></div>
            <div style="width: 250px; float: left;">
                <label>15. Preca</label><telerik:RadTextBox ID="pregunta15A1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>ido</label>
            </div>
            <div style="width: 250px; float: left;">
                <label>16. Ad</label><telerik:RadTextBox ID="pregunta16A1"  EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>enimiento</label>
            </div>
            <div style="height: 35px;"></div>
            <div style="width: 250px; float: left;">
                <label>17. Pre</label><telerik:RadTextBox ID="pregunta17A1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>isi</label><telerik:RadTextBox ID="pregunta17A2" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>le</label>
            </div>
            <div style="width: 250px; float: left;">
                <label>18. Conce</label><telerik:RadTextBox ID="pregunta18A1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>ir</label>
            </div>
            <div style="height: 35px;"></div>
            <div style="width: 250px; float: left;">
                <label>19. Pro</label><telerik:RadTextBox ID="pregunta19A1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>er</label><telerik:RadTextBox ID="pregunta19A2"  EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>ial</label>
            </div>
            <div style="width: 250px; float: left;">
                <label>20. Pro</label><telerik:RadTextBox ID="pregunta20A1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox><label>eta</label>
            </div>

            <%--         <telerik:RadMaskedTextBox CssClass="Contenedor" Label="2.-" ID="preguntaA2" runat="server" Mask="au\lner\aa\le" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px" >
                </telerik:RadMaskedTextBox>
            </div>--%>
            <%--  <telerik:RadMaskedTextBox  CssClass="Contenedor" Label="3.-" ID="preguntaA3" runat="server" Mask="Inaent\aa\a" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
             <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="4.-" ID="preguntaA4" runat="server" Mask="Heraíaoro" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px" >

            </telerik:RadMaskedTextBox>
             <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="5.-" ID="preguntaA5" runat="server" Mask="\L\aa\aa\le" RequireCompleteText="True"  SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="6.-" ID="preguntaA6" runat="server" Mask="Herair" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox  CssClass="Contenedor"  Label="7.-" ID="preguntaA7" runat="server" Mask="Inaencia\le" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="8.-" ID="preguntaA8" runat="server" Mask="C\aaer" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="9.-" ID="preguntaA9" runat="server" Mask="\Adaeraio" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="10.-" ID="preguntaA10" runat="server" Mask="Incua\ador\a" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="11.-" ID="preguntaA11" runat="server" Mask="\Aaso\laer" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="12.-" ID="preguntaA12" runat="server" Mask="\Lesiao" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="13.-" ID="preguntaA13" runat="server" Mask="Reproa\aa\a" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox  CssClass="Contenedor"  Label="14.-" ID="preguntaA14" runat="server" Mask="\Aasoraer" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="15.-" ID="preguntaA15" runat="server" Mask="Prec\aaido" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="16.-" ID="preguntaA16" runat="server" Mask="\Adaenimiento" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="17.-" ID="preguntaA17" runat="server" Mask="Preaisia\le" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="18.-" ID="preguntaA18" runat="server" Mask="Conceair" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="19.-" ID="preguntaA19" runat="server" Mask="Proaerai\a\l" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="20.-" ID="preguntaA20" runat="server" Mask="Proaet\a" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>--%>
            <div style="height: 30px;"></div>
        </div>
        <div id="seccion2" style="width: 510px; float: left; margin-top: 3%; margin-left: 5%; border: .1px solid; padding-left: 5px">
            <br />
            <label>II.Escribe en los espacios C, S o Z, según corresponda:</label><br />
            <div style="height: 20px;"></div>
            <div style="width: 250px; float: left;">
                <label>1. Atro</label><telerik:RadTextBox ID="pregunta1B1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox>
                <label>idad</label>
            </div>
            <div style="width: 250px; float: left;">
                <label>2. Prin</label><telerik:RadTextBox ID="pregunta2B1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox>
                <label>e</label><telerik:RadTextBox ID="pregunta2B2" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox>
                <label>ita</label>
            </div>
            <div style="height: 35px;"></div>
            <div style="width: 250px; float: left;">
                <label>3. Pardu</label><telerik:RadTextBox ID="pregunta3B1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox>
                <label>co</label>
            </div>
            <div style="width: 250px; float: left;">
                <label>4. Anali</label><telerik:RadTextBox ID="pregunta4B1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox>
                <label>e</label>
            </div>
            <div style="height: 35px;"></div>
            <div style="width: 250px; float: left;">
                <label>5. E</label><telerik:RadTextBox ID="pregunta5B1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox>
                <label>en</label><telerik:RadTextBox ID="pregunta5B2" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox>
                <label>ial</label>
            </div>
            <div style="width: 250px; float: left;">
                <label>6. Con</label><telerik:RadTextBox ID="pregunta6B1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox>
                <label>en</label><telerik:RadTextBox ID="pregunta6B2" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox>
                <label>o</label>
            </div>
            <div style="height: 35px;"></div>
            <div style="width: 250px; float: left;">
                <label>7. Sinaloen</label><telerik:RadTextBox ID="pregunta7B1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox>
                <label>e</label>
            </div>
            <div style="width: 250px; float: left;">
                <label>8. Exta</label><telerik:RadTextBox ID="pregunta8B1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox>
                <label>iar</label>
            </div>
            <div style="height: 35px;"></div>
            <div style="width: 250px; float: left;">
                <label>9. Ra</label><telerik:RadTextBox ID="pregunta9B1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox>
                <label>go(sustantivo)</label>
            </div>
            <div style="width: 250px; float: left;">
                <label>10. Compla</label><telerik:RadTextBox ID="pregunta10B1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox>
                <label>er</label>
            </div>
            <div style="height: 35px;"></div>
            <div style="width: 250px; float: left;">
                <label>11. Comen</label><telerik:RadTextBox ID="pregunta11B1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox>
                <label>emos</label>
            </div>
            <div style="width: 250px; float: left;">
                <label>12. Compla</label><telerik:RadTextBox ID="pregunta12B1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox>
                <label>co</label>
            </div>
            <div style="height: 35px;"></div>
            <div style="width: 250px; float: left;">
                <label>13. Discre</label><telerik:RadTextBox ID="pregunta13B1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox>
                <label>ión</label>
            </div>
            <div style="width: 250px; float: left;">
                <label>14. No</label><telerik:RadTextBox ID="pregunta14B1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox>
                <label>ivo</label>
            </div>
            <div style="height: 35px;"></div>
            <div style="width: 250px; float: left;">
                <label>15. Timide</label><telerik:RadTextBox ID="pregunta15B1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox>
            </div>
            <div style="width: 250px; float: left;">
                <label>16. Oca </label>
                <telerik:RadTextBox ID="pregunta16B1" EnabledStyle-HorizontalAlign="Center" CssClass="Contenedor" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox>
                <label>ión</label>
            </div>
            <div style="height: 35px;"></div>
            <div style="width: 250px; float: left;">
                <label>17. Blancu</label><telerik:RadTextBox ID="pregunta17B1" CssClass="Contenedor" EnabledStyle-HorizontalAlign="Center" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox>
                <label>co</label>
            </div>
            <div style="width: 250px; float: left;">
                <label>18. Horten</label><telerik:RadTextBox ID="pregunta18B1" CssClass="Contenedor" EnabledStyle-HorizontalAlign="Center" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox>
                <label>ia</label>
            </div>
            <div style="height: 35px;"></div>
            <div style="width: 250px; float: left;">
                <label>19. Pre</label><telerik:RadTextBox ID="pregunta19B1" CssClass="Contenedor" EnabledStyle-HorizontalAlign="Center" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox>
                <label>i</label><telerik:RadTextBox ID="pregunta19B2" CssClass="Contenedor" EnabledStyle-HorizontalAlign="Center" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox>
                <label>ar</label>
            </div>
            <div style="width: 250px; float: left;">
                <label>20. E</label><telerik:RadTextBox CssClass="Contenedor" ID="pregunta20B1" EnabledStyle-HorizontalAlign="Center" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox>
                <label>ca</label><telerik:RadTextBox CssClass="Contenedor" ID="pregunta20B2" EnabledStyle-HorizontalAlign="Center" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox>
                <label>e</label><telerik:RadTextBox CssClass="Contenedor" ID="pregunta20B3" EnabledStyle-HorizontalAlign="Center" runat="server" Width="22" BorderStyle="None" Height="20" MaxLength="1" ForeColor="Red"></telerik:RadTextBox>
            </div>
            <%--<telerik:RadMaskedTextBox   CssClass="Contenedor" Label="1.-" ID="preguntaB1" runat="server" Mask="Atroaid\ad" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="2.-" ID="preguntaB2" runat="server" Mask="Prinaeait\a" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="3.-" ID="preguntaB3" runat="server" Mask="P\arduaco" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="4.-" ID="preguntaB4" runat="server" Mask="An\a\liae" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="5.-" ID="preguntaB5" runat="server" Mask="Eaenai\a\l" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox  CssClass="Contenedor"  Label="6.-" ID="preguntaB6" runat="server" Mask="Conaenao" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="7.-" ID="preguntaB7" runat="server" Mask="Sin\a\loenae" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="8.-" ID="preguntaB8" runat="server" Mask="Ext\aai\ar" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="9.-" ID="preguntaB9" runat="server" Mask="R\aago(sust\antivo)" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="10.-" ID="preguntaB10" runat="server" Mask="Comp\l\aaer" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="11.-" ID="preguntaB11" runat="server" Mask="Comenaemos" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox  CssClass="Contenedor"  Label="12.-" ID="preguntaB12" runat="server" Mask="Comp\l\aaco" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="13.-" ID="preguntaB13" runat="server" Mask="Discreaión" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="14.-" ID="preguntaB14" runat="server" Mask="Noaivo" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="15.-" ID="preguntaB15" runat="server" Mask="Timidea" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="16.-" ID="preguntaB16" runat="server" Mask="Oc\aaión" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox  CssClass="Contenedor"  Label="17.-" ID="preguntaB17" runat="server" Mask="B\l\ancuaco" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox   CssClass="Contenedor" Label="18.-" ID="preguntaB18" runat="server" Mask="Hortenai\a" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox  CssClass="Contenedor"  Label="19.-" ID="preguntaB19" runat="server" Mask="Preaia\ar" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>
            <telerik:RadMaskedTextBox  CssClass="Contenedor"  Label="20.-" ID="preguntaB20" runat="server" Mask="Eac\aaea" RequireCompleteText="True" SelectionOnFocus="CaretToBeginning"  Width="170px" LabelWidth="30px">

            </telerik:RadMaskedTextBox>--%>
            <div style="height: 30px;"></div>
        </div>
    </div>

    <div style="clear: both; height: 10px;"></div>

    <div class="DivMoveLeft" id="cronometro" runat="server">
        <div class="Cronometro">Tiempo restante <span id="time"></span></div>
    </div>
    <div class="divControlDerecha" style="margin: 2px;">
            <telerik:RadButton ID="btnEliminarBateria" runat="server" Text="Eliminar batería" AutoPostBack="true" OnClientClicking="ConfirmarEliminarRespuestas" OnClick="btnEliminarBateria_Click"></telerik:RadButton>
    </div>
    <div class="divControlDerecha" style="margin: 2px;">
        <telerik:RadButton ID="btnEliminar" runat="server"  Text="Eliminar" AutoPostBack="true" OnClientClicking="ConfirmarEliminarPrueba" OnClick="btnEliminar_Click"></telerik:RadButton>
    </div>
    <div class="divControlDerecha" style="margin: 2px;">
        <telerik:RadButton Visible="false" ID="btnImpresionPrueba" runat="server" OnClientClicked="OpenReport" Text="Imprimir" AutoPostBack="false"></telerik:RadButton>
    </div>
    <div class="divControlDerecha" style="margin: 2px;">
        <telerik:RadButton ID="btnTerminar" runat="server" OnClientClicking="close_window" OnClick="btnTerminar_Click" Text="Terminar" AutoPostBack="true"></telerik:RadButton>
    </div>
    <div class="divControlDerecha" style="margin: 2px;">
        <telerik:RadButton ID="btnCorregir" runat="server" Visible="false" OnClick="btnCorregir_Click" Text="Guardar" AutoPostBack="true"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
