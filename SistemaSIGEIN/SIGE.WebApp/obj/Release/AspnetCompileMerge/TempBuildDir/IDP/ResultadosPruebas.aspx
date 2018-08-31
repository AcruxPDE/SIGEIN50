<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/MenuIDP.master" AutoEventWireup="true" CodeBehind="ResultadosPruebas.aspx.cs" Inherits="SIGE.WebApp.IDP.ResultadosPruebas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../Assets/js/Graph.js" type="text/javascript"></script>
    <script type="text/javascript" id="script">
        var centerX = 299;
        var centerY = 235;
        var radioLen = 181;

        function DoGraph() {
            var grafica = '<%= grafica %>';
            if (grafica.value == "") {
                return;
            }

            InitGraphContext();

            var dataArr = grafica.split(",");
            var pointArr = new Array(4);

            // Pasar las unidades de porcentaje a pixeles
            for (i = 0; i < 4; ++i) {
                pointArr[i] = Math.floor(parseInt(dataArr[i]) * radioLen / 100);
            }
            // Graficar
            DrawLine(centerX, centerY - pointArr[0], centerX + pointArr[1], centerY, "red");
            DrawLine(centerX + pointArr[1], centerY, centerX, centerY + pointArr[2], "red");
            DrawLine(centerX, centerY + pointArr[2], centerX - pointArr[3], centerY, "red");
            DrawLine(centerX - pointArr[3], centerY, centerX, centerY - pointArr[0], "red");

        }


        function OpenLaboral1() {
            var pValue = "LABORAL-1";
            OpenReporteadorResultados(pValue);
        }

        function OpenInteres() {
            var pValue = "INTERES";
            OpenReporteadorResultados(pValue);
        }

        function OpenEstilo() {
            var pValue = "PENSAMIENTO";
            OpenReporteadorResultados(pValue);
        }

        function OpenAptitud1() {
            var pValue = "APTITUD-1";
            OpenReporteadorResultados(pValue);
        }
        
        function OpenAptitud2() {
            var pValue = "APTITUD-2";
            OpenReporteadorResultados(pValue);
        }

        function OpenOrtografia() {
            var pValue = "ORTOGRAFIA";
            OpenReporteadorResultados(pValue);
        }

        function OpenTecnica() {
            var pValue = "TECNICAPC";
            OpenReporteadorResultados(pValue);
        }
        
        function OpenIngles(){
            var pValue = "INGLES";
            OpenReporteadorResultados(pValue);
        }

        function OpenLaboral2() {
            var pValue = "LABORAL-2";
            OpenReporteadorResultados(pValue);
        }

        function OpenAdaptacion() {
            var pValue = "ADAPTACION";
            OpenReporteadorResultados(pValue);
        }
        
        function OpenTiva() {
            var pValue = "TIVA";
            OpenReporteadorResultados(pValue);
        }

        function OpenReporteadorResultados(pValue) {
            var vIdBateria = '<%= vIdBateria %>';
            var vClToken = '<%= vClToken %>';

            var vURL = "ReporteadorResultadosPruebas.aspx";
            var vTitulo = "Impresión resultados";

            vURL = vURL + "?IdBateria=" + vIdBateria + "&ClToken=" + vClToken + "&ClPrueba=" + pValue;
            var win = window.open(vURL, '_blank');
            win.focus();
        }

    </script>
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

        Table td {
            padding: 3px 0px 3px 0px;
        }

        label {
            font-weight: bold !important;
        }

        .Abajo {
            vertical-align: bottom;
        }

        .Arriba {
            vertical-align: top;
        }

        .Izquierda {
            text-align: left;
        }

        .Derecha {
            text-align: right;
        }

        .Cuadrante {
            width: 205px !important;
            height: 175px !important;
        }

        .sombra {
            -webkit-box-shadow: 4px 4px 17px 0px rgba(0,0,0,0.75);
            -moz-box-shadow: 4px 4px 17px 0px rgba(0,0,0,0.75);
            box-shadow: 4px 4px 17px 0px rgba(0,0,0,0.75);
        }

        .Ink {
            position: absolute;
            background-color: #fff;
            border-top: 1px solid red;
            width: 1px;
            height: 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            var c = "";

            window.onload = function (sender, args) {

                if ('<%=this.vHabilitaLaboralI%>' != "False") {
                    DisplayGraphs();
                }
            };

            function DisplayGraphs() {
                // Favorables  
                for (i = 1; i <= 4; ++i)
                    AuxDisplay("F", i);
                // Desfavorables  
                for (i = 1; i <= 4; ++i)
                    AuxDisplay("D", i);
            }

            function AuxDisplay(pCode, pNum) {

                var fldName = pCode + pNum;
                var vContenedorValor;

                switch (pCode + pNum) {
                    case "F1":
                        vContenedorValor = document.getElementById("<%= lblF1.ClientID %>");
                        break;
                    case "F2":
                        vContenedorValor = document.getElementById("<%= lblF2.ClientID %>");
                        break;
                    case "F3":
                        vContenedorValor = document.getElementById("<%= lblF3.ClientID %>");
                        break;
                    case "F4":
                        vContenedorValor = document.getElementById("<%= lblF4.ClientID %>");
                        break;
                    case "D1":
                        vContenedorValor = document.getElementById("<%= lblD1.ClientID %>");
                        break;
                    case "D2":
                        vContenedorValor = document.getElementById("<%= lblD2.ClientID %>");
                        break;
                    case "D3":
                        vContenedorValor = document.getElementById("<%= lblD3.ClientID %>");
                        break;
                    case "D4":
                        vContenedorValor = document.getElementById("<%= lblD4.ClientID %>");
                        break;
                }

                if (vContenedorValor != undefined) {
                    var noValor = vContenedorValor.innerHTML;
                    var valImg = Math.floor((parseInt(noValor) - 10) / 2) + 1;
                    if (pNum > 1)--valImg;
                    if (valImg > 13) valImg = 13;
                    if (valImg < 0) valImg = 0;
                    var Color = "";
                    var Style = "";
                    switch (pNum) {
                        case 1:
                            Color = "Azul";
                            break;
                        case 2:
                            Color = "Rojo";

                            break;
                        case 3:
                            Color = "Verde";
                            break;
                        case 4:
                            Color = "Amarillo";
                            break;
                    }
                    document.getElementById("img" + fldName).src = "../Assets/images/PruebaLaboralII/Lifo" + Color + valImg + ".gif";
                    document.getElementById("sp" + fldName).innerHTML = noValor;
                }

            }

        </script>
    </telerik:RadCodeBlock>
    <div class="ctrlBasico" style="width: 10%">
        <label id="Label6" name="" class="labelTitulo">Resultados</label>
    </div>
    <div class="ctrlBasico">
        <table class="ctrlTableForm">
            <tr>
                
                <td class="ctrlTableDataContext">
                    <label>Folio de solicitud:</label></td>
                <td colspan="2" class="ctrlTableDataBorderContext">
                    <div id="txtFolio" runat="server"></div>
                </td>

                <td class="ctrlTableDataContext">
                    <label>Candidato:</label></td>
                <td colspan="2" class="ctrlTableDataBorderContext">
                    <div id="txtNbCandidato" runat="server"></div>
                </td>

            </tr>
        </table>
    </div>
    <div style="height: calc(100% - 60px);">
        <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0">
            <telerik:RadPane ID="rpResultadoPruebas" runat="server" Width="185" Scrolling="Y">
                <telerik:RadTabStrip ID="tbPruebas" runat="server" SelectedIndex="0" MultiPageID="mpgResultados" Orientation="VerticalLeft" CssClass="divControlDerecha">
                    <Tabs>
                        <telerik:RadTab Text="Personalidad laboral I" Visible="false"></telerik:RadTab>
                        <telerik:RadTab Text="Intereses personales" Visible="false"></telerik:RadTab>
                        <telerik:RadTab Text="Estilo de pensamiento" Visible="false"></telerik:RadTab>
                        <telerik:RadTab Text="Aptitud mental I" Visible="false"></telerik:RadTab>
                        <telerik:RadTab Text="Aptitud mental II" Visible="false"></telerik:RadTab>
                        <telerik:RadTab Text="Personalidad laboral II" Visible="false"></telerik:RadTab>
                        <telerik:RadTab Text="Adaptación al medio" Visible="false"></telerik:RadTab>
                        <telerik:RadTab Text="TIVA" Visible="false"></telerik:RadTab>
                        <telerik:RadTab Text="Ortografía I,II,III" Visible="false"></telerik:RadTab>
                        <telerik:RadTab Text="Técnica PC" Visible="false"></telerik:RadTab>
                        <telerik:RadTab Text="Redacción" Visible="false"></telerik:RadTab>
                        <telerik:RadTab Text="Inglés" Visible="false"></telerik:RadTab>
                        <telerik:RadTab Text="Entrevista" Visible="false"></telerik:RadTab>
                        <telerik:RadTab Text="Resultados" Visible="false"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
            </telerik:RadPane>
            <telerik:RadSplitBar ID="rsbResultadoPruebas" runat="server" CollapseMode="Forward" EnableResize="false"></telerik:RadSplitBar>
            <telerik:RadPane ID="radPanelPreguntas" runat="server">
                <div style="height: 5px"></div>
                <telerik:RadMultiPage ID="mpgResultados" runat="server" SelectedIndex="0">
                    <telerik:RadPageView ID="RPVLaboralI" runat="server">

                        <div style="width: 95%; margin-left: 3%; margin-right: 2%;">
                            <div class="ctrlBasico">
                                <telerik:RadGrid ID="grdResultadosLaboralI" ShowHeader="true" runat="server" GridLines="None" AutoGenerateColumns="false">
                                    <MasterTableView>
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderText="" DataField="NB_TITULO" HeaderStyle-Width="60"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="D" DataField="D_VALUE" HeaderStyle-Width="60"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="I" DataField="I_VALUE" HeaderStyle-Width="60"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="S" DataField="S_VALUE" HeaderStyle-Width="60"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="C" DataField="C_VALUE" HeaderStyle-Width="60"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <div class="ctrlBasico">
                                <table>
                                    <tr>
                                        <td>
                                            <h4 style="width: 300px;">Características sobresalientes de personalidad:</h4>
                                        </td>
                                        <td>
                                            <label id="lblCaracteristicas" runat="server"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4 style="width: 300px;">Situación motivante:</h4>
                                        </td>
                                        <td>
                                            <label id="lblMotivante" runat="server"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4 style="width: 300px;">Situación presionante:</h4>
                                        </td>
                                        <td>
                                            <label id="lblPresionante" runat="server"></label>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <div style="clear: both; height: 20px;"></div>

                            <label id="lblValidez" runat="server">Validez: 1</label>

                            <div style="clear: both; height: 20px;"></div>
                            <div class="ctrlBasico">
                                <table>
                                    <tr>
                                        <td>
                                            <h4 id="Label2" runat="server">D = Empuje</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4 id="Label3" runat="server">I = Influencia</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4 id="Label4" runat="server">S= Constancia</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4 id="Label5" runat="server">C = Apego a normas</h4>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <div style="clear: both; height: 10px;"></div>

                            <%--<div class="ctrlBasico">--%>
                            <div style="float: left; width: 20%; height: 500px">
                                <telerik:RadHtmlChart runat="server" ID="RadHtmlCotidiana" Width="100%" Height="100%" Transitions="true" Skin="Silk">
                                    <Appearance>
                                        <FillStyle BackgroundColor="Transparent"></FillStyle>
                                    </Appearance>
                                    <Legend>
                                        <Appearance BackgroundColor="Transparent" Position="Bottom">
                                        </Appearance>
                                    </Legend>
                                    <PlotArea>
                                        <Appearance>
                                            <FillStyle BackgroundColor="Transparent"></FillStyle>
                                        </Appearance>
                                        <XAxis AxisCrossingValue="0" Color="black" MajorTickType="Outside" MinorTickType="Outside"
                                            Reversed="false">
                                            <Items>
                                                <telerik:AxisItem LabelText="D"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="I"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="S"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="C"></telerik:AxisItem>
                                            </Items>
                                            <LabelsAppearance DataFormatString="{0}" RotationAngle="0" Skip="0" Step="1">
                                            </LabelsAppearance>
                                        </XAxis>

                                        <%--<telerik:AxisItem LabelText="5"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="10"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="16"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="20"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="30"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="40"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="50"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="60"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="70"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="80"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="84"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="90"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="95"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="99"></telerik:AxisItem>--%>
                                        <YAxis AxisCrossingValue="0" Color="black" MajorTickSize="1" MajorTickType="Outside"
                                            MaxValue="100" MinorTickSize="1" MinorTickType="Outside" MinValue="0" Reversed="false"
                                            Step="10">
                                            <PlotBands>
                                                <telerik:PlotBand From="93" To="94" Color="#008de7" Alpha="100" />
                                                <telerik:PlotBand From="50" To="51" Color="#008de7" Alpha="100" />
                                                <telerik:PlotBand From="8" To="9" Color="#008de7" Alpha="100" />
                                            </PlotBands>
                                        </YAxis>
                                        <Series>
                                            <telerik:LineSeries>
                                                <Appearance>
                                                    <FillStyle BackgroundColor="#5ab7de"></FillStyle>
                                                </Appearance>
                                                <LabelsAppearance DataFormatString="{0}%" Position="Above">
                                                </LabelsAppearance>
                                                <LineAppearance Width="1" />
                                                <MarkersAppearance MarkersType="Circle" BackgroundColor="White" Size="8" BorderColor="#5ab7de"
                                                    BorderWidth="2"></MarkersAppearance>
                                                <TooltipsAppearance DataFormatString="{0}%"></TooltipsAppearance>
                                            </telerik:LineSeries>
                                        </Series>
                                    </PlotArea>
                                    <ChartTitle Text="T">
                                        <Appearance Align="Center" BackgroundColor="Transparent" Position="Top">
                                        </Appearance>
                                    </ChartTitle>
                                </telerik:RadHtmlChart>
                            </div>
                            <div style="float: left; width: 60px; height: 500px"></div>
                            <div style="float: left; width: 20%; height: 500px">
                                <telerik:RadHtmlChart runat="server" ID="rhcMotivante" Width="100%" Height="100%" Transitions="true" Skin="Silk">
                                    <Appearance>
                                        <FillStyle BackgroundColor="Transparent"></FillStyle>
                                    </Appearance>
                                    <Legend>
                                        <Appearance BackgroundColor="Transparent" Position="Bottom">
                                        </Appearance>
                                    </Legend>
                                    <PlotArea>
                                        <Appearance>
                                            <FillStyle BackgroundColor="Transparent"></FillStyle>
                                        </Appearance>
                                        <XAxis AxisCrossingValue="0" Color="black" MajorTickType="Outside" MinorTickType="Outside"
                                            Reversed="false">
                                            <Items>
                                                <telerik:AxisItem LabelText="D"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="I"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="S"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="C"></telerik:AxisItem>
                                            </Items>
                                            <LabelsAppearance DataFormatString="{0}" RotationAngle="0" Skip="0" Step="1">
                                            </LabelsAppearance>
                                        </XAxis>
                                        <YAxis AxisCrossingValue="0" Color="black" MajorTickSize="1" MajorTickType="Outside"
                                            MaxValue="100" MinorTickSize="1" MinorTickType="Outside" MinValue="0" Reversed="false"
                                            Step="10">
                                            <PlotBands>
                                                <telerik:PlotBand From="93" To="94" Color="#008de7" Alpha="100" />
                                                <telerik:PlotBand From="50" To="51" Color="#008de7" Alpha="100" />
                                                <telerik:PlotBand From="8" To="9" Color="#008de7" Alpha="100" />
                                            </PlotBands>
                                            <%--<LabelsAppearance DataFormatString="{0}%" RotationAngle="0" Skip="0" Step="1">
                                                </LabelsAppearance>--%>
                                            <%--   <TitleAppearance Position="Center" RotationAngle="0" Text="CPU Load">
                                                </TitleAppearance>--%>
                                        </YAxis>
                                        <Series>
                                            <telerik:LineSeries>
                                                <Appearance>
                                                    <FillStyle BackgroundColor="#5ab7de"></FillStyle>
                                                </Appearance>
                                                <LabelsAppearance DataFormatString="{0}%" Position="Above">
                                                </LabelsAppearance>
                                                <LineAppearance Width="1" />
                                                <MarkersAppearance MarkersType="Circle" BackgroundColor="White" Size="8" BorderColor="#5ab7de"
                                                    BorderWidth="2"></MarkersAppearance>
                                                <TooltipsAppearance DataFormatString="{0}%"></TooltipsAppearance>
                                            </telerik:LineSeries>
                                        </Series>
                                    </PlotArea>
                                    <ChartTitle Text="M">
                                        <Appearance Align="Center" BackgroundColor="Transparent" Position="Top">
                                        </Appearance>
                                    </ChartTitle>
                                </telerik:RadHtmlChart>
                            </div>
                            <div style="float: left; width: 60px; height: 500px"></div>
                            <div style="float: left; width: 20%; height: 500px">
                                <telerik:RadHtmlChart runat="server" ID="rhcPresionante" Width="100%" Height="100%" Transitions="true" Skin="Silk">
                                    <Appearance>
                                        <FillStyle BackgroundColor="Transparent"></FillStyle>
                                    </Appearance>
                                    <Legend>
                                        <Appearance BackgroundColor="Transparent" Position="Bottom">
                                        </Appearance>
                                    </Legend>
                                    <PlotArea>
                                        <Appearance>
                                            <FillStyle BackgroundColor="Transparent"></FillStyle>
                                        </Appearance>
                                        <XAxis AxisCrossingValue="0" Color="black" MajorTickType="Outside" MinorTickType="Outside"
                                            Reversed="false">
                                            <Items>
                                                <telerik:AxisItem LabelText="D"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="I"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="S"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="C"></telerik:AxisItem>
                                            </Items>
                                            <LabelsAppearance DataFormatString="{0}" RotationAngle="0" Skip="0" Step="1">
                                            </LabelsAppearance>
                                        </XAxis>
                                        <YAxis AxisCrossingValue="0" Color="black" MajorTickSize="1" MajorTickType="Outside"
                                            MaxValue="100" MinorTickSize="0" MinorTickType="Outside" MinValue="0" Reversed="false"
                                            Step="10">
                                            <PlotBands>
                                                <telerik:PlotBand From="95" To="96" Color="#008de7" Alpha="100" />
                                                <telerik:PlotBand From="50" To="51" Color="#008de7" Alpha="100" />
                                                <telerik:PlotBand From="8" To="9" Color="#008de7" Alpha="100" />
                                            </PlotBands>
                                            <%-- <LabelsAppearance DataFormatString="{0}%" RotationAngle="0" Skip="0" Step="1">
                                                </LabelsAppearance>--%>
                                            <%--   <TitleAppearance Position="Center" RotationAngle="0" Text="CPU Load">
                                                </TitleAppearance>--%>
                                        </YAxis>
                                        <Series>
                                            <telerik:LineSeries>
                                                <Appearance>
                                                    <FillStyle BackgroundColor="#5ab7de"></FillStyle>
                                                </Appearance>
                                                <LabelsAppearance DataFormatString="{0}%" Position="Above">
                                                </LabelsAppearance>
                                                <LineAppearance Width="1" />
                                                <MarkersAppearance MarkersType="Circle" BackgroundColor="White" Size="8" BorderColor="#5ab7de"
                                                    BorderWidth="2"></MarkersAppearance>
                                                <TooltipsAppearance DataFormatString="{0}%"></TooltipsAppearance>
                                            </telerik:LineSeries>
                                        </Series>
                                    </PlotArea>
                                    <ChartTitle Text="L">
                                        <Appearance Align="Center" BackgroundColor="Transparent" Position="Top">
                                        </Appearance>
                                    </ChartTitle>
                                </telerik:RadHtmlChart>
                            </div>





                            <%--</div>--%>
                            <div style="clear: both; height: 20px;"></div>
                            <table>
                                <tr>
                                    <td>
                                        <div id="divCotidiana" runat="server">
                                        </div>
                                        <br>
                                        <hr>
                                        <br>
                                        <div id="divMotivante" runat="server">
                                        </div>
                                        <br>
                                        <hr>
                                        <br>
                                        <div style="text-align: justify;">
                                            <p><b>Posibles limitaciones</b></p>
                                            <br />
                                            <p>
                                                Debemos recordar que ninguna persona es perfecta en todas las situaciones. Las características
            sobresalientes de comportamiento que resultan en el éxito en un clima compatible, son las mismas
            características que pueden llegar a ser factores limitantes en una situación de presión o de
            stress. Todas las personas tenemos limitaciones. El ejecutivo debe comprender estas posibles limitaciones
            y estar preparado para manejarlas, puesto que tienden a surgir en las ocasiones en que pueden ser
            más perjudiciales.
                                            </p>
                                        </div>
                                        <div id="divPresion" runat="server">
                                        </div>
                                    </td>
                                </tr>
                            </table>

                            <div class="divControlDerecha">
                                <telerik:RadButton ID="btnImprimir" runat="server" AutoPostBack="false" Text="Imprimir" OnClientClicked="OpenLaboral1"></telerik:RadButton>
                            </div>
                        </div>
                    </telerik:RadPageView>

                    <telerik:RadPageView ID="RPVIntereses" runat="server">
                        <div>
                            <div class="ctrlBasico" style="margin-left: 20px;">
                                <telerik:RadGrid ID="grdResultadosInteres" ShowHeader="false" runat="server" GridLines="None" AutoGenerateColumns="false">
                                    <MasterTableView>
                                        <Columns>
                                            <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="" DataField="NB_TITULO" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="" DataField="VALOR" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>

                            <div style="clear: both;"></div>
                            <div class="ctrlBasico" style="margin-left: 20px;">
                                <div>
                                    <telerik:RadHtmlChart runat="server" ID="LineChart" Width="630px" Height="400px" Transitions="true" Skin="Silk">
                                        <Appearance>
                                            <FillStyle BackgroundColor="Transparent"></FillStyle>
                                        </Appearance>
                                        <ChartTitle Text="Perfil de valores personales">
                                            <Appearance Align="Center" BackgroundColor="Transparent" Position="Top">
                                            </Appearance>
                                        </ChartTitle>
                                        <Legend>
                                            <Appearance BackgroundColor="Transparent" Position="Bottom">
                                            </Appearance>
                                        </Legend>
                                        <PlotArea>
                                            <Appearance>
                                                <FillStyle BackgroundColor="Transparent"></FillStyle>
                                            </Appearance>
                                            <XAxis AxisCrossingValue="0" Color="black" MajorTickType="Outside" MinorTickType="Outside"
                                                Reversed="false">
                                                <Items>
                                                    <telerik:AxisItem LabelText="Teórico"></telerik:AxisItem>
                                                    <telerik:AxisItem LabelText="Económico"></telerik:AxisItem>
                                                    <telerik:AxisItem LabelText="Artístico"></telerik:AxisItem>
                                                    <telerik:AxisItem LabelText="Social"></telerik:AxisItem>
                                                    <telerik:AxisItem LabelText="Político"></telerik:AxisItem>
                                                    <telerik:AxisItem LabelText="Regulatorio"></telerik:AxisItem>
                                                    <%--<telerik:AxisItem LabelText="TS"></telerik:AxisItem>--%>
                                                </Items>
                                                <LabelsAppearance DataFormatString="{0}" RotationAngle="280" Skip="0" Step="1">
                                                </LabelsAppearance>
                                                <%--  <TitleAppearance Position="Center" RotationAngle="0" Text="Days">
                                                </TitleAppearance>--%>
                                            </XAxis>
                                            <YAxis AxisCrossingValue="0" Color="black" MajorTickSize="1" MajorTickType="Outside"
                                                MaxValue="100" MinorTickSize="1" MinorTickType="Outside" MinValue="0" Reversed="false"
                                                Step="25">
                                                <LabelsAppearance DataFormatString="{0}%" RotationAngle="0" Skip="0" Step="1">
                                                </LabelsAppearance>
                                                <%--   <TitleAppearance Position="Center" RotationAngle="0" Text="CPU Load">
                                                </TitleAppearance>--%>
                                            </YAxis>
                                            <Series>
                                                <telerik:LineSeries>
                                                    <Appearance>
                                                        <FillStyle BackgroundColor="#5ab7de"></FillStyle>
                                                    </Appearance>
                                                    <LabelsAppearance DataFormatString="{0}%" Position="Above">
                                                    </LabelsAppearance>
                                                    <LineAppearance Width="1" />
                                                    <MarkersAppearance MarkersType="Circle" BackgroundColor="White" Size="8" BorderColor="#5ab7de"
                                                        BorderWidth="2"></MarkersAppearance>
                                                    <TooltipsAppearance DataFormatString="{0}%"></TooltipsAppearance>

                                                </telerik:LineSeries>
                                            </Series>
                                        </PlotArea>
                                    </telerik:RadHtmlChart>

                                </div>

                            </div>

                            <div style="clear: both; height: 20px"></div>

                            <div>

                                <table cellspacing="5">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <div style="text-align: justify; margin-right: 20px; margin-left: 20px;">
                                                    <p>
                                                        <b>TEÓRICO ALTO:</b><br>
                                                        Su interés dominante es la búsqueda de la verdad. Está interesado en un proceso de razonamiento lógico y secuencial. Tiene una alta puntuación mostrándose como intelectual, pone en orden las cosas e interrelaciona todo dentro de un sistema lógico. Más que aceptar las cosas por el valor que aparenten, es objetivo, crítico y busca los hechos. Tiende a preferir ideas o cosas, en lugar de personas. Trata de ordenar y sistematizar el conocimiento con investigación y validación.
                                                    </p>

                                                    <p>
                                                        <b>ECONÓMICO ALTO:</b><br>
                                                        Esta persona se caracteriza por un interés común hacia las ganancias económicas. Ve todos los objetos, ideas y cosas de su medio ambiente como parte de una estructura materialista. Es práctico y tiende a fijar un valor monetario sobre las cosas. Busca la utilidad y la inversión potencial. Tiene un deseo personal de ganancia material y en el sentido del trabajo, tiene una apreciación hacia los resultados positivos como centro de utilidad. Respeta los logros económicos de otros.
                                                    </p>

                                                    <p>
                                                        <b>ESTÉTICO ALTO:</b><br>
                                                        Esta persona busca la belleza artística o la creatividad en áreas de expresión cultural. Busca la forma y la armonía, la gracia y la simetría. Sus percepciones pueden variar desde individualista. Es apto para ser perfeccionista en el diseño, el color y los detalles. Demanda libertad para crear sus propias cosas. Su aguda sensibilidad por lo bello, puede estar acompañada  por su intolerancia a lo feo. Objeta la falta de sensibilidad refinada cuando es vista en un comercialismo grueso. En las ventas puede tener éxito porque esta persona sirve a otros con un interés similar. En la administración, tiende a tener relaciones armoniosas.
                                                    </p>


                                                    <p>
                                                        <b>SOCIAL ALTO:</b><br>
                                                        Este valor implica un sentimiento altruista por la gente, tanto extraños como conocidos. Esta persona busca desinteresadamente mejorar el bienestar de otros sirviéndoles. Procura ayudar a toda clase de personas: quienes están aventajados y/o se sientan maltratados. Sus simpatizantes son impulsados a la acción por un sentido de justicia social. Sus juicios son subjetivos, matizados de emociones e idealismo. La indignación social frecuentemente causa conflictos con el individuo de valores económicos.
                                                    </p>

                                                    <p>
                                                        <b>POLÍTICO ALTO:</b><br>
                                                        Esta persona busca status y poder de una naturaleza individual. Busca ser colocado por encima de otros en la organización jerárquica y estructural. Disfruta siendo influyente y se siente estimulado, más que turbado por el reconocimiento personal. El puntaje político alto, está fuertemente motivado para ganar status, reconocimiento y control. 
                                                    </p>

                                                    <p>
                                                        <b>REGULATORIO ALTO:</b><br>
                                                        Esta persona busca identificarse con una fuerza reconocida por el bien o gobernar su vida por un código de conducta que prometerá aprobación o aceptación por una alta autoridad. Busca unidad en su propio cosmos y una relación con esa totalidad. Lo correcto o incorrecto es importante para esta persona y tiende a hacer juicios morales de conformidad con ellos. Quiere estar en lo correcto. Generalmente tiende a ser cooperativo, controlar y a observar estándares establecidos.
                                                    </p>

                                                    <p>
                                                        <b>TEÓRICO BAJO:</b><br>
                                                        Este individuo se forma opiniones de aspectos o situaciones rápidamente. Siente que sus instintos son correctos y que una gran cantidad de investigación no es requerida. Dominado por la emoción, tiende a aceptar las cosas por su valor aparente. Prefiere tratar más con las emociones de la gente, que con la investigación científica de los hechos. Frecuentemente piensa que el tiempo utilizado en cuestionamientos del cómo y del por qué de una situación, es tiempo perdido. Por esto, prefiere expresar sentimientos y opiniones, más que hechos científicamente validados.                         
                                                    </p>

                                                    <p>
                                                        <b>ECONÓMICO BAJO:</b><br>
                                                        Este individuo muestra un desinterés por las cosas materiales, prefiriendo los conceptos más intangibles de servicios personales y relaciones espirituales. Como tal, las aplicaciones de carrera se inclinan  fuertemente a servir, más que a la explotación de otros para lograr ganancias materiales. Una característica clave de esta persona es ayudar a las víctimas o desvalidos. En el manejo de negocios, tenderá a ser impráctico, careciendo de interés por costos o beneficios. En el puesto de ventas, tiende a ignorar precios o plazos de entrega.
                                                    </p>

                                                    <p>
                                                        <b>ESTETICO BAJO:</b><br>
                                                        Esta persona no está interesada en el gusto o la belleza estética. Tiende a ser práctico y programático. Juzga objetos, cosas o programas por su utilidad, producción o ingreso financiero. Es olvidadizo o distraído respecto a las cualidades de forma, diseño, estilo o clase. Como tal, son considerados por los estetas como seres sencillos y no personas en su totalidad.
                                                    </p>

                                                    <p>
                                                        <b>SOCIAL BAJO:</b><br>
                                                        Este individuo tiende a ser indiferente hacia la gente menesterosa o desamparada, quienes tienen menos bienes materiales o riqueza. Tiende a creer que cada persona obtiene lo que se merece. Carece de compasión por los extraños. Sus valores primarios son poco cambiados y producen predisposiciones y prejuicios con otra gente. 
                                                    </p>

                                                    <p>
                                                        <b>POLÍTICO BAJO:</b><br>
                                                        El ejercicio del poder no vale la pena, debido a las adversidades que uno debe afrontar al ganarlo. Una persona con puntuaciones bajas en esta área está particularmente consciente de los riesgos del manejo del poder y de que requiere de contacto con personas o situaciones no deseables. Sin embargo, está dispuesto a ejercer el liderazgo detrás de escena para ganar una causa.
                                                    </p>

                                                    <p>
                                                        <b>REGULATORIO BAJO:</b><br>
                                                        Este individuo es independiente e individualista. Quiere tomar decisiones independientes a códigos o costumbres. Se resiste a ser cuidadoso y resentirá que otros prescriban por él. Puede interpretar la ley para sus propias necesidades y racionalizar para justificar sus acciones individuales.
                                                    </p>

                                                    <p>
                                                        <b>TEÓRICO - REGULATORIO:</b><br>
                                                        Esta persona tiene juicios morales basados en un fundamento teórico puro. Preocupado por el mantenimiento del orden de su cosmos, esta persona continuamente expondrá sus creencias a un riguroso examen. Cuestionador y lógico, irá detrás del reconocimiento de un orden existente para  preguntar ¿Por qué?. Es generalmente un individuo cooperativo y disciplinado, guiado por un profundo sentido de moralidad.
                                                    </p>

                                                    <p>
                                                        <b>POLÍTICO - ECONÓMICO:</b><br>
                                                        Esta persona disfruta estando a cargo de las empresas, pues disfruta ejerciendo su autoridad y dirigiendo a otros hacia fines o resultados. También disfruta la naturaleza competitiva de los negocios, luchando duro para ganar. Dotado de una alta dosis de pragmatismo y de un agudo sentido de los valores, concentra sus recursos donde tiene más posibilidades de ganar. Usualmente disfruta del reconocimiento y el status y puede encontrar maneras tangibles de publicar su posición por medio de símbolos de status como automóviles caros y oficinas grandes y bien amuebladas.
                                                    </p>

                                                    <p>
                                                        <b>ARTÍSTICO - ESTÉTICO:</b><br>
                                                        Busca la belleza artística o la creatividad en áreas de expresión cultural. Busca la forma y la armonía, la gracia y la simetría. Sus percepciones pueden variar. Esta persona es apta para ser perfeccionista en el diseño, el color y los detalles. Demanda libertad para crear sus propias cosas. Su aguda sensibilidad por lo bello, puede estar acompañada por su intolerancia a lo feo. Objeta la falta de sensibilidad refinada cuando es vista en un comercialismo grueso.
                                                    </p>

                                                    <p>
                                                        <b>TEÓRICO - ECONÓMICO:</b><br>
                                                        Esta persona disfruta descubriendo ideas nuevas y útiles. Cae dentro del mundo de la ciencia o de los conceptos. Este estilo se refuerza por encontrar aplicaciones prácticas y útiles para sus ideas. Aplica frecuentemente sistemas lógicos a la administración, esforzándose por hacer una ciencia más que un arte. Mientras esta persona es frecuentemente muy creativa, usualmente canaliza su creatividad hacia lo tangible y lo objetivo y la mayoría de las veces mide sus éxitos en pesos y centavos.
                                                    </p>

                                                    <p>
                                                        <b>TEÓRICO - POLÍTICO:</b><br>
                                                        Esta persona está enfocada al éxito y a tener influencia sobre los otros. Utiliza el conocimiento y el cálculo de opciones para favorecer sus ambiciones. Es personalmente ambicioso, atraído por el mundo de la razón y de los hechos. Puede parecer no estar consciente de las consecuencias sociales de sus actos. Por esta razón puede ser muy efectivo en situaciones que requieren de un razonamiento frío asociado con acciones firmes y positivas. Usualmente son respetados por su fuerza ante la adversidad y su habilidad para pensar bajo presión.
                                                    </p>

                                                    <p>
                                                        <b>TEÓRICO - SOCIAL:</b><br>
                                                        Este individuo tiende a ser altruista y desinteresado, pues busca ayudar a su prójimo a través del uso del conocimiento y la razón. Convencido de que el conocimiento y el entendimiento puede proporcionar las mejores soluciones a los problemas, esta persona propone sistemas y lógica para enfrentar la mayoría de los problemas. Su deseo de ayudar a otros, va acompañado de una curiosidad innata, llevándole a confiar en la investigación y en la aplicación de métodos científicos para la solución de problemas humanos.
                                                    </p>

                                                    <p>
                                                        <b>ECONÓMICO - REGULATORIO:</b><br>
                                                        Tiende a guiarse por principios y un estricto sentido del orden. Motivado a maximizar beneficios, aplica sistemas y disciplinas que monitorean y controlan la actividad económica. Tiende a creer que toda la actividad de la empresa debe ser guiada por un plan o un grupo de estándares y principios predeterminados que sirvan como modelo para la acción. Aunque es generalmente cooperativo, puede revelarse a algo que contradice su noción de lo que es correcto. 
                                                    </p>

                                                    <p>
                                                        <b>ESTÉTICO - POLÍTICO:</b><br>
                                                        Este individuo puede sentirse dividido en ocasiones entre su deseo de poder y su deseo de paz y armonía. Tiende a ser sensitivo al color, la luz, el sonido y la forma, y quizá especialmente talentoso en el empleo de los mismos para expresar sus pensamientos y sus sentimientos. Usualmente ambicioso, empujará hasta llegar a la cumbre de su campo o para asegurar que sus enfoques o puntos de vista sean los ganadores del día.
                                                    </p>

                                                    <p>
                                                        <b>TEÓRICO - ESTÉTICO:</b><br>
                                                        Este individuo es generalmente gracioso y armonioso. Es reflexivo y naturalmente inquisitivo, muestra una alta sensibilidad al color, el diseño, el balance y la forma. El idealismo caracteriza a este estilo, que es devoto de la armonía y la belleza, así como de la dedicación a aprender la verdad. Frecuentemente trabaja con facilidad, pues usa su conocimiento para preservar la existencia de balance o crear un nuevo orden en el nombre de la armonía o la belleza.
                                                    </p>

                                                    <p>
                                                        <b>ECONÓMICO - ESTÉTICO:</b><br>
                                                        Este individuo tiende a ser armonioso, busca un ambiente sereno y elegante. Tiende a coleccionar objetos bellos de valor. Aprecia la gracia y la belleza de las cosas, pero también busca algo de utilidad. Para esta persona, la belleza por sí misma es fría. Sin embargo, toda la vida es más rica cuando la belleza es parte de ella. Generalmente es sensitivo al color, la forma y los sonidos y se esfuerza por lograr el balance y la armonía. De este modo, sus relaciones con otros son usualmente positivas y pacíficas.
                                                    </p>

                                                    <p>
                                                        <b>ECONÓMICO - SOCIAL:</b><br>
                                                        Este individuo tiende a seguir la creencia de proveer el máximo beneficio, por el máximo número y el menor costo. De este modo, extiende sus recursos hasta el límite para promover el bienestar social. Tiene un buen sentido del valor y se inclina hacia el manejo o solución de problemas de tipo práctico. No obstante, es sensitivo y se preocupa por la gente. Frecuentemente dedica su tiempo y energía desinteresadamente a ayudar a otros.
                                                    </p>

                                                    <p>
                                                        <b>ESTÉTICO - SOCIAL:</b><br>
                                                        Este individuo tiende a buscar la expresión artística de sus inquietudes o amor hacia los demás. Generalmente es sensitivo y gracioso, por lo que establece contacto con los demás de una manera cálida y personal, llegándose a ver envuelto en el caos de la vida de otros. Altamente armonizante con la belleza de su mundo, ve como una fuerte de felicidad que disfruta al participar con otra gente.
                                                    </p>

                                                    <p>
                                                        <b>ESTÉTICO - REGULATORIO:</b><br>
                                                        Este individuo es sereno y disciplinado, aprecia el balance y el orden de su universo. Busca la forma, la gracia y la simetría. Es cooperativo y auto controlado, tiende a gobernarse así mismo por un profundo sentido de lo correcto y lo incorrecto. Se identifica con una fuerza reconocida del bien y busca la unidad en el cosmos.
                                                    </p>

                                                    <p>
                                                        <b>POLÍTICO - SOCIAL:</b><br>
                                                        Este individuo es activista social, el campeón de los desvalidos. Profundamente sensitivo al sufrimiento humano, este individuo se siente impulsado a tomar la acción, hacer cambios y corregir errores. Es ambicioso y disfruta teniendo el poder y el control para imponer sus ideas. Generalmente interesado en promover el bienestar de otros. Compite agresivamente por su causa. Tales personas están listas para comprometerse en una controversia para favorecer sus objetivos.
                                                    </p>

                                                    <p>
                                                        <b>SOCIAL - REGULATORIO:</b><br>
                                                        Este individuo tiende a ser ordenado, así como cálido y amistoso. Generalmente es un individuo guiado por la ética y por un profundo sentido de lo correcto o lo incorrecto. Tiende a poner los problemas humanos en la perspectiva de algún orden cósmico. Motivado a promover la armonía y a ser cooperativo, se resistirá a cualquier acción que afecte sus creencias o estándares.
                                                    </p>

                                                    <p>
                                                        <b>POLÍTICO - REGULATORIO:</b><br>
                                                        Este individuo busca status e imponer sus ideas de acuerdo a lo correcto o incorrecto. Tiende a gobernarse a sí mismo por un código de conducta en obediencia y una alta autoridad. Busca unidad en su propio cosmos y una relación con esta totalidad. Busca control sobre otros, tratando de hacerlo desde su propio punto de vista acerca de lo que el mundo debería ser. Dispuesto a pelear por sus  creencias, desafía cualquier oponente en el nombre de lo moralmente justo y distingue el bien del mal, luchando siempre del lado del bien.
                                                    </p>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>

                            </div>
                        </div>
                         <div class="divControlDerecha">
                                <telerik:RadButton ID="btnImprimirInteres" runat="server" AutoPostBack="false" Text="Imprimir" OnClientClicked="OpenInteres"></telerik:RadButton>
                            </div>
                    </telerik:RadPageView>

                    <telerik:RadPageView ID="RPVPensamiento" runat="server">
                        <div>
                            <div class="ctrlBasico">
                                <div>
                                    <div style="position:absolute;" id="dynamicAttacher"></div>
                                    <div>
                                        <img src="../Assets/images/EstiloPensamiento.jpg"
                                           style="position:absolute; z-index: -1; padding-left:20px" alt="Gráfica" />
                                        <br />
                                        <asp:Literal ID="litTextos" runat="server"></asp:Literal>
                                    </div>

                                    <%-- <telerik:RadHtmlChart runat="server" ID="rhcPenslamiento" Width="500px" Height="500px" Transitions="true" Skin="Silk">
                                        <Appearance>
                                            <FillStyle BackgroundColor="Transparent"></FillStyle>
                                        </Appearance>
                                        <Legend>
                                            <Appearance BackgroundColor="Transparent" Position="Bottom">
                                            </Appearance>
                                        </Legend>
                                        <PlotArea>
                                            <Appearance>
                                                <FillStyle BackgroundColor="Transparent"></FillStyle>
                                            </Appearance>

                                            <XAxis Color="Black" MajorTickType="Outside" MinorTickType="Outside" Reversed="false" StartAngle="180">
                                                <LabelsAppearance DataFormatString="{0}" RotationAngle="0" Step="1" Skip="0">
                                                </LabelsAppearance>
                                                <MajorGridLines Color="#c8c8c8" Width="1"></MajorGridLines>
                                                <MinorGridLines Visible="false"></MinorGridLines>
                                                <Items>
                                                    <telerik:AxisItem LabelText="Visión"></telerik:AxisItem>
                                                    <telerik:AxisItem LabelText="Derecha"></telerik:AxisItem>
                                                    <telerik:AxisItem LabelText="Intuición"></telerik:AxisItem>
                                                    <telerik:AxisItem LabelText="Específica"></telerik:AxisItem>
                                                    <telerik:AxisItem LabelText="Lógica"></telerik:AxisItem>
                                                    <telerik:AxisItem LabelText="Izquierda"></telerik:AxisItem>
                                                    <telerik:AxisItem LabelText="Análisis"></telerik:AxisItem>
                                                    <telerik:AxisItem LabelText="Conceptual"></telerik:AxisItem>
                                                </Items>
                                            </XAxis>

                                            <YAxis Color="black" MajorTickSize="1" MajorTickType="Outside" Type="Numeric"
                                                MaxValue="100" MinorTickSize="1" MinorTickType="Outside" MinValue="0" Reversed="false" Step="20">
                                                <LabelsAppearance DataFormatString="{0}%" RotationAngle="0" Skip="0" Step="1"></LabelsAppearance>
                                                <MajorGridLines Color="#c8c8c8" Width="1" />
                                                <MinorGridLines Visible="true" />
                                            </YAxis>

                                            <Series>
                                                <telerik:RadarLineSeries DataFieldY="NO_VALOR" MissingValues="Interpolate">
                                                    <Appearance>
                                                        <FillStyle BackgroundColor="#5ab7de"></FillStyle>
                                                    </Appearance>
                                                    <LabelsAppearance DataFormatString="{0}%" Position="Above">
                                                    </LabelsAppearance>
                                                    <LineAppearance Width="1" />
                                                    <MarkersAppearance MarkersType="Circle" BackgroundColor="White" Size="8" BorderColor="#5ab7de"
                                                        BorderWidth="2"></MarkersAppearance>
                                                    <TooltipsAppearance DataFormatString="{0}%"></TooltipsAppearance>
                                                </telerik:RadarLineSeries>
                                            </Series>
                                        </PlotArea>
                                    </telerik:RadHtmlChart>--%>
                                </div>
                            </div>

                            <div class="divControlDerecha" style=" position:absolute; padding-left:650px; height:400px; ">
                                <telerik:RadGrid ID="grdEstiloPensamiento" ShowHeader="false" runat="server" GridLines="None" AutoGenerateColumns="false">
                                    <MasterTableView>
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderText="" DataField="NB_TITULO" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="" DataField="VALOR" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <div style="clear: both"></div>
                            <div style="clear: both; height: 450px"></div>
                            <div style="position:absolute;">

                                <table cellspacing="5">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <p style="text-align: justify; margin-left: 20px; margin-right: 20px;">
                                                    <b>Visión</b><br>
                                                    Esta persona es un soñador de mente abierta en el sentido positivo de la palabra.
            Posiblemente es un pensador que usa la gestalt para visualizar la imagen panorámica.
            Tiende a sintetizar la información que le llega de diversas fuentes para crear nuevos conceptos.
            Esta persona está dispuesta a arriesgarse, experimentando con ideas y relaciones hasta descubrir la comunicación correcta.
            Se llega al estado de lo correcto intuitivamente y con frecuencia es un estado que no se puede explicar.
            El aprendizaje es de tipo visual, pictorial y experiencial, generalmente sin análisis y por lo tanto, se capta una compresión
            superficial del asunto aunque le falta detalle. Su habla puede carecer de sintaxis y brincar de un pensamiento a otro,
            de inspiración a inspiración.
                                                </p>
                                                <br />
                                                <p style="text-align: justify; margin-left: 20px; margin-right: 20px;">
                                                    <b>Lógica</b><br>
                                                    Su estilo de pensamiento está vinculado con dar forma, conservar los recursos, organizar y administrar
            recursos de manera conservadora. Posee un pensamiento normativo, trae el orden a su ambiente. Los problemas son
            abordados como un recetario de cocina, las soluciones con frecuencia se basan en su instinto y se apoya en la lógica.
            Esta persona exhibe una forma de pensar conservadora y el comportamiento adecuado. Los hechos, las cifras y el detalle.
            Es gente que piensa como un capataz exacto y preciso, recolectando datos, prescribiendo métodos y ayudando a llevar la cuenta.
            Esta persona tiene un sexto sentido de lo correcto. Su habla es articulada y se centra en los datos, procesos,
            procedimientos y tareas más que en los sentimientos y la gente.
                                                </p>
                                                <br />
                                                <p style="text-align: justify; margin-left: 20px; margin-right: 20px;">
                                                    <b>Intuición</b><br>
                                                    Es un pensador intuitivo, emotivo y centrado en los sentimientos. Para él los sentimientos son hechos:
            los sentimientos son más importantes que la llamada prueba científica. Esta persona sabe instintivamente e intuitivamente,
            sin lógica o razonamiento. Tiene un radar social que le ayuda a juzgar el tenor de las relaciones sociales o interpersonales.
            Debido a esta elevada conciencia o responsabilidad empática esta persona con frecuencia emigra hacia otros puestos que
            tienen mucho involucramiento con la gente. Sus conversaciones están orientadas emocionalmente y son perceptivas.
            Tiene un sentido intuitivo de la armonía y evita los conflictos que puedan afectar la retroinformación que necesita
            de los demás. Es un pensador que aprende por observación y no tiende a disecar y reacomodar la información.
            La información se acepta como se presenta así como es el mundo. Esta persona se sintoniza con su ambiente.
                                                </p>

                                                <br />
                                                <p style="text-align: justify; margin-left: 20px; margin-right: 20px;">
                                                    <b>Analítico</b><br>
                                                    Lógico, analítico, objetivo. Este estilo de pensamiento se alimenta de la información técnica y de los hechos.
            La toma de decisiones se  basa en una razón sin emotividad y en los hechos relevantes. La persona que exhibe este
            tipo de pensamiento analítico tiende a sospechar de las personas y sistemas que carecen de validación científica.
            Aprende mediante el análisis de los conceptos y la información, examinando críticamente a cada componente.
            Su habla se mueve ordenadamente de punto a punto; los demás con frecuencia etiquetan a esta persona de intelectual.
            La cuantificación, las pruebas, la lógica, la matemática, etc. son cosas sumamente valorizadas por estas personas.
            Tienden a  alegar, como medio para extraer información de los demás. Prefiere que la información se le presente verbalmente.
                                                </p>
                                                <br />
                                                <p style="text-align: justify; margin-left: 20px; margin-right: 20px;">
                                                    <b>Analítico - Visión</b><br>
                                                    Esta persona tiene una marcada preferencia  por el pensamiento cognoscitivo, teórico e intelectual y evita el estilo 
            de procesamiento visceral, afectivo y emocional.  Dependiendo de la situación, esta persona tendrá una preferencia 
            igual por la información que involucre probabilidades, hechos, datos, lógica, conceptos, síntesis e intuición. 
            Tiene un  gran respeto  por las teorías, las ideas y la lógica pero poco respeto a lo estructurado, controlado, 
            planeado al detalle, normativo e interpersonal. En él es característico un procesamiento mental emocional – experiencial.
                                                </p>
                                                <br />
                                                <p style="text-align: justify; margin-left: 20px; margin-right: 20px;">
                                                    <b>Análisis - Lógico</b><br>
                                                    Tiene una marcada preferencia por el pensamiento del cerebro izquierdo, esto lo conduce a concentrarse en datos,
            hechos y tareas. Los procesos de pensamiento fluyen lógicamente de punto a punto, en contraste con las personas
            de cerebro derecho cuyos pensamientos brincan de una idea a otra. La confianza en las políticas y los procedimientos
            se combinan con el análisis, la cuantificación, la programación y la toma lógica de decisiones. Los procesos
            de pensamiento  están basados en emociones, sentimientos y síntesis. Es un pensador bien organizado y disciplinado,
            efectivo en la administración de sistemas y disciplinas técnicas o científicas.
                                                </p>
                                                <br />
                                                <p style="text-align: justify; margin-left: 20px; margin-right: 20px;">
                                                    <b>Visión - Intuición</b><br>
                                                    La gente que tiene una marcada preferencia hacia el cerebro derecho tiende a depender de los procesos de pensamiento 
            visuales, espaciales, intuitivos, conceptuales y sensibles. Es creativo, de mente abierta, experimental y holístico 
            lo que ayuda en la planeación de largo plazo y a ver la imagen panorámica. Disfruta de las experiencias interpersonales, 
            emocionales, espirituales y musicales.  Las funciones analíticas de pensamiento lógico requeridas por las funciones de 
            investigación o contabilidad serán percibidas por esta persona como limitantes de sus abordajes preferidos de tipo exploratorio 
            y experimental. Esta persona tiende a soñar las ideas y dejar su implementación  o el análisis de costo / beneficio a las 
            personas de cerebro izquierdo.
                                                </p>
                                                <br />
                                                <p style="text-align: justify; margin-left: 20px; margin-right: 20px;">
                                                    <b>Visión - Lógica</b><br>
                                                    Trabaja ambos hemisferios cerebrales de diferente forma.  Busca organizar, conservar y administrar adecuadamente 
            los recursos de manera  conservadora. Trae el orden a su ambiente.  Se basa con frecuencia en su instinto y se apoya 
            en la lógica. Es de mente abierta y soñador en el sentido positivo de la palabra, tiende a sistematizar la información 
            que le llega de diversas fuentes para crear nuevos conceptos. Esta persona está dispuesta a experimentar con ideas nuevas 
            y relacionar hasta lograr la combinación correcta. El aprendizaje es de tipo visual y experiencial. Los hechos, las cifras 
            y el detalle son importantes. Piensa como un capataz exacto y preciso, recolectando datos, prescribiendo métodos y 
            ayudando a llevar la cuenta.
                                                </p>
                                                <br />
                                                <p style="text-align: justify; margin-left: 20px; margin-right: 20px;">
                                                    <b>Intuición - Lógica</b><br>
                                                    Esta persona tiene un énfasis dominante en la región afectiva y visceral del cerebro, esta persona exhibe distinta 
            preferencia por el orden, la seguridad y los sistemas así como también lo interpersonal. Expresa vulnerabilidad 
            emocional. La toma de decisiones se basa en el instinto y / o la intuición que opaca la lógica y la razón, o a una 
            integración e innovación de mente más abierta. Los procesos de pensamiento que esta persona evita son la gestalt, 
            el holismo, la creatividad, el análisis y el examen crítico. Se ocupa mucho de los asuntos concretos, es decir, 
            las estructuras y los sentimientos. Los planes y la gente.
                                                </p>
                                                <br />
                                                <p style="text-align: justify; margin-left: 20px; margin-right: 20px;">
                                                    <b>Análisis - Lógica - Intuición</b><br>
                                                    Esta persona tiene una marcada preferencia por el pensamiento del cerebro izquierdo, esto lo conduce a concentrarse 
            en datos, hechos y tareas. Los procesos de pensamiento fluyen lógicamente de punto a punto, en contraste con las 
            personas de cerebro derecho, sin embargo esta persona ha desarrollado la cualidad de la intuición por lo que todo 
            análisis será juzgado por su intuición como un radar social que le ayuda a juzgar el tenor de las relaciones 
            sociales o interpersonales. Debido a esta elevada conciencia o responsabilidad empática, esta persona con frecuencia 
            emigra hacia puestos que tienen mucho involucramiento con la gente.
                                                </p>
                                                <br />
                                                <p style="text-align: justify; margin-left: 20px; margin-right: 20px;">
                                                    <b>Análisis - Visión - Lógica - Intuición</b><br>
                                                    Esta persona usa los cuatro cuadrantes de cerebro con la misma facilidad. Su preferencia se acomoda a la situación 
            que tiene enfrente de él, de manera que pueda analizar los conceptos con la misma facilidad con que puede 
            organizar y sintonizarse con los sentimientos de las personas involucradas. Esta persona puede soñar una visión y 
            llevarla a rendir frutos. Es percibido por la mayoría de la gente como verdaderamente ingenioso y creativo. 
            Con frecuencia se dice de esta persona: “si tienes un problema, llévaselo a...”.
                                                </p>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                 <div style="clear: both"></div>
                         <div class="divControlDerecha" style="padding-right:25px;">
                                <telerik:RadButton ID="btnImprimeEstilo" runat="server" AutoPostBack="false" Text="Imprimir" OnClientClicked="OpenEstilo"></telerik:RadButton>
                            </div>
                            </div>
                           
                        </div>
            
                    </telerik:RadPageView>

                    <telerik:RadPageView ID="RPVAptitudI" runat="server">
                        <div style="clear: both; height: 20px"></div>
                        <div>
                            <div class="ctrlBasico" style="margin-left: 20px;">
                                <div>
                                    <telerik:RadHtmlChart runat="server" ID="ChartAptitudMental1" Width="600px" Height="400px">
                                        <ChartTitle Text="">
                                        </ChartTitle>
                                        <PlotArea>
                                            <Appearance>
                                                <FillStyle BackgroundColor="Transparent"></FillStyle>
                                            </Appearance>
                                            <XAxis AxisCrossingValue="0" Color="black" MajorTickType="Outside" MinorTickType="Outside"
                                                Reversed="false">
                                                <Items>
                                                    <telerik:AxisItem LabelText="Conocimientos"></telerik:AxisItem>
                                                    <telerik:AxisItem LabelText="Comprensión"></telerik:AxisItem>
                                                    <telerik:AxisItem LabelText="Significados verbales"></telerik:AxisItem>
                                                    <telerik:AxisItem LabelText="Selección lógica"></telerik:AxisItem>
                                                    <telerik:AxisItem LabelText="Aritmética"></telerik:AxisItem>
                                                    <telerik:AxisItem LabelText="Juicio práctico"></telerik:AxisItem>
                                                    <telerik:AxisItem LabelText="Analogías"></telerik:AxisItem>
                                                    <telerik:AxisItem LabelText="Ordenamiento de frases"></telerik:AxisItem>
                                                    <telerik:AxisItem LabelText="Clasificación"></telerik:AxisItem>
                                                    <telerik:AxisItem LabelText="Seriación"></telerik:AxisItem>
                                                </Items>
                                                <LabelsAppearance DataFormatString="{0}"  RotationAngle="280" Skip="0" Step="1">
                                                </LabelsAppearance>
                                                <%--  <TitleAppearance Position="Center" RotationAngle="0" Text="Days">
                                                </TitleAppearance>--%>
                                            </XAxis>
                                            <YAxis AxisCrossingValue="0" Color="black" MajorTickSize="1" MajorTickType="Outside"
                                                MinorTickSize="1" MinorTickType="Outside" MinValue="0" Reversed="false"
                                                Step="25" MaxValue="100">
                                                <LabelsAppearance DataFormatString="{0}%" RotationAngle="0" Skip="0" Step="1">
                                                </LabelsAppearance>

                                            </YAxis>

                                            <Series>
                                                <telerik:ColumnSeries>
                                                    <TooltipsAppearance Color="White" />
                                                </telerik:ColumnSeries>
                                            </Series>
                                        </PlotArea>
                                        <Legend>
                                            <Appearance Visible="false" />
                                        </Legend>
                                    </telerik:RadHtmlChart>
                                </div>

                            </div>
                            <div class="ctrlBasico" style="margin-left: 20px;">

                                <telerik:RadGrid ID="grdAptitudI" ShowHeader="false" runat="server" GridLines="None" AutoGenerateColumns="false">
                                    <MasterTableView>
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderText="" DataField="NB_TITULO" HeaderStyle-Width="140"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="" DataField="VALOR" HeaderStyle-Width="140"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <div style="clear: both"></div>
                        </div>
                         <div class="divControlDerecha">
                                <telerik:RadButton ID="btnMental1" runat="server" AutoPostBack="false" Text="Imprimir" OnClientClicked="OpenAptitud1"></telerik:RadButton>
                            </div>
                    </telerik:RadPageView>

                    <telerik:RadPageView ID="RPVAptitudII" runat="server">

                        <div class="ctrlBasico" style="margin-left: 20px;">
                            <telerik:RadGrid ID="grdMentalII" ShowHeader="false" runat="server" GridLines="None" AutoGenerateColumns="false">
                                <MasterTableView>
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderText="" DataField="NB_TITULO" HeaderStyle-Width="200"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="" DataField="VALOR" HeaderStyle-Width="200"></telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>

                        <div style="clear: both; height: 20px"></div>

                        <div class="ctrlBasico" style="padding-left: 20px;">
                            <div>
                                <div>
                                    <label style="color: #800000">Magnitud de problemas que puede resolver de forma satisfactoria: Inteligencia</label>
                                </div>
                                <div style="clear: both; height: 10px"></div>

                                <div style="position: relative">
                                    <div id="c11" runat="server" class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 45px; margin-right: 10px; width: 100px; text-align: center; line-height: 43px; margin-top: 5px">
                                        Simples 
                                        <div style="clear: both"></div>
                                        <div style="text-align: center; font-size: 12px;">-79</div>
                                    </div>
                                    <div id="c12" runat="server" class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 45px; margin-right: 10px; width: 100px; text-align: center; line-height: 43px; margin-top: 5px">
                                        Comunes 
                                        <div style="clear: both"></div>
                                        <div style="text-align: center; font-size: 12px;">80-89</div>
                                    </div>
                                    <div id="c13" runat="server" class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 45px; margin-right: 10px; width: 100px; text-align: center; margin-top: 5px">
                                        Término medio 
                                        <div style="clear: both"></div>
                                        <div style="text-align: center; font-size: 12px; line-height: 49px;">90-109</div>
                                    </div>
                                    <div id="c14" runat="server" class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 45px; margin-right: 10px; width: 100px; text-align: center; line-height: 43px; margin-top: 5px">
                                        Difíciles
                                        <div style="clear: both"></div>
                                        <div style="text-align: center; font-size: 12px;">110-119</div>
                                    </div>
                                    <div id="c15" runat="server" class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 45px; margin-right: 10px; width: 100px; text-align: center; margin-top: 5px">
                                        De lo más complejo  
                                        <div style="clear: both"></div>
                                        <div style="text-align: center; font-size: 12px; line-height: 49px;">120+</div>
                                    </div>
                                    <div style="clear: both"></div>
                                </div>

                            </div>

                            <div style="clear: both; height: 40px"></div>

                            <div>
                                <div>
                                    <label style="color: #800000">Comparativamente es más inteligente que este porcentaje de las personas</label>
                                </div>
                                <div style="clear: both; height: 10px"></div>

                                <div style="position: relative">
                                    <div id="c21" runat="server" class="ctrlBasico sombra" style="border: 1px solid #ddd; line-height: 40px; height: 40px; margin-right: 15px; width: 45px; text-align: center; margin-top: 5px">
                                        10 
                                        <div style="clear: both"></div>
                                        <div style="text-align: center; font-size: 12px;">-69</div>
                                    </div>
                                    <div id="c22" runat="server" class="ctrlBasico sombra" style="border: 1px solid #ddd; line-height: 40px; height: 40px; margin-right: 15px; width: 45px; text-align: center; margin-top: 5px">
                                        10 
                                        <div style="clear: both"></div>
                                        <div style="text-align: center; font-size: 12px;">70-89</div>
                                    </div>
                                    <div id="c23" runat="server" class="ctrlBasico sombra" style="border: 1px solid #ddd; line-height: 40px; height: 40px; margin-right: 15px; width: 45px; text-align: center; margin-top: 5px">
                                        20 
                                        <div style="clear: both"></div>
                                        <div style="text-align: center; font-size: 12px;">80-84</div>
                                    </div>
                                    <div id="c24" runat="server" class="ctrlBasico sombra" style="border: 1px solid #ddd; line-height: 40px; height: 40px; margin-right: 15px; width: 45px; text-align: center; margin-top: 5px">
                                        30
                                        <div style="clear: both"></div>
                                        <div style="text-align: center; font-size: 12px;">85-90</div>
                                    </div>
                                    <div id="c25" runat="server" class="ctrlBasico sombra" style="border: 1px solid #ddd; line-height: 40px; height: 40px; margin-right: 15px; width: 45px; text-align: center; margin-top: 5px">
                                        40  
                                        <div style="clear: both"></div>
                                        <div style="text-align: center; font-size: 12px;">90-99</div>
                                    </div>
                                    <div id="c26" runat="server" class="ctrlBasico sombra" style="border: 1px solid #ddd; line-height: 40px; height: 40px; margin-right: 15px; width: 60px; text-align: center; margin-top: 5px">
                                        50  
                                        <div style="clear: both"></div>
                                        <div style="text-align: center; font-size: 12px;">100-109</div>
                                    </div>
                                    <div id="c27" runat="server" class="ctrlBasico sombra" style="border: 1px solid #ddd; line-height: 40px; height: 40px; margin-right: 15px; width: 60px; text-align: center; margin-top: 5px">
                                        60  
                                        <div style="clear: both"></div>
                                        <div style="text-align: center; font-size: 12px;">110-114</div>
                                    </div>
                                    <div id="c28" runat="server" class="ctrlBasico sombra" style="border: 1px solid #ddd; line-height: 40px; height: 40px; margin-right: 15px; width: 60px; text-align: center; margin-top: 5px">
                                        70  
                                        <div style="clear: both"></div>
                                        <div style="text-align: center; font-size: 12px;">115-119</div>
                                    </div>
                                    <div id="c29" runat="server" class="ctrlBasico sombra" style="border: 1px solid #ddd; line-height: 40px; height: 40px; margin-right: 15px; width: 60px; text-align: center; margin-top: 5px">
                                        80  
                                        <div style="clear: both"></div>
                                        <div style="text-align: center; font-size: 12px;">120-124</div>
                                    </div>
                                    <div id="c210" runat="server" class="ctrlBasico sombra" style="border: 1px solid #ddd; line-height: 40px; height: 40px; margin-right: 15px; width: 40px; text-align: center; margin-top: 5px">
                                        90  
                                        <div style="clear: both"></div>
                                        <div style="text-align: center; font-size: 12px;">125+</div>
                                    </div>
                                    <div style="clear: both"></div>
                                </div>

                            </div>
                            <div style="clear: both; height: 50px"></div>
                            <div>
                                <div>
                                    <label style="color: #800000">Su forma de resolver problemas </label>
                                </div>
                                <div style="clear: both; height: 10px"></div>

                                <div style="position: relative">
                                    <div id="c31" runat="server" class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 45px; margin-right: 10px; width: 100px; text-align: center; margin-top: 5px">
                                        Sin orden Disperso 
                                        <div style="clear: both"></div>
                                        <div style="text-align: center; font-size: 12px; line-height: 49px;">9+</div>
                                    </div>
                                    <div id="c32" runat="server" class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 45px; margin-right: 10px; width: 100px; text-align: center; margin-top: 5px">
                                        Tiende a ser Disperso 
                                        <div style="clear: both"></div>
                                        <div style="text-align: center; font-size: 12px; line-height: 49px;">6-8</div>
                                    </div>
                                    <div id="c33" runat="server" class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 45px; margin-right: 10px; width: 100px; text-align: center; margin-top: 5px">
                                        Ningún extremo 
                                        <div style="clear: both"></div>
                                        <div style="text-align: center; font-size: 12px; line-height: 49px;">3-5</div>
                                    </div>
                                    <div id="c34" runat="server" class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 45px; margin-right: 10px; width: 100px; text-align: center; margin-top: 5px">
                                        Tiende a ser metódico
                                        <div style="clear: both"></div>
                                        <div style="text-align: center; font-size: 12px; line-height: 49px;">1-2</div>
                                    </div>
                                    <div id="c35" runat="server" class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 45px; margin-right: 10px; width: 100px; text-align: center; margin-top: 5px">
                                        Metódico organizado  
                                        <div style="clear: both"></div>
                                        <div style="text-align: center; font-size: 12px; line-height: 49px;">0</div>
                                    </div>
                                    <div style="clear: both"></div>
                                </div>

                            </div>


                            <div style="clear: both; height: 50px" ></div>

                            <div>
                                <div>
                                    <label style="color: #800000">Su velocidad en tareas mentales </label>
                                </div>
                                <div style="clear: both; height: 10px"></div>

                                <div style="position: relative">
                                    <div id="c41" runat="server" class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 59px; margin-right: 10px; width: 100px; text-align: center; margin-top: 5px">
                                        39
                                        <br />
                                        Muy lento 
                                        <div style="clear: both"></div>
                                        <div style="text-align: center; font-size: 12px; line-height: 81px;">30 min</div>
                                    </div>
                                    <div id="c42" runat="server" class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 59px; margin-right: 10px; width: 100px; text-align: center; margin-top: 5px">
                                        40-59
                                        <br />
                                        Lento 
                                        <div style="clear: both"></div>
                                        <div style="text-align: center; font-size: 12px; line-height: 81px;">30 min</div>
                                    </div>
                                    <div id="c43" runat="server" class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 59px; margin-right: 10px; width: 100px; text-align: center; margin-top: 5px">
                                        60-74
                                        <br />
                                        Ningún extremo 
                                        <div style="clear: both"></div>
                                        <div style="text-align: center; font-size: 12px; line-height: 42px;">30 min</div>
                                    </div>
                                    <div id="c44" runat="server" class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 59px; margin-right: 10px; width: 100px; text-align: center; margin-top: 5px">
                                        75
                                        <br />
                                        Rápido
                                        <div style="clear: both"></div>
                                        <div style="text-align: center; font-size: 12px; line-height: 81px;">30 min</div>
                                    </div>
                                    <div id="c45" runat="server" class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 59px; margin-right: 10px; width: 100px; text-align: center; margin-top: 5px">
                                        >75
                                        <br />
                                        Muy rápido  
                                        <div style="clear: both"></div>
                                        <div style="text-align: center; font-size: 12px; line-height: 81px;">25 min</div>
                                    </div>
                                    <div style="clear: both"></div>
                                </div>

                            </div>

                        </div>

                        <%--<div style="width:200px; height:50px; border: 1px solid red;"></div>--%>

                        <div class="ctrlBasico" style="margin-left: 20px; margin-right: 20px; margin-top:50px;">

                            <div style="text-align: center; margin: 0 auto">
                                <div style="clear: both; height: 10px;"></div>
                                <telerik:RadHtmlChart runat="server" ID="rhcAptitud2" Width="600px" Height="400px">
                                    <ChartTitle Text="">
                                    </ChartTitle>
                                    <PlotArea>
                                        <XAxis Color="Black" MajorTickType="Outside" MinorTickType="Outside" Reversed="false" StartAngle="180">
                                            <LabelsAppearance DataFormatString="{0}" RotationAngle="280" Step="1" Skip="0">
                                            </LabelsAppearance>
                                            <MajorGridLines Color="#c8c8c8" Width="1"></MajorGridLines>
                                            <MinorGridLines Visible="false"></MinorGridLines>

                                            <Items>
                                                <telerik:AxisItem LabelText="Conocimientos"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="Comprensión"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="Significados verbales"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="Selección logica"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="Aritmética"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="Juicio práctico"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="Analogias"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="Ordenamiento de frases"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="Clasificación"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="Seriación"></telerik:AxisItem>
                                            </Items>

                                        </XAxis>
                                        <YAxis AxisCrossingValue="0" Color="black" MajorTickSize="1" MajorTickType="Outside"
                                                MinorTickSize="1" MinorTickType="Outside" MinValue="0" Reversed="false" Step="25" MaxValue="100"
                                                >
                                                <LabelsAppearance DataFormatString="{0}%" RotationAngle="0" Skip="0" Step="1">
                                                </LabelsAppearance>

                                            </YAxis>

                                    </PlotArea>
                                    <Legend>
                                        <Appearance Visible="false" />
                                    </Legend>
                                </telerik:RadHtmlChart>

                            </div>

                        </div>
                        <div style="clear: both"></div>
                        <div class="divControlDerecha">
                                <telerik:RadButton ID="btnMental2" runat="server" AutoPostBack="false" Text="Imprimir" OnClientClicked="OpenAptitud2"></telerik:RadButton>
                            </div>
                    </telerik:RadPageView>

                    <telerik:RadPageView ID="RPVLaboralII" runat="server">
                        <div style="clear: both; height: 10px;"></div>


                        <!-- Gráficas favorables -->
                        <div class="ctrlBasico" style="margin-left: 20px;">
                            <telerik:RadGrid ID="grdPersonalidadLaboralII" ShowHeader="true" runat="server" GridLines="None" AutoGenerateColumns="false">
                                <MasterTableView>
                                    <Columns>
                                        <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="Da/Apoya (DA/AP)" DataField="DA_AP" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="Toma/Controla (TM/CT)" DataField="TM_CT" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="Mantiene/Conserva (MT/CS)" DataField="MT_CS" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="Adapta/Negocia (AD/NG)" DataField="AD_NG" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="Total" DataField="TOTAL" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>

                        <label style="visibility: hidden;" id="lblDAAP" runat="server">DA/AP</label>
                        <label style="visibility: hidden;" id="lblTMCT" runat="server">TM/CT</label>
                        <label style="visibility: hidden;" id="lblMTCS" runat="server">MT/CS</label>
                        <label style="visibility: hidden;" id="lblADNG" runat="server">AD/NG</label>
                        <label style="visibility: hidden;" id="Label1" runat="server">Total</label>
                        <label style="visibility: hidden;" runat="server" id="lblF1"></label>
                        <label style="visibility: hidden;" runat="server" id="lblF2"></label>
                        <label style="visibility: hidden;" runat="server" id="lblF3"></label>
                        <label style="visibility: hidden;" runat="server" id="lblF4"></label>
                        <label style="visibility: hidden;" runat="server" id="lblTotalF"></label>
                        <label style="visibility: hidden;" runat="server" id="lblD1"></label>
                        <label style="visibility: hidden;" runat="server" id="lblD2"></label>
                        <label style="visibility: hidden;" runat="server" id="lblD3"></label>
                        <label style="visibility: hidden;" runat="server" id="lblD4"></label>
                        <label style="visibility: hidden;" runat="server" id="lblTotalD"></label>
                        <div style="clear: both; height: 10px;"></div>
                        <div class="ctrlBasico" style="margin-left: 20px;">
                            <b>Cuando alguno de los valores es mayor o igual a 33,
                                se considera una característica en exceso</b>
                        </div>
                        <div style="clear: both; height: 10px;"></div>

                        <div class="ctrlBasico" style="margin-left: 20px;">
                            <div style="background: #CCC; width: 100%">
                                <b>Gráfica de condiciones favorables</b>
                            </div>
                            <div style="clear: both; height: 10px;"></div>

                            <table style="background-image: url('../Assets/images/PruebaLaboralII/LifoGraficaBase.gif');">
                                <tbody>
                                    <tr>
                                        <td>
                                            <div class="Cuadrante Abajo Derecha">
                                                <img id="imgF1" src="../Assets/images/PruebaLaboralII/LifoAzul3.gif" style="margin: -3px 0px 0px 0px;">
                                            </div>
                                            <div style="position: relative; float: right; bottom: 5px;">
                                                <span id="spF1" style="padding-right:5px;">A</span>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="Cuadrante Abajo Izquierda">
                                                <img id="imgF2" src="../Assets/images/PruebaLaboralII/LifoRojo2.gif" style="margin: -3px 0px 0px -205px;">
                                            </div>
                                            <div style="position: relative; bottom: 5px;">
                                                <span id="spF2" style="padding-left:5px;">A</span>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="position: relative; top: -5px; float: right;">
                                                <span id="spF3" style="padding-right:5px;">A</span>
                                            </div>
                                            <div class="Cuadrante Arriba Derecha">
                                                <img id="imgF3" src="../Assets/images/PruebaLaboralII/LifoVerde2.gif" style="margin: -235px 0px 5px 0px;">
                                            </div>

                                        </td>
                                        <td>
                                            <div style="position: relative; top: 5px;">
                                                <span id="spF4" style="padding-left:5px;">A</span>
                                            </div>
                                            <div class="Cuadrante Arriba Izquierda">
                                                <img id="imgF4" src="../Assets/images/PruebaLaboralII/LifoAmarillo2.gif" style="margin: -224px 0px 0px -206px;">
                                            </div>

                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <%--<img src="../Assets/images/PruebaLaboralII/LifoGraficaBase.gif" />--%>


                        <div class="ctrlBasico" style="margin-left: 20px;">
                            <div style="background: #CCC; width: 100%;">
                                <b>Gráfica de condiciones desfavorables</b>
                            </div>
                            <div style="clear: both; height: 10px;"></div>

                            <table style="background-image: url('../Assets/images/PruebaLaboralII/LifoGraficaBase.gif');">
                                <tbody>
                                    <tr>
                                        <td>
                                            <div class="Cuadrante Abajo Derecha">
                                                <img id="imgD1" src="../Assets/images/PruebaLaboralII/LifoAzul3.gif" style="margin: -3px 0px 0px 0px;">
                                            </div>
                                            <div style="position: relative; float: right; bottom: 5px;">
                                                <span id="spD1" style="padding-right:5px;">A</span>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="Cuadrante Abajo Izquierda">
                                                <img id="imgD2" src="../Assets/images/PruebaLaboralII/LifoRojo2.gif" style="margin: -3px 0px 0px -205px;">
                                            </div>
                                            <div style="position: relative; bottom: 5px;">
                                                <span id="spD2" style="padding-left:5px;">A</span>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="position: relative; top: -5px; float: right;">
                                                <span id="spD3" style="padding-right:5px;">A</span>
                                            </div>
                                            <div class="Cuadrante Arriba Derecha">
                                                <img id="imgD3" src="../Assets/images/PruebaLaboralII/LifoVerde2.gif" style="margin: -235px 0px 5px 0px;">
                                            </div>
                                        </td>
                                        <td>
                                            <div style="position: relative; top: 5px;">
                                                <span id="spD4" style="padding-left:5px;">A</span>
                                            </div>
                                            <div class="Cuadrante Arriba Izquierda">
                                                <img id="imgD4" src="../Assets/images/PruebaLaboralII/LifoAmarillo2.gif" style="margin: -224px 0px 0px -206px;">
                                            </div>

                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div style="clear: both; height: 10px;"></div>
                        <div style="background: #CCC; width: 98%; margin-left: 20px;">
                            Resultados de condiciones favorables  
                        </div>
                        <div style="clear: both; height: 10px;"></div>
                        <%--inicio condiciones favorables mantiene y conserva--%>

                        <div runat="server" style="margin-left: 20px; display: none;" id="divMCF">
                            <table style="width: 98%; margin-left:20px; text-align: center;" border="1">
                                <thead>
                                    <tr>
                                        <td style="text-align: center;" colspan="5">
                                            <label>MANEJO DE LAS FUERZAS </label>
                                            <br />
                                            <label>DESARROLLO DE LAS FUERZAS</label><br />
                                            <label>RESUMEN</label>
                                            <br />
                                            <label><span style="color: #0A884B">ESTILO LIFO MANTIENE Y CONSERVA</span></label>
                                        </td>
                                    </tr>
                                </thead>
                            </table>
                                     <div style="height:5px; clear:both;"></div>
                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <label>USO PRODUCTIVO DE LAS FUERZAS</label>
                                <br />
                                <label><span style="color: #0A884B;">Estilo productivo</span> </label>
                                <br />
                                <h4>Tiene gran confianza en la lógica, los hechos y el sistema.</h4>
                                <br />
                                <h4>A menudo sopesa todas las alternativas para eliminar los riesgos.</h4>
                                <br />
                                <h4>Tiene necesidad de prevenir, no quiere sorpresas.</h4>
                                <br />
                                <h4>Usa al máximo procedimiento y regulaciones.</h4>
                                <br />
                                <%--<h4>Uno de probar lo práctico del cambio para convencer de la necesidad del cambio.</h4>--%>                          <%--SE CAMBIÓ EL 01 DE SEP 2017, SE COMPARO CON XML DE 4.9--%>
                                <H4>Uno debe probar la practicabilidad del cambio para convencerlo de la necesidad del cambio.</H4>
                                <br />
                                <h4>La filosofía subyacente es: "Hay que mantener lo que se tiene y construir el futuro sobre la base del pasado".</h4>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            <label>IRONIA DE LA FUERZA/FLAQUEZA</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label><span style="color: #0A884B;">Productivo </span></label>
                                        </td>
                                        <td>
                                            <label><span style="color: #0A884B;">Exceso</span> </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Tenaz</h4>
                                        </td>
                                        <td>
                                            <h4>Demasiado persistente</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Práctico</h4>
                                        </td>
                                        <td>
                                            <h4>No creativo</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Económico</h4>
                                        </td>
                                        <td>
                                            <h4>Avaro</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Reservado</h4>
                                        </td>
                                        <td>
                                            <h4>Inamistoso</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Factual</h4>
                                        </td>
                                        <td>
                                            <h4>Limitado por datos</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Constante</h4>
                                        </td>
                                        <td>
                                            <h4>Obstinado</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Cuidadoso</h4>
                                        </td>
                                        <td>
                                            <h4>Elaborado</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Metódico</h4>
                                        </td>
                                        <td>
                                            <h4>Lento</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Detallado</h4>
                                        </td>
                                        <td>
                                            <h4>Demasiado minucioso</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Analítico</h4>
                                        </td>
                                        <td>
                                            <h4>Crítico</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Controlado</h4>
                                        </td>
                                        <td>
                                            <h4>Sin sentimientos</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Cauteloso </h4>
                                        </td>
                                        <td>
                                            <h4>No arriesga</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Realista</h4>
                                        </td>
                                        <td>
                                            <h4>Sin imaginación</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Lógico</h4>
                                        </td>
                                        <td>
                                            <h4>Rígido</h4>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <%--<div style="clear: both; height: 10px;"></div>--%>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO PRODUCTIVO DE LAS FUERZAS</label>--%>
                                <label><span style="color: #0A884B;">Cómo influir en la persona</span> </label>
                                <br />
                                <h4>Presente ideas no riesgos.</h4>
                                <br />
                                <h4>Dé oportunidad para analizar.</h4>
                                <br />
                                <h4>Use lógica, use hechos.</h4>
                                <%--<br />
                                <h4>Usa al máximo procedimiento y regulaciones.</h4>--%>          <%--SE CAMBIÓ EL 01 DE SEP 2017, SE COMPARO CON XML DE 4.9--%>
                                <br />
                                <h4>Use familiaridad, rutina y estructura.</h4>
                                <br />
                                <h4>Relacione cosas nuevas a cosas viejas .</h4>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO PRODUCTIVO DE LAS FUERZAS</label>--%>
                                <br />
                                <label><span style="color: #0A884B;">Cuál es el ambiente Más efectivo</span> </label>
                                <br />
                                <h4>Neutralidad emocional.</h4>
                                <br />
                                <h4>Basado en hechos.</h4>
                                <br />
                                <h4>Científico.</h4>
                                <br />
                                <h4>Práctico.</h4>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO PRODUCTIVO DE LAS FUERZAS</label>--%>
                                <br />
                                <label><span style="color: #0A884B;">Cuál es el ambiente Menos efectivo</span> </label>
                                <br />
                                <h4>Reglas y procedimientos en constante cambio.</h4>
                                <br />
                                <h4>Altamente emocional.</h4>
                                <br />
                                <h4>Decisiones prematuras.</h4>
                                <br />
                                <h4>No se le toma en serio.</h4>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO PRODUCTIVO DE LAS FUERZAS</label>--%>
                                <br />
                                <label><span style="color: #0A884B;">Cómo ser el Patrón más efectivo</span> </label>
                                <br />
                                <h4>Sea organizado.</h4>
                                <br />
                                <h4>Muestre que tiene un propósito.</h4>
                                <br />
                                <h4>Preste atención a detalles.</h4>
                                <br />
                                <h4>Sea sistemático.</h4>
                                <br />
                                <h4>Sea objetivo.</h4>
                                <br />
                                <h4>Sea justo.</h4>
                                <br />
                                <h4>Sea consiente.</h4>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO PRODUCTIVO DE LAS FUERZAS</label>--%>
                                <br />
                                <label><span style="color: #0A884B;">Cómo ser el Empleado más efectivo</span> </label>
                                <br />
                                <h4>Sea respetuoso.</h4>
                                <br />
                                <h4>Adaptado.</h4>
                                <br />
                                <h4>Lógico.</h4>
                                <br />
                                <h4>Preste atención.</h4>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO PRODUCTIVO DE LAS FUERZAS</label>--%>
                                <br />
                                <label><span style="color: #0A884B;">Las Necesidades del estilo</span> </label>
                                <br />
                                <h4>Ser visto como alguien objetivo, determinado y racional.</h4>
                                <br />
                                <h4>Sentirse a salvo, seguro.</h4>
                                <br />
                                <h4>Sentir que ninguna pérdida es irreparable.</h4>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO PRODUCTIVO DE LAS FUERZAS</label>--%>
                                <br />
                                <label><span style="color: #0A884B;">Cómo apoyarlo a superar sus excesos</span> </label>
                                <br />
                                <h4>Trate de disminuir la tensión y la amenaza.</h4>
                                <br />
                                <h4>Lleve las emociones al mínimo.</h4>
                                <br />
                                <h4>Trate un tono más ligero preferentemente con humor.</h4>
                                <br />
                                <h4>Pida sugerencias sobre los criterios que podrían utilizarse para evaluar el problema.</h4>
                                <br />
                                <h4>Permita un cierto tiempo extra antes de tomar la decisión.</h4>
                                <br />
                                <h4>Obtenga datos adicionales en los que la persona pueda confiar.</h4>
                            </div>

                        </div>

                        <%--fin mantiene y conserva favorable--%>

                        <%--inicio favorable da y apoya--%>

                        <div runat="server" style="margin-left: 20px; display: none;" id="divDAF">
                            <table style="margin-left: 20px; width: 98%; text-align: center;" border="1">
                                <thead>
                                    <tr>
                                        <td style="text-align: center;" colspan="5">
                                            <label>MANEJO DE LAS FUERZAS </label>
                                            <br />
                                            <label>DESARROLLO DE LAS FUERZAS</label><br />
                                            <label>RESUMEN</label>
                                            <br />
                                            <label><span style="color: #0026ff">ESTILO LIFO DA Y APOYA</span></label>
                                        </td>
                                    </tr>
                                </thead>
                            </table>
                                     <div style="height:5px; clear:both;"></div>
                            <%--PÁRRAFOS--%>
                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <label>USO PRODUCTIVO DE LAS FUERZAS</label>
                                <br />
                                <label><span style="color: #0026ff;">Estilo productivo</span> </label>
                                <br />
                                <h4>Expectativas muy altas para sí mismo y para los demás.</h4>
                                <br />
                                <h4>Admira y apoya las realizaciones de los otros.</h4>
                                <br />
                                <h4>Gran fe y confianza en los demás.</h4>
                                <br />
                                <h4>Ansioso de responder cuando se pide ayuda.</h4>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <table border="0">
                                    <tr>
                                        <td colspan="2">
                                            <label>IRONIA DE LA FUERZA/FLAQUEZA</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label><span style="color: #0026ff;">Productivo </span></label>
                                        </td>
                                        <td>
                                            <label><span style="color: #0026ff;">Exceso</span> </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Acepta</h4>
                                        </td>
                                        <td>
                                            <h4>Indulgente</h4>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <h4>Cooperativo</h4>
                                        </td>
                                        <td>
                                            <h4>Fácilmente influenciable</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Considerado</h4>
                                        </td>
                                        <td>
                                            <h4>Se niega a si mismo</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Idealista</h4>
                                        </td>
                                        <td>
                                            <h4>Impráctico</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Modesto</h4>
                                        </td>
                                        <td>
                                            <h4>Auto-despectivo</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Cortés</h4>
                                        </td>
                                        <td>
                                            <h4>Deferente</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Optimista</h4>
                                        </td>
                                        <td>
                                            <h4>No realista</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Confiado</h4>
                                        </td>
                                        <td>
                                            <h4>Ingenuo</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Afectuoso</h4>
                                        </td>
                                        <td>
                                            <h4>Sentimental</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Leal</h4>
                                        </td>
                                        <td>
                                            <h4>Obligado</h4>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <h4>Ayuda</h4>
                                        </td>
                                        <td>
                                            <h4>Paternal</h4>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <h4>Receptivo</h4>
                                        </td>
                                        <td>
                                            <h4>Pasivo</h4>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <h4>Sensible</h4>
                                        </td>
                                        <td>
                                            <h4>Demasiado comprometido</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Procura excelencia</h4>
                                        </td>
                                        <td>
                                            <h4>Perfeccionista</h4>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO PRODUCTIVO DE LAS FUERZAS</label>--%>

                                <label><span style="color: #0026ff;">Cómo influir en la persona</span> </label>
                                <br />
                                <h4>Enfatice causas valederas.</h4>
                                <br />
                                <h4>Apele al idealismo.</h4>
                                <br />
                                <h4>Pida ayuda.</h4>
                                <br />
                                <h4>Apele a la excelencia.</h4>
                                <br />
                                <h4>Muestre interés y preocupación.</h4>
                                <br />
                                <h4>Enfatice el desarrollo personal.</h4>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO PRODUCTIVO DE LAS FUERZAS</label>--%>

                                <label><span style="color: #0026ff;">Cuál es el ambiente Más efectivo</span> </label>
                                <br />
                                <h4>Respeto.</h4>
                                <br />
                                <h4>Apoyo.</h4>
                                <br />
                                <h4>Reafirmación.</h4>
                                <br />
                                <h4>Idealismo.</h4>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO PRODUCTIVO DE LAS FUERZAS</label>--%>

                                <label><span style="color: #0026ff;">Cuál es el ambiente Menos efectivo</span> </label>
                                <br />
                                <h4>Traición.</h4>
                                <br />
                                <h4>Critica personal.</h4>
                                <br />
                                <h4>Ridículo.</h4>
                                <br />
                                <h4>Fracaso.</h4>
                                <br />
                                <h4>Falta de apoyo.</h4>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO PRODUCTIVO DE LAS FUERZAS</label>--%>

                                <label><span style="color: #0026ff;">Cómo ser el Patrón más efectivo</span> </label>
                                <br />
                                <h4>Dé reconocimiento, confianza y gratitud.</h4>
                                <br />
                                <h4>Defina las metas conjuntamente.</h4>
                                <br />
                                <h4>Sea accesible.</h4>
                                <br />
                                <h4>Trate de compartir.</h4>
                                <br />
                                <h4>Sea confiable.</h4>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO PRODUCTIVO DE LAS FUERZAS</label>--%>

                                <label><span style="color: #0026ff;">Cómo ser el Empleado más efectivo</span> </label>
                                <br />
                                <h4>Demuestre que vale.</h4>
                                <br />
                                <h4>Muestre lealtad.</h4>
                                <br />
                                <h4>Sea sincero.</h4>
                                <br />
                                <h4>Orientado hacia el equipo de trabajo.</h4>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO PRODUCTIVO DE LAS FUERZAS</label>--%>

                                <label><span style="color: #0026ff;">Las Necesidades del estilo</span> </label>
                                <br />
                                <h4>Ser visto como alguien adecuado y valioso.</h4>
                                <br />
                                <h4>Sentirse apreciado, comprendido, aceptado-tenerle confianza.</h4>
                                <br />
                                <h4>Sentir que los ideales no están perdidos.</h4>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO PRODUCTIVO DE LAS FUERZAS</label>--%>

                                <label><span style="color: #0026ff;">Cómo apoyarlo a superar sus excesos</span> </label>
                                <br />
                                <h4>Brinde apoyo, reafirmación y aliento.</h4>
                                <br />
                                <h4>Dé auxilio y ayuda específicos.</h4>
                                <br />
                                <h4>Escuche atentamente a la persona.</h4>
                                <br />
                                <h4>Provea justificaciones significativas dirigidas a la ansiedad, la queja o preocupación.</h4>
                                <br />
                                <h4>Reconozca el valor del intento aún cuando las consecuencias no hayan sido las deseadas.</h4>
                                <br />
                                <h4>Sugiera modos como la persona puede compensar por el error, o recuperarse.</h4>
                                <br />
                                <h4>No insista o atice por respuestas retrasadas.</h4>
                            </div>
                        </div>

                        <%--fin favorable de da y apoya--%>

                        <%---inicio toma y controla  --%>

                         <div runat="server" style="margin-left: 20px; margin-right: 20px; display: none;" id="divTCF">
                            <table style="margin-left: 20px; margin-right: 20px; width: 98%; text-align: center;" border="1">
                                <thead>
                                    <tr>
                                        <td style="text-align: center;" colspan="5">
                                            <label>MANEJO DE LAS FUERZAS </label>
                                            <br />
                                            <label>DESARROLLO DE LAS FUERZAS</label><br />
                                            <label>RESUMEN</label>
                                            <br />
                                            <label><span style="color: #FC2D05">ESTILO LIFO TOMA Y CONTROLA</span></label>
                                        </td>
                                    </tr>
                                </thead>
                            </table>
                             <div style="height:5px; clear:both;"></div>
                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <label>USO PRODUCTIVO DE LAS FUERZAS</label>
                                <br />
                                <label><span style="color: #FC2D05;">Estilo productivo</span> </label>
                                <br />
                                <h4>Le gusta estar a cargo, asumir el mando y el control.</h4>
                                <br />
                                <h4>Rápido para actuar o correr riesgos.</h4>
                                <br />
                                <h4>Le gusta el desafío, la oportunidad para superar una dificultad.</h4>
                                <br />
                                <h4>Busca la novedad y la variedad.</h4>
                                <br />
                                <h4>Prefiere dirigir y coordinar el trabajo de los demás.</h4>
                                <br />
                                <h4>Se posesiona de una oportunidad cuando la ve.</h4>
                                <br />
                                <h4>Dice: "Si usted quiere que algo ocurra, usted debe hacer que ello ocurra".</h4>
                            </div>

                            <%--<div class="ctrlBasico" style="width: 250px; height: 600px; text-align: center;">
                            <label>USO EXCESIVO DE LAS FUERZAS BAJO TENSIÓN</label>
                            <label><span style="color: #FC2D05;">Estilo de tensión</span> </label>
                            <br />
                            <h4>Se vuelve manipulador.</h4>
                            <br />
                            <h4>Se vuelve impulsivo.</h4>
                            <h4>Le gustan las cosas nuevas simplemente por la novedad, abandona lo viejo aunque aún sea útil.</h4>
                            <h4>Quita a los otros su autonomía y sus oportunidades.</h4>
                        </div>--%>
                            <%--<div class="ctrlBasico" style="width: 250px; height: 600px; text-align: center;">
                            <label>USO EXCESIVO DE LAS FUERZAS EN CONFLICTO O DESACUERDO</label><br />
                            <label><span style="color: #FC2D05;">Estilo de Lucha</span> </label>
                            <br />
                            <h4>Tiende a reclamar abiertamente que las cosas se hagan como él quiere</h4>
                            <br />
                            <h4>Defiende su posición con rapidez.</h4>
                            <br />
                            <h4>Pronto para la lucha y coerción.</h4>
                            <h4>Es capaz de pelear por sus derechos hasta la eternidad.</h4>
                        </div>--%>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            <label>IRONIA DE LA FUERZA/FLAQUEZA</label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label><span style="color: #FC2D05;">Productivo </span></label>
                                        </td>
                                        <td>
                                            <label><span style="color: #FC2D05;">Exceso</span> </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Controlador</h4>
                                        </td>
                                        <td>
                                            <h4>Dominante</h4>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <h4>Rápido para actuar</h4>
                                        </td>
                                        <td>
                                            <h4>Impulsivo</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Confianza en sí mismo</h4>
                                        </td>
                                        <td>
                                            <h4>Arrogante</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Busca el cambio</h4>
                                        </td>
                                        <td>
                                            <h4>Desperdicia lo viejo</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Persuasivo</h4>
                                        </td>
                                        <td>
                                            <h4>Distorsionador</h4>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <h4>Esforzado</h4>
                                        </td>
                                        <td>
                                            <h4>Coercitivo</h4>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <h4>Competitivo</h4>
                                        </td>
                                        <td>
                                            <h4>Luchador</h4>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <h4>Corre riesgos</h4>
                                        </td>
                                        <td>
                                            <h4>Apuesta</h4>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <h4>Persistente</h4>
                                        </td>
                                        <td>
                                            <h4>Presiona</h4>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <h4>Urgente</h4>
                                        </td>
                                        <td>
                                            <h4>Impaciente</h4>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <h4>Emprendedor</h4>
                                        </td>
                                        <td>
                                            <h4>Oportunista</h4>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <h4>Toma iniciativa</h4>
                                        </td>
                                        <td >
                                            <h4>Actúa sin autorización constantemente, probándose así mismo</h4>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <h4>Responde al desafío</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h4>Imaginativo</h4>
                                        </td>
                                        <td>
                                            <h4>No realista</h4>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO EXCESIVO DE LAS FUERZAS BAJO TENSIÓN</label>--%>
                                <label><span style="color: #FC2D05;">Cómo influir en la persona</span> </label>
                                <br />
                                <h4>Dé oportunidades.</h4>
                                <br />
                                <h4>Dé más responsabilidad.</h4>
                                <br />
                                <h4>Desafíe.</h4>
                                <br />
                                <h4>Dé recursos que faciliten el logro.</h4>
                                <br />
                                <h4>Dé autoridad.</h4>
                            </div>
                          <%--   <div style="height:5px; clear:both;"></div>--%>
                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO EXCESIVO DE LAS FUERZAS BAJO TENSIÓN</label>--%>
                                <label><span style="color: #FC2D05;">Cuál es el ambiente Más efectivo</span> </label>
                                <br />
                                <h4>Competencia.</h4>
                                <br />
                                <h4>Directo.</h4>
                                <br />
                                <h4>Con riesgos.</h4>
                                <br />
                                <h4>Oportunidades.</h4>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO EXCESIVO DE LAS FUERZAS BAJO TENSIÓN</label>--%>
                                <label><span style="color: #FC2D05;">Cuál es el ambiente Menos efectivo</span> </label>
                                <br />
                                <h4>Sin recursos.</h4>
                                <br />
                                <h4>Autoridad bloqueada.</h4>
                                <br />
                                <h4>Responsabilidad disminuida.</h4>
                                <br />
                                <h4>Sin desafíos.</h4>
                                <br />
                                <h4>Imposible controlar factores que afectan los resultados.</h4>
                            </div>
                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO EXCESIVO DE LAS FUERZAS BAJO TENSIÓN</label>--%>
                                <label><span style="color: #FC2D05;">Cómo ser el Patrón más efectivo</span> </label>
                                <br />
                                <h4>Téngase confianza.</h4>
                                <br />
                                <h4>Provea autonomía.</h4>
                                <br />
                                <h4>Recompense los resultados.</h4>
                                <br />
                                <h4>Fije límites firmes, pero reconozca iniciativa.</h4>
                                <br />
                                <h4>Escuche, pero sea decidido.</h4>
                                <br />
                                <h4>Luche en igualdad de condiciones.</h4>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO EXCESIVO DE LAS FUERZAS BAJO TENSIÓN</label>--%>
                                <label><span style="color: #FC2D05;">Cómo ser el Empleado más efectivo</span> </label>
                                <br />
                                <h4>Responda, no se muestre indiferente.</h4>
                                <br />
                                <h4>Sea capaz.</h4>
                                <br />
                                <h4>Sea independiente.</h4>
                                <br />
                                <h4>Sea directo.</h4>
                            </div>
                            <%-- <div style="height:5px; clear:both;"></div>--%>
                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO EXCESIVO DE LAS FUERZAS BAJO TENSIÓN</label>--%>
                                <label><span style="color: #FC2D05;">Las Necesidades del estilo</span> </label>
                                <br />
                                <h4>Ser visto como alguien poderoso, capaz y competente.</h4>
                                <br />
                                <h4>Sentirse capaz de superar obstáculos.</h4>
                                <br />
                                <h4>Sentir que hay aún otras oportunidades.</h4>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO EXCESIVO DE LAS FUERZAS BAJO TENSIÓN</label>--%>
                                <label><span style="color: #FC2D05;">Cómo apoyarlo a superar sus excesos</span> </label>
                                <br />
                                <h4>Trate de responder rápidamente.</h4>
                                <br />
                                <h4>Ofrezca soluciones, no traiga nuevos problemas.</h4>
                                <br />
                                <h4>Sea sincero y firme, pero respetuoso.</h4>
                                <br />
                                <h4>Refleje su comprensión de la preocupación.</h4>
                                <br />
                                <h4>Haga preguntas para ayudar a la persona a sentir que ella ha encontrado su propia solución.</h4>
                                <br />
                                <h4>Provea maneras alternativas de enfocar la situación.</h4>
                                <br />
                                <h4>Espere a que baje la presión antes de exigir.</h4>
                            </div>
                        </div>

                        <%-- fin toma y controla --%>

                        <%-- inicio adpta y negocia cuadros--%>

                        <div runat="server" style="display: none;" id="divANF">
                            <%--         <div style="clear: both; height: 10px;"></div>--%>
                            <table style="margin-left: 20px; width: 98%; text-align: center;" border="1">
                                <thead>
                                    <tr>
                                        <td style="text-align: center;" colspan="5">
                                            <label>MANEJO DE LAS FUERZAS </label>
                                            <br />
                                            <label>DESARROLLO DE LAS FUERZAS</label><br />
                                            <label>RESUMEN</label>
                                            <br />
                                            <label><span style="color: #cf790f">ESTILO LIFO ADAPTA Y NEGOCIA</span></label>
                                        </td>
                                    </tr>
                                </thead>
                            </table>
                                     <div style="height:5px; clear:both;"></div>
                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; border: double">
                                <div style="padding-left: 5px;">
                                    <label>USO PRODUCTIVO DE LAS FUERZAS</label>
                                    <br />
                                    <label><span style="color: #cf790f;">Estilo productivo</span> </label>
                                    <br />
                                    <h4>Usa sus habilidades sociales y su encanto personal para manejarse con las realidades del mundo.</h4>
                                    <br />
                                    <h4>Enfatiza la adaptación y el acuerdo con los demás.</h4>                                                 
                                    <%--<br />
                                    <h4>Le gusta el desafío, la oportunidad para superar una dificultad.</h4>--%>                                       <%--SE CAMBIÓ EL 01 DE SEP 2017, SE COMPARO CON XML DE 4.9--%>
                                    <br />  
                                    <h4>Tiene maneras joviales, juguetonas, no serias.</h4>
                                    <br />
                                    <h4>Es socialmente sensible a las necesidades de los demás.</h4>
                                    <br />
                                    <h4>Dice: "Si quieres ir hacia adelante, averigüe lo que los otros necesitan y asegúrese de que lo  obtengan".</h4>
                                    <br />
                                </div>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; border: double">
                                <div style="padding-left: 5px;">
                                    <table>
                                        <tr>
                                            <td colspan="2">
                                                <label>IRONIA DE LA FUERZA/FLAQUEZA</label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label><span style="color: #cf790f;">Productivo </span></label>
                                            </td>
                                            <td>
                                                <label><span style="color: #cf790f;">Exceso</span> </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <h4>Flexible</h4>
                                            </td>
                                            <td>
                                                <h4>Inconsistente</h4>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <h4>Experimentador</h4>
                                            </td>
                                            <td>
                                                <h4>Sin metas</h4>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <h4>Jovial</h4>
                                            </td>
                                            <td>
                                                <h4>Pueril</h4>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <h4>Entusiasta</h4>
                                            </td>
                                            <td>
                                                <h4>Agitado</h4>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <h4>Diplomático</h4>
                                            </td>
                                            <td>
                                                <h4>Acepta demasiado</h4>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <h4>Adaptable</h4>
                                            </td>
                                            <td>
                                                <h4>Sin convicción</h4>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <h4>Hábil socialmente</h4>
                                            </td>
                                            <td>
                                                <h4>Manipulador</h4>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <h4>Negociador</h4>
                                            </td>
                                            <td>
                                                <h4>Renuncia demasiado</h4>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <h4>Animado</h4>
                                            </td>
                                            <td>
                                                <h4>Melodramático</h4>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <h4>Inspirador</h4>
                                            </td>
                                            <td>
                                                <h4>No realista</h4>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <h4>Sociable</h4>
                                            </td>
                                            <td>
                                                <h4>Incapaz de estar solo</h4>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <h4>Solícito</h4>
                                            </td>
                                            <td>
                                                <h4>Lisonjero</h4>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <h4>Divertido</h4>
                                            </td>
                                            <td>
                                                <h4>Tonto</h4>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <h4>Cumplido</h4>
                                            </td>
                                            <td>
                                                <h4>Adulador</h4>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; border: double">
                                <div style="padding-left: 5px;">
                                    <%--<label>USO EXCESIVO DE LAS FUERZAS EN CONFLICTO O DESACUERDO</label><br />--%>
                                    <label><span style="color: #cf790f;">Cómo influir en la persona</span> </label>
                                    <br />
                                    <h4>Dé oportunidad para hacer cosas con otros.</h4>
                                    <br />
                                    <h4>Use humor.</h4>
                                    <br />
                                    <h4>Hágale saber que usted está complacido.</h4>
                                    <br />
                                    <h4>Dé oportunidades para lucimiento personal.</h4>
                                </div>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; border: double">
                                <div style="padding-left: 5px;">
                                    <%--<label>USO EXCESIVO DE LAS FUERZAS EN CONFLICTO O DESACUERDO</label><br />--%>
                                    <label><span style="color: #cf790f;">Cuál es el ambiente Más efectivo</span> </label>
                                    <br />
                                    <h4>Social.</h4>
                                    <br />
                                    <h4>Cambiante.</h4>
                                    <br />
                                    <h4>Jovial.</h4>
                                    <br />
                                    <h4>Optimista.</h4>
                                </div>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; border: double">
                                <div style="padding-left: 5px;">
                                    <%--<label>USO EXCESIVO DE LAS FUERZAS EN CONFLICTO O DESACUERDO</label><br />--%>
                                    <label><span style="color: #cf790f;">Cuál es el ambiente Menos efectivo</span> </label>
                                    <br />
                                    <h4>Autoridad crítica.</h4>
                                    <br />
                                    <h4>Compañeros no amistosos.</h4>
                                    <br />
                                    <h4>Rutinas y detalles.</h4>
                                    <br />
                                    <h4>Horarios firmes y supervisión.</h4>
                                </div>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; border: double">
                                <div style="padding-left: 5px;">
                                    <%--<label>USO EXCESIVO DE LAS FUERZAS EN CONFLICTO O DESACUERDO</label><br />--%>
                                    <label><span style="color: #cf790f;">Cómo ser el Patrón más efectivo</span> </label>
                                    <br />
                                    <h4>Sea amistoso.</h4>
                                    <br />
                                    <h4>Dé información.</h4>
                                    <br />
                                    <h4>Hágale saber los resultados.</h4>
                                    <br />
                                    <h4>Sea comprensivo.</h4>
                                    <br />
                                    <h4>Sea alentador.</h4>
                                    <br />
                                    <h4>Sea flexible.</h4>
                                    <br />
                                    <h4>Use su sentido del humor.</h4>
                                </div>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; border: double">
                                <div style="padding-left: 5px;">
                                    <%--<label>USO EXCESIVO DE LAS FUERZAS EN CONFLICTO O DESACUERDO</label><br />--%>
                                    <label><span style="color: #cf790f;">Cómo ser el Empleado más efectivo</span> </label>
                                    <br />
                                    <h4>Sea sociable.</h4>
                                    <br />
                                    <h4>Sofisticado.</h4>
                                    <br />
                                    <h4>Tenga tacto.</h4>
                                    <br />
                                    <h4>Tenga influencia.</h4>
                                </div>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; border: double">
                                <div style="padding-left: 5px;">
                                    <%--<label>USO EXCESIVO DE LAS FUERZAS EN CONFLICTO O DESACUERDO</label><br />--%>
                                    <label><span style="color: #cf790f;">Las Necesidades del estilo</span> </label>
                                    <br />
                                    <h4>Ser visto como alguien que gusta a los demás, popular.</h4>
                                    <br />
                                    <h4>Hacer sentir feliz a todos con los resultados.</h4>
                                    <br />
                                    <h4>Sentir que hay aún oportunidades para complacer a la gente. </h4>
                                </div>
                            </div>
                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; border: double">
                                <div style="padding-left: 5px;">
                                    <%--<label>USO EXCESIVO DE LAS FUERZAS EN CONFLICTO O DESACUERDO</label><br />--%>
                                    <label><span style="color: #cf790f;">Cómo apoyarlo a superar sus excesos</span> </label>
                                    <br />
                                    <h4>Dele la seguridad de que usted lo aprecia.</h4>
                                    <br />
                                    <h4>Esté dispuesto a tratar de negociar.</h4>
                                    <br />
                                    <h4>Sugiera que usted admira a las personas que son sinceras cuando no están de acuerdo.</h4>
                                    <br />
                                    <h4>Use un enfoque positivo: "Lo que me gusta de esto es"..."Las reservas que tengo son...".</h4>
                                    <br />
                                    <h4>Pase algo de tiempo en actitud amistosamente social, antes de exigir la decisión.</h4>
                                    <br />
                                    <h4>Permítale a la persona "conservar una buena fachada".</h4>
                                </div>
                            </div>
                            |
                        </div>

                        <%-- fin adpata y negocia--%>

                        <%-- condiciones desfavorables--%>

                        <%-- inicio --%>

                        <div style="clear: both; height: 10px;"></div>
                        <div style="margin-left: 20px; background: #CCC; width: 98%;">
                            Resultados de condiciones desfavorables  
                        </div>
                        <div style="clear: both; height: 10px;"></div>

                        <div runat="server" style="margin-left: 20px; display: none;" id="divDAD">
                            <%-- <div style="clear: both; height: 10px;"></div>--%>
                            <table style="width: 98%; margin-left:20px; text-align: center;" border="1">
                                <thead>
                                    <tr>
                                        <td style="text-align: center;" colspan="5">
                                            <%-- <label>MANEJO DE LAS FUERZAS </label>
                                        <br />
                                        <label>DESARROLLO DE LAS FUERZAS</label><br />
                                        <label>RESUMEN</label>
                                        <br />--%>
                                            <label><span style="color: #0026ff">ESTILO DA Y APOYA</span></label>
                                        </td>
                                    </tr>
                                </thead>
                            </table>
                            <div style="height:5px; clear:both;"></div>
                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO EXCESIVO DE LAS FUERZAS BAJO TENSIÓN</label>--%>
                                <label><span style="color: #0026ff;">Estilo de tensión</span> </label>
                                <br />
                                <h4>Se vuelve demasiado confiable e ingenuo.</h4>
                                <br />
                                <h4>Enfatiza tanto el estilo que se vuelve deferente.</h4>
                                <br />
                                <h4>Vulnerable a la desilusión cuando las metas son altas.</h4>
                                <br />
                                <h4>Fácilmente desilusionado y decepcionado por la gente.</h4>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO EXCESIVO DE LAS FUERZAS EN CONFLICTO O DESACUERDO</label><br />--%>
                                <label><span style="color: #0026ff;">Estilo de Lucha</span> </label>
                                <br />
                                <h4>Asume la culpa</h4>
                                <br />
                                <h4>Se vuelve inseguro y pide ayuda, se vuelve dependiente.</h4>
                                <br />
                                <h4>Percibido por los demás como demasiado "blando".</h4>
                                <br />
                                <h4>Se rinde en vez de pelear y "causar problemas"</h4>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO EXCESIVO DE LAS FUERZAS EN CONFLICTO O DESACUERDO</label><br />--%>
                                <label><span style="color: #0026ff;">Cómo apoyarlo a superar sus excesos</span> </label>
                                <br />
                                <h4>Brinde apoyo, reafirmación y aliento.</h4>
                                <br />
                                <h4>Dé auxilio y ayuda específicos.</h4>
                                <br />
                                <h4>Escuche atentamente a la persona.</h4>
                                <br />
                                <h4>Provea justificaciones significativas dirigidas a la ansiedad, la queja o preocupación.</h4>
                                <br />
                                <h4>Reconozca el valor del intento aún cuando las consecuencias no hayan sido las deseadas.</h4>
                                <br />
                                <h4>Sugiera modos como la persona puede compensar por el error, o recuperarse.</h4>
                                <br />
                                <h4>No insista o atice por respuestas retrasadas. </h4>
                            </div>
                        </div>

                        <%-- FIN DE DA Y APOYA DESFAVORABLE--%>

                        <%-- INICIO TOMA Y CONTROLA DESFAVORABLE--%>

<%--                        <div style="clear: both; height: 10px;"></div>--%>

                        <div runat="server" style="margin-left: 20px; display: none;" id="divTCD">
                            <table style="width: 98%; margin-left: 20px; text-align: center;" border="1">
                                <thead>
                                    <tr>
                                        <td style="text-align: center;" colspan="5">
                                            <%-- <label>MANEJO DE LAS FUERZAS </label>
                                        <br />
                                        <label>DESARROLLO DE LAS FUERZAS</label><br />
                                        <label>RESUMEN</label>
                                        <br />--%>
                                            <label><span style="color: #FC2D05">ESTILO TOMA Y CONTROLA</span></label>
                                        </td>
                                    </tr>
                                </thead>
                            </table>
                            <div style="height:5px; clear:both;"></div>
                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO EXCESIVO DE LAS FUERZAS BAJO TENSIÓN</label>--%>
                                <label><span style="color: #FC2D05;">Estilo de tensión</span> </label>
                                <br />
                                <h4>Se vuelve manipulador.</h4>
                                <br />
                                <h4>Se vuelve impulsivo.</h4>
                                <br />
                                <h4>Le gustan las cosas nuevas simplemente por la novedad, abandona lo viejo aunque aún sea útil.</h4>
                                <br />
                                <h4>Quita a los otros su autonomía y sus oportunidades.</h4>
                            </div>
                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO EXCESIVO DE LAS FUERZAS EN CONFLICTO O DESACUERDO</label><br />--%>
                                <label><span style="color: #FC2D05;">Estilo de Lucha</span> </label>
                                <br />
                                <h4>Tiende a reclamar abiertamente que las cosas se hagan como él quiere</h4>
                                <br />
                                <h4>Defiende su posición con rapidez.</h4>
                                <br />
                                <h4>Pronto para la lucha y coerción.</h4>
                                <br />
                                <h4>Es capaz de pelear por sus derechos hasta la eternidad.</h4>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO EXCESIVO DE LAS FUERZAS EN CONFLICTO O DESACUERDO</label><br />--%>
                                <label><span style="color: #FC2D05;">Cómo apoyarlo a superar sus excesos</span> </label>
                                <br />
                                <h4>Trate de responder rápidamente.</h4>
                                <br />
                                <h4>Ofrezca soluciones, no traiga nuevos problemas.</h4>
                                <br />
                                <h4>Sea sincero y firme, pero respetuoso.</h4>
                                <br />
                                <h4>Refleje su comprensión de la preocupación.</h4>
                                <br />
                                <h4>Haga preguntas para ayudar a la persona a sentir que ella ha encontrado su propia solución.</h4>
                                <br />
                                <h4>Provea maneras alternativas de enfocar la situación.</h4>
                                <br />
                                <h4>Espere a que baje la presión antes de exigir. </h4>
                            </div>
                        </div>

                        <%-- FIN TOMA Y CONTROLA DESVARABLE--%>

                        <%-- INICIO MANTIENE Y CONSERVA DESFAVORABLE --%>

                        <div runat="server" style="margin-left: 20px; display: none;" id="divMCD">
                         <%--   <div style="clear: both; height: 10px;"></div>--%>
                            <table style="margin-left: 20px; width: 98%; text-align: center;" border="1">
                                <thead>
                                    <tr>
                                        <td style="text-align: center;" colspan="5">
                                            <%-- <label>MANEJO DE LAS FUERZAS </label>
                                        <br />
                                        <label>DESARROLLO DE LAS FUERZAS</label><br />
                                        <label>RESUMEN</label>
                                        <br />--%>
                                            <label><span style="color: #0A884B">ESTILO MANTIENE Y CONSERVA</span></label>
                                        </td>
                                    </tr>
                                </thead>
                            </table>
                            <div style="height:5px; clear:both;"></div>
                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO EXCESIVO DE LAS FUERZAS BAJO TENSIÓN</label>--%>
                                <label><span style="color: #0A884B;">Estilo de tensión</span> </label>
                                <br />
                                <h4>Llega a tener  "parálisis de análisis".</h4>
                                <br />
                                <h4>Se adhiere a viejos métodos y cosas ante las necesidades del cambio.</h4>
                            </div>
                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO EXCESIVO DE LAS FUERZAS EN CONFLICTO O DESACUERDO</label><br />--%>
                                <label><span style="color: #0A884B;">Estilo de Lucha</span> </label>
                                <br />
                                <h4>Acumula gran cantidad de hechos para apoyar sus ideas y espera que los otros reconozcan sus puntos de vista.</h4>
                                <br />
                                <h4>Se vuelve obstinado, frío o reservado.</h4>
                                <br />
                                <h4>Se "sale" de la situación y espera que los demás vayan a él.</h4>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; text-align: center; border: double">
                                <%--<label>USO EXCESIVO DE LAS FUERZAS EN CONFLICTO O DESACUERDO</label><br />--%>
                                <label><span style="color: #0A884B;">Cómo apoyarlo a superar sus excesos</span> </label>
                                <br />
                                <h4>Trate de disminuir la tensión y la amenaza.</h4>
                                <br />
                                <h4>Lleve las emociones al mínimo.</h4>
                                <br />
                                <h4>Trate un tono más ligero preferentemente con humor.</h4>
                                <br />
                                <h4>Pida sugerencias sobre los criterios que podrían utilizarse para evaluar el problema.</h4>
                                <br />
                                <h4>Permita un cierto tiempo extra antes de tomar la decisión.</h4>
                                <%--<br />
                                <h4>Provea maneras alternativas de enfocar la situación.</h4>--%>                                   <%--SE CAMBIÓ EL 01 DE SEP 2017, SE COMPARO CON XML DE 4.9--%>
                                <br />
                                <h4>Obtenga datos adicionales en los que la persona pueda confiar.</h4>
                            </div>
                        </div>

                        <%-- FIN MANTIENE Y CONSERVA DESFAVORABLE --%>

                        <%-- INICIO ADAPTA Y NEGOCIA DESFAVORABLE--%>

                        <div runat="server" style="margin-left: 20px; display: none;" id="divAND">
                       <%--     <div style="clear: both; height: 10px;"></div>--%>
                            <table style="margin-left: 20px; width: 98%; text-align: center;" border="1">
                                <thead>
                                    <tr>
                                        <td style="text-align: center;" colspan="5">
                                            <%-- <label>MANEJO DE LAS FUERZAS </label>
                                        <br />
                                        <label>DESARROLLO DE LAS FUERZAS</label><br />
                                        <label>RESUMEN</label>
                                        <br />--%>
                                            <label><span style="color: #cf790f">ESTILO LIFO ADAPTA Y NEGOCIA</span></label>
                                        </td>
                                    </tr>
                                </thead>
                            </table>
                            <div style="height:5px; clear:both;"></div>
                            <div class="ctrlBasico" style="margin-left: 20px; margin-left: 20px; width: 270px; height: 650px; border: double">
                                <%--<label>USO EXCESIVO DE LAS FUERZAS BAJO TENSIÓN</label>--%>
                                <label><span style="color: #cf790f;">Estilo de tensión</span> </label>
                                <br />
                                <h4>Demasiado solícito.</h4>
                                <br />
                                <h4>Se vuelve infantil y amigo de juguetear.</h4>
                                <br />
                                <h4>Tiende a ser visto como un tonto a veces.</h4>
                                <br />
                                <h4>Puede perder el sentido de su propia identidad.</h4>
                                <br />
                                <h4>Se vuelve ambivalente y demasiado flexible.</h4>
                            </div>
                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; border: double">
                                <%--<label>USO EXCESIVO DE LAS FUERZAS EN CONFLICTO O DESACUERDO</label><br />--%>
                                <label><span style="color: #cf790f;">Estilo de Lucha</span> </label>
                                <br />
                                <h4>Renuncia a demasiadas cosas y da la impresión de estar de acuerdo</h4>
                                <br />
                                <h4>Evita enfrentamientos aún cuando no crea que el otro tiene la razón.</h4>
                                <br />
                                <h4>Mantiene la armonía a cualquier precio.</h4>
                            </div>

                            <div class="ctrlBasico" style="margin-left: 20px; width: 270px; height: 650px; border: double">
                                <%--<label>USO EXCESIVO DE LAS FUERZAS EN CONFLICTO O DESACUERDO</label><br />--%>
                                <label><span style="color: #cf790f;">Cómo apoyarlo a superar sus excesos</span> </label>
                                <br />
                                <h4>Déle la seguridad de que usted lo aprecia.</h4>
                                <br />
                                <h4>Esté dispuesto a tratar de negociar.</h4>
                                <br />
                                <h4>Sugiera que usted admira a las personas que son sinceras cuando no están de acuerdo.</h4>
                                <br />
                                <h4>Use un enfoque positivo: "Lo que me gusta de esto es"..."Las reservas que tengo son...".</h4>
                                <br />
                                <h4>Pase algo de tiempo en actitud amistosamente social, antes de exigir la decisión.</h4>
                                <br />
                                <h4>Permítale a la persona "conservar una buena fachada".</h4>
                            </div>
                        </div>

                        <%-- FIN ADAPTA Y NEGOCIA DESFAVORABLE--%>
                        <div style="height:10px; clear:both;"></div>
                         <div class="divControlDerecha">
                                <telerik:RadButton ID="btnLaboral2" runat="server" AutoPostBack="false" Text="Imprimir" OnClientClicked="OpenLaboral2"></telerik:RadButton>
                            </div>
                    </telerik:RadPageView>

                     <telerik:RadPageView ID="RPVAdaptacion" runat="server">
                        <div class="ctrlBasico" style="padding-left: 20px;">
                            <table style="width: 400px; border-spacing: 5px;">
                                <tr style="border-style: solid; border-width: 1px; border-color: #808080;">
                                    <td colspan="6" style="text-align: center; background-color: #E6E6FA;">
                                        <label id="lblADesc" runat="server" style="text-align: center; width: 100%; height: 100%;"></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">&nbsp</td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                    <td colspan="2" style="border-style: solid; border-width: 1px; border-color: #808080; text-align: center; background-color: #E6E6FA;">Personalidad</td>
                                    <td colspan="2" style="border-style: solid; border-width: 1px; border-color: #808080; text-align: center; background-color: #E6E6FA;">Adaptación al medio</td>
                                </tr>
                                <tr style="border-bottom-style: dotted; border-bottom-width: 1px;">
                                    <td>
                                        <div style="background-color: #0A884B; width: 10px; height: 10px; border: 1px solid gray">&nbsp</div>
                                    </td>
                                    <td>2 Productividad</td>
                                    <td>
                                        <label style="text-align: center; width: 100%;" id="lblP2" runat="server"></label>

                                    </td>
                                    <td>
                                        <div style="background-color: #33AD33; width: 10px; height: 10px; border: 1px solid gray">&nbsp</div>
                                    </td>
                                    <td>
                                        <label style="text-align: center; width: 100%;" id="lblA2" runat="server"></label>
                                    </td>
                                    <td>
                                        <div style="background-color: #85FF85; width: 10px; height: 10px; border: 1px solid gray">&nbsp</div>
                                    </td>
                                </tr>
                                <tr style="border-bottom-style: dotted; border-bottom-width: 1px;">
                                    <td>
                                        <div style="background-color: #FC2D05; width: 10px; height: 10px; border: 1px solid gray">&nbsp</div>
                                    </td>
                                    <td>3 Empuje</td>
                                    <td>
                                        <label style="text-align: center; width: 100%;" id="lblP3" runat="server"></label>
                                    </td>
                                    <td>
                                        <div style="background-color: #D63333; width: 10px; height: 10px; border: 1px solid gray">&nbsp</div>
                                    </td>
                                    <td>
                                        <label style="text-align: center; width: 100%;" id="lblA3" runat="server"></label>
                                    </td>
                                    <td>
                                        <div style="background-color: #FF8585; width: 10px; height: 10px; border: 1px solid gray">&nbsp</div>
                                    </td>
                                </tr>
                                <tr style="border-bottom-style: dotted; border-bottom-width: 1px;">
                                    <td>
                                        <div style="background-color: #F6DB08; width: 10px; height: 10px; border: 1px solid gray">&nbsp</div>
                                    </td>
                                    <td>4 Sociabilidad</td>
                                    <td>
                                        <label style="text-align: center; width: 100%;" id="lblP4" runat="server"></label>
                                    </td>
                                    <td>
                                        <div style="background-color: #FFFF33; width: 10px; height: 10px; border: 1px solid gray">&nbsp</div>
                                    </td>
                                    <td>
                                        <label style="text-align: center; width: 100%;" id="lblA4" runat="server"></label>
                                    </td>
                                    <td>
                                        <div style="background-color: #FFFF85; width: 10px; height: 10px; border: 1px solid gray">&nbsp</div>
                                    </td>

                                </tr>
                                <tr style="border-bottom-style: dotted; border-bottom-width: 1px;">
                                    <td>
                                        <div style="background-color: #E1093C; width: 10px; height: 10px; border: 1px solid gray">&nbsp</div>
                                    </td>
                                    <td>5 Creatividad</td>
                                    <td>
                                        <label style="text-align: center; width: 100%;" id="lblP5" runat="server"></label>
                                    </td>
                                    <td>
                                        <div style="background-color: #AD33AD; width: 10px; height: 10px; border: 1px solid gray">&nbsp</div>
                                    </td>
                                    <td>
                                        <label style="text-align: center; width: 100%;" id="lblA5" runat="server"></label>
                                    </td>
                                    <td>
                                        <div style="background-color: #FFADFE; width: 10px; height: 10px; border: 1px solid gray">&nbsp</div>
                                    </td>
                                </tr>
                                <tr style="border-bottom-style: dotted; border-bottom-width: 1px;">
                                    <td>
                                        <div style="background-color: #523997; width: 10px; height: 10px; border: 1px solid gray">&nbsp</div>
                                    </td>
                                    <td>1 Paciencia</td>
                                    <td>
                                        <label style="text-align: center; width: 100%;" id="lblP1" runat="server"></label>
                                    </td>
                                    <td>
                                        <div style="background-color: #8533D6; width: 10px; height: 10px; border: 1px solid gray">&nbsp</div>
                                    </td>
                                    <td>
                                        <label style="text-align: center; width: 100%;" id="lblA1" runat="server"></label>
                                    </td>
                                    <td>
                                        <div style="background-color: #C185FF; width: 10px; height: 10px; border: 1px solid gray">&nbsp</div>
                                    </td>
                                </tr>
                                <tr style="border-bottom-style: dotted; border-bottom-width: 1px;">
                                    <td>
                                        <div style="background-color: #A45200; width: 10px; height: 10px; border: 1px solid gray">&nbsp</div>
                                    </td>
                                    <td>6 Vigor</td>
                                    <td>
                                        <label style="text-align: center; width: 100%;" id="lblP6" runat="server"></label>
                                    </td>
                                    <td>
                                        <div style="background-color: #AD7033; width: 10px; height: 10px; border: 1px solid gray">&nbsp</div>
                                    </td>
                                    <td>
                                        <label style="text-align: center; width: 100%;" id="lblA6" runat="server"></label>
                                    </td>
                                    <td>
                                        <div style="background-color: #FFC185; width: 10px; height: 10px; border: 1px solid gray">&nbsp</div>
                                    </td>
                                </tr>
                                <tr style="border-bottom-style: dotted; border-bottom-width: 1px;">
                                    <td>
                                        <div style="background-color: #D2CC78; width: 10px; height: 10px; border: 1px solid gray">&nbsp</div>
                                    </td>
                                    <td>0 Participación</td>
                                    <td>
                                        <label style="text-align: center; width: 100%;" id="lblP0" runat="server"></label>
                                    </td>
                                    <td>
                                        <div style="background-color: #FFFFAD; width: 10px; height: 10px; border: 1px solid gray">&nbsp</div>
                                    </td>

                                    <td>
                                        <label style="text-align: center; width: 100%;" id="lblA0" runat="server"></label>
                                    </td>
                                    <td>
                                        <div style="background-color: #FFFFD6; width: 10px; height: 10px; border: 1px solid gray">&nbsp</div>
                                    </td>

                                </tr>
                                <tr style="border-bottom-style: dotted; border-bottom-width: 1px;">
                                    <td>
                                        <div style="background-color: #000000; width: 10px; height: 10px; border: 1px solid gray">&nbsp</div>
                                    </td>
                                    <td>7 Satisfacción</td>
                                    <td>
                                        <label style="text-align: center; width: 100%;" id="lblP7" runat="server"></label>
                                    </td>
                                    <td>
                                        <div style="background-color: #000000; width: 10px; height: 10px; border: 1px solid gray">&nbsp</div>
                                    </td>

                                    <td>
                                        <label style="text-align: center; width: 100%;" id="lblA7" runat="server"></label>
                                    </td>
                                    <td>
                                        <div style="background-color: #808080; width: 10px; height: 10px; border: 1px solid gray">&nbsp</div>
                                    </td>

                                </tr>
                            </table>
                        </div>

                        <div class="ctrlBasico" style="padding-left: 10px;">

                            <div style="text-align: center; margin: 0 auto">

                                <telerik:RadHtmlChart runat="server" ID="rhcAdaptacion" Width="600px" Height="400px">
                                    <PlotArea>

                                        <XAxis Color="Black" MajorTickType="Outside" MinorTickType="Outside" Reversed="false" StartAngle="180">
                                            <LabelsAppearance DataFormatString="{0}" RotationAngle="75" Step="1" Skip="0"></LabelsAppearance>
                                            <MajorGridLines Color="#c8c8c8" Width="1"></MajorGridLines>
                                            <MinorGridLines Visible="false"></MinorGridLines>

                                            <Items>
                                                <telerik:AxisItem LabelText="2 Productividad"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="3 Empuje"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="4 Sociabilidad"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="5 Creatividad"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="1 Paciencia"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="6 Vigor"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="0 Participación"></telerik:AxisItem>
                                                <telerik:AxisItem LabelText="7 satisfacción"></telerik:AxisItem>
                                            </Items>
                                        </XAxis>
                                    </PlotArea>
                                    <Legend>
                                        <Appearance Visible="true" />
                                    </Legend>
                                </telerik:RadHtmlChart>

                            </div>
                            
                        </div>
                         <div style="height:15px; clear:both"></div>
                         <div class="divControlDerecha">
                                <telerik:RadButton ID="btnAdaptacion" runat="server" AutoPostBack="false" Text="Imprimir" OnClientClicked="OpenAdaptacion"></telerik:RadButton>
                            </div>
                    </telerik:RadPageView>

                    <telerik:RadPageView ID="RPVTIVA" runat="server">
                        <div class="ctrlBasico">

<%--                            <div style="position:absolute;" id="Div1"></div>
                                    <div>
                                        <img src="../Assets/images/EstiloPensamiento.jpg"
                                           style="position:absolute; z-index: -1; padding-left:20px" alt="Gráfica" />
                                        <br />
                                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                    </div>--%>

                            <div runat="server" style="display: none;" id="divInvalida">
                                <label><span style="color: #FF0000;">Prueba inválida- No hay datos a mostrar</span> </label>
                            </div>



                            <telerik:RadHtmlChart runat="server" ID="RadarLineTIVA" Width="700" Height="400" Transitions="true" Skin="Silk">
                                <PlotArea>
                                    <Series>
                                        <telerik:RadarColumnSeries Gap="1.5" Spacing="0">
                                            <Appearance>
                                                <FillStyle BackgroundColor="#2dc1ec"></FillStyle>
                                            </Appearance>
                                            <LabelsAppearance Visible="false" Position="OutsideEnd">
                                            </LabelsAppearance>
                                            <%--     <MarkersAppearance MarkersType="Circle" BackgroundColor="White" Size="8" BorderColor="#2dc1ec"
                                                BorderWidth="2"></MarkersAppearance>--%>
                                            <TooltipsAppearance DataFormatString="{0}%"></TooltipsAppearance>
                                        </telerik:RadarColumnSeries>
                                    </Series>
                                    <Appearance>
                                        <FillStyle BackgroundColor="Transparent"></FillStyle>
                                    </Appearance>
                                    <XAxis Color="Black" MajorTickType="Outside" MinorTickType="Outside"
                                        Reversed="false">
                                        
                                        <LabelsAppearance DataFormatString="{0}" RotationAngle="0" Step="1" Skip="0">
                                            <TextStyle Margin="10" />
                                        </LabelsAppearance>
                                        <MajorGridLines Color="#c8c8c8" Width="1"></MajorGridLines>
                                        <MinorGridLines Visible="false"></MinorGridLines>
                                        <Items>
                                            <telerik:AxisItem LabelText="Integridad Personal">
                                            </telerik:AxisItem>
                                            <telerik:AxisItem LabelText="Apego a leyes y reglamentos"></telerik:AxisItem>
                                            <telerik:AxisItem LabelText="Ética laboral"></telerik:AxisItem>
                                            <telerik:AxisItem LabelText="Integridad cívica"></telerik:AxisItem>
                                        </Items>
                                    </XAxis>
                                    <YAxis Visible="true" MinValue="0" MaxValue="100">
                                        <LabelsAppearance DataFormatString="{0}%"></LabelsAppearance>
                                        <MajorGridLines Color="#c8c8c8" Width="1"></MajorGridLines>
                                        <MinorGridLines Visible="true"></MinorGridLines>
                                    </YAxis>
                                </PlotArea>
                                <Appearance>
                                    <FillStyle BackgroundColor="Transparent"></FillStyle>
                                </Appearance>
                                <Legend>
                                    <Appearance BackgroundColor="Transparent" Position="Bottom">
                                    </Appearance>
                                </Legend>
                            </telerik:RadHtmlChart>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadGrid ID="grdtiva" ShowHeader="false" runat="server" GridLines="None" AutoGenerateColumns="false">
                                <MasterTableView>
                                    <Columns>
                                        <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="" DataField="NB_TITULO" HeaderStyle-Width="200"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="" DataField="VALOR" HeaderStyle-Width="200" DataFormatString="{0:N2}"></telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>

                        <div style="clear: both; height: 10px"></div>

                        <div runat="server" style="display: none;" id="divIndiceG0">
                            <div class="ctrlBasico" style="margin-left: 20px; margin-right: 20px;">
                                <label><span style="color: #151515; margin-left: 20px; margin-right: 20px;">Índice global de integridad</span> </label>
                                <br />
                                <h4>El resultado de la prueba TIVA es poco confiable, debido a la variabilidad en los enfoques de las elecciones de esta persona. Es probable que la persona no haya comprendido la prueba o bien, sea poco congruente en sus respuestas. TIVA NO PUEDE DAR UN RESULTADO CONFIABLE. Se recomienda utilizar otras herramientas (referencias, pruebas proyectivas, etc.) o bien, tener precaución al contratar / evaluar a esta persona.</h4>
                            </div>
                        </div>
                        <div runat="server" style="display: none;" id="divIndiceG1">
                            <div class="ctrlBasico" style="margin-left: 20px; margin-right: 20px;">
                                <label><span style="color: #151515;">Índice global de integridad</span> </label>
                                <br />
                                <h4>Esta persona es flexible en sus enfoques, mostrando poco interés e importancia en el cumplimiento de normas y reglas. Tenderá a anteponer el logro de sus objetivos ante la imposición de estructuras o imposiciones éticas.</h4>
                            </div>
                        </div>
                        <div runat="server" style="display: none;" id="divIndiceG2">
                            <div class="ctrlBasico" style="margin-left: 20px; margin-right: 20px;">
                                <label><span style="color: #151515;">Índice global de integridad</span> </label>
                                <br />
                                <h4>Esta persona muestra compromiso y preocupación por las consecuencias de sus actos, sólo cuando se relacionan con el cumplimiento de sus objetivos. Se mostrará flexible en sus enfoques. Tiene la capacidad de negociar y adecuar las reglas de manera que se ajusten al logro de sus objetivos. Tiende a actuar de manera neutral ante situaciones en los que tenga que mostrar alguna postura ética.</h4>
                            </div>
                        </div>
                        <div runat="server" style="display: none;" id="divIndiceG3">
                            <div class="ctrlBasico" style="margin-left: 20px; margin-right: 20px;">
                                <label><span style="color: #151515;">Índice global de integridad</span> </label>
                                <br />
                                <h4>Esta persona muestra una alta rigidez en su forma de actuar ante decisiones éticas. Tiende a ser una persona con alta conciencia social que promueve valores o creencias, en ocasiones imponiendo su punto de vista. Tiende a juzgar lo que es bueno y lo que es malo según su escala de valores. Por lo general será directo, y honesto, externando sus opiniones, aún cuando éstas resulten incómodas o poco populares.</h4>
                            </div>
                        </div>

                        <div runat="server" style="display: none;" id="divPersonal">
                            <div class="ctrlBasico" style="margin-left: 20px; margin-right: 20px;">
                                <label><span style="color: #151515;">Integridad personal</span> </label>
                                <br />
                                <h4>Esta persona muestra alta congruencia entre lo que dice y lo que piensa, por lo general actúa de manera conciente y se responsabiliza por sus actos. Se caracteriza por tener firmes sus valores y convicciones, actuando generalmente acorde a ellos.</h4>
                            </div>
                        </div>

                        <div runat="server" style="display: none; margin-left: 20px; margin-right: 20px;" id="divReglamentos">
                            <div class="ctrlBasico" style="margin-left: 20px; margin-right: 20px;">
                                <label><span style="color: #151515;">Apego a leyes y reglamentos</span> </label>
                                <br />
                                <h4>Esta persona es altamente apegada a las reglas y reglamentos que son impuestos por una institución y/o una autoridad. Será poco flexible al romper con una regla y se sentirá muy incómodo cuando otros lo hacen, pudiendo incluso denunciar la falta a un tercero.</h4>
                            </div>
                        </div>

                        <div runat="server" style="display: none; margin-left: 20px; margin-right: 20px;" id="divEtica">
                            <div class="ctrlBasico" style="margin-left: 20px; margin-right: 20px;">
                                <label><span style="color: #151515;">Integridad y ética laboral</span> </label>
                                <br />
                                <h4>Esta persona se muestra con altos niveles de integridad laboral, presentándose como alguien rígido en cuestión del cumplimiento de reglamentos de trabajo, horarios o incluso normas de conducta éticas laborales. Respetará a la autoridad y a las políticas, incomodándose incluso con quienes no lo hacen.</h4>
                            </div>
                        </div>

                        <div runat="server" style="display: none; margin-left: 20px; margin-right: 20px;" id="divCivica">
                            <div class="ctrlBasico" style="margin-left: 20px; margin-right: 20px;">
                                <label><span style="color: #151515;">Integridad cívica</span> </label>
                                <br />
                                <h4>Esta persona muestra niveles altos de conciencia social y cívica, cumpliendo con normas de urbanidad que servirán para la convivencia social, pero así mismo con una alta conciencia ecológica y/o patriótica. Cumplirán sus obligaciones ciudadanas, además de promover y defender hasta el cansancio sus valores y creencias.</h4>
                            </div>
                        </div>
                         <div style="clear: both; height: 10px"></div>
                          <div class="divControlDerecha">
                                <telerik:RadButton ID="btnTiva" runat="server" AutoPostBack="false" Text="Imprimir" OnClientClicked="OpenTiva"></telerik:RadButton>
                            </div>

                    </telerik:RadPageView>

                    <telerik:RadPageView ID="RPVOrtografias" runat="server">
                        <div style="clear: both; height: 10px"></div>
                        <div>
                            <div class="ctrlBasico" style="margin-left: 20px;">
                                <div style="clear: both"></div>
                                <div>
                                    <telerik:RadHtmlChart runat="server" ID="RadChartOrtografias" Width="500px" Height="400px">
                                        <ChartTitle Text="">
                                        </ChartTitle>
                                        <PlotArea>
                                            <Appearance>
                                                <FillStyle BackgroundColor="Transparent"></FillStyle>
                                            </Appearance>
                                            <XAxis AxisCrossingValue="0" Color="black" MajorTickType="Outside" MinorTickType="Outside"
                                                Reversed="false">
                                                <Items>
                                                    <telerik:AxisItem LabelText="Ortografía I"></telerik:AxisItem>
                                                    <telerik:AxisItem LabelText="Ortografía II"></telerik:AxisItem>
                                                    <telerik:AxisItem LabelText="Ortografía III"></telerik:AxisItem>

                                                </Items>
                                                <LabelsAppearance DataFormatString="{0}" RotationAngle="280" Skip="0" Step="1">
                                                </LabelsAppearance>
                                            </XAxis>
                                            <YAxis AxisCrossingValue="0" Color="black" MajorTickSize="1" MajorTickType="Outside"
                                                MinorTickSize="1" MinorTickType="Outside" MinValue="0" Reversed="false"
                                                Step="25">
                                                <LabelsAppearance DataFormatString="{0:N2}%" RotationAngle="0" Skip="0" Step="1">
                                                </LabelsAppearance>
                                                <TitleAppearance Position="Center" RotationAngle="0" Text="">
                                                </TitleAppearance>
                                            </YAxis>

                                            <Series>
                                                <telerik:ColumnSeries>
                                                    <TooltipsAppearance Color="White" />
                                                </telerik:ColumnSeries>
                                            </Series>
                                        </PlotArea>
                                        <Legend>
                                            <Appearance Visible="false" />
                                        </Legend>
                                    </telerik:RadHtmlChart>
                                </div>
                            </div>

                            <div style="height: 10px"></div>
                            <div class="ctrlBasico">
                                <telerik:RadGrid ID="grdOrtografias" ShowHeader="true" runat="server" GridLines="None" AutoGenerateColumns="false">
                                    <MasterTableView>
                                        <Columns>
                                            <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="" DataField="NB_TITULO" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="Aciertos" DataField="ACIERTOS" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="Valores máximos" DataField="VALORES_MAXIMOS" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="Porcentaje de aciertos" DataField="PORCENTAJE" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <div style="clear: both"></div>

                            <div class="ctrlBasico" style="margin-left: 20px;">
                                <table class="ctrlTableForm">
                                    <tr>
                                        <td class="ctrlTableDataContext">
                                            <label>Total:</label></td>
                                        <td colspan="2" class="ctrlTableDataBorderContext">
                                            <div id="txtPorcentajeTotal" runat="server" style="min-width: 100px;"></div>
                                        </td>
                                        <td class="ctrlTableDataBorderContext">
                                            <div id="txtNivel" runat="server" width="170" maxlength="1000" enabled="false"></div>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                        </div>
                        <div class="divControlDerecha">
                                <telerik:RadButton ID="btnOrtografia" runat="server" AutoPostBack="false" Text="Imprimir" OnClientClicked="OpenOrtografia"></telerik:RadButton>
                            </div>
                    </telerik:RadPageView>

                    <telerik:RadPageView ID="RPVTecnicaPC" runat="server">

                        <div style="clear: both; height: 10px"></div>
                        <div>
                            <div class="ctrlBasico" style="margin-left: 20px;">
                                <div style="clear: both"></div>
                                <div>
                                    <telerik:RadHtmlChart runat="server" ID="RadHtmlCSIH" Width="500px" Height="400px">
                                        <ChartTitle Text="">
                                        </ChartTitle>
                                        <PlotArea>
                                            <Appearance>
                                                <FillStyle BackgroundColor="Transparent"></FillStyle>
                                            </Appearance>
                                            <XAxis AxisCrossingValue="0" Color="black" MajorTickType="Outside" MinorTickType="Outside"
                                                Reversed="false">
                                                <Items>
                                                    <telerik:AxisItem LabelText="Comunicación"></telerik:AxisItem>
                                                    <telerik:AxisItem LabelText="Sotfware"></telerik:AxisItem>
                                                    <telerik:AxisItem LabelText="Internet"></telerik:AxisItem>
                                                    <telerik:AxisItem LabelText="Hardware"></telerik:AxisItem>
                                                </Items>
                                                <LabelsAppearance DataFormatString="{0}" RotationAngle="360" Skip="0" Step="1">
                                                </LabelsAppearance>
                                            </XAxis>
                                            <YAxis AxisCrossingValue="0" Color="black" MajorTickSize="1" MajorTickType="Outside"
                                                MinorTickSize="1" MinorTickType="Outside" MinValue="0" Reversed="false"
                                                Step="25">
                                                <LabelsAppearance DataFormatString="{0}%" RotationAngle="0" Skip="0" Step="1">
                                                </LabelsAppearance>
                                                <TitleAppearance Position="Center" RotationAngle="0" Text="Puntaje">
                                                </TitleAppearance>
                                            </YAxis>

                                            <Series>
                                                <telerik:ColumnSeries>
                                                    <TooltipsAppearance Color="White" />
                                                </telerik:ColumnSeries>
                                            </Series>
                                        </PlotArea>
                                        <Legend>
                                            <Appearance Visible="false" />
                                        </Legend>
                                    </telerik:RadHtmlChart>
                                </div>
                            </div>

                            <div style="height: 10px"></div>
                            <div class="ctrlBasico">
                                <telerik:RadGrid ID="grdTecnicaPC" ShowHeader="true" runat="server" GridLines="None" AutoGenerateColumns="false">
                                    <MasterTableView>
                                        <Columns>
                                            <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="" DataField="NB_TITULO" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="Aciertos" DataField="ACIERTOS" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="Valores máximos" DataField="VALORES_MAXIMOS" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="Porcentaje de aciertos" DataField="PORCENTAJE" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>

                            <div style="height: 10px"></div>
                            <div class="ctrlBasico">
                                <telerik:RadGrid ID="grdTecnicaMensajesRes" ShowHeader="false" runat="server" GridLines="None" AutoGenerateColumns="false">
                                    <MasterTableView>
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderText="" DataField="NB_TITULO" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="" DataField="PORCENTAJE" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="" DataField="MENSAJE" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <div style="clear: both"></div>
                        </div>
                         <div class="divControlDerecha">
                                <telerik:RadButton ID="btnTecnica" runat="server" AutoPostBack="false" Text="Imprimir" OnClientClicked="OpenTecnica"></telerik:RadButton>
                            </div>
                    </telerik:RadPageView>

                     <telerik:RadPageView ID="RPVRedaccion" runat="server">
                        L
                    </telerik:RadPageView>

                    <telerik:RadPageView ID="RPVIngles" runat="server">

                        <div class="ctrlBasico" style="margin-left: 20px; margin-right: 20px;">
                            <telerik:RadGrid ID="grdIngles" ShowHeader="true" runat="server" GridLines="None" AutoGenerateColumns="false">
                                <MasterTableView>
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderText="" DataField="NB_TITULO" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="Aciertos" DataField="ACIERTOS" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="Valores máximos" DataField="VALORES_MAXIMOS" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderText="Porcentaje de aciertos" DataField="PORCENTAJE" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>

                        <div style="clear: both; height: 10px;"></div>

                        <div class="ctrlBasico" style="margin-left: 20px; margin-right: 20px; border: 1px solid lightgray;">
                            <div id="lblNivel" runat="server" style="width: 400px; font-weight: bold;"></div>

                            <%--<div style="clear: both; height:5px;"></div>--%>

                            <div id="lblInformacion" runat="server" style="width: 400px; font-weight: lighter;"></div>
                        </div>
                        <%--<div style="width: 100%; height: 10px;"></div>--%>
                        <div style="clear: both; height: 10px;"></div>
                       <div class="divControlDerecha">
                                <telerik:RadButton ID="btnIngles" runat="server" AutoPostBack="false" Text="Imprimir" OnClientClicked="OpenIngles"></telerik:RadButton>
                            </div>
                    </telerik:RadPageView>
                   
                    <telerik:RadPageView ID="RPVEntrevista" runat="server">
                        No hay resultados
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RPVNoResultados" runat="server">
                        No hay resultados
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>

    <div style="clear: both; height: 10px;"></div>
    <%--   <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnTerminar" runat="server" OnClientClicking="close_window" OnClick="btnTerminar_Click" Text="Siguiente" AutoPostBack="true"></telerik:RadButton>
        </div>
    </div>--%>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
