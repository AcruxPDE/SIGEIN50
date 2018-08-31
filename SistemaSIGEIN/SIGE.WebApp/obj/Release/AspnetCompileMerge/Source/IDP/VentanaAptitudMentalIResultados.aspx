<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaAptitudMentalIResultados.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaAptitudMentalIResultados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <h4>Aptitud Mental I</h4>
    <div style="clear: both; height: 10px"></div>
    <div>
        <div class="ctrlBasico">

            <div>

                <telerik:RadHtmlChart runat="server" ID="ChartWithColumnsAndLineSeries" Width="500px" Height="400px" >
                    <ChartTitle Text="Chart with Line and Column series"></ChartTitle>
                    
                    <PlotArea>
                        <XAxis Reversed="true" StartAngle="90">
                            <Items>
                                <telerik:AxisItem LabelText="Prueba1" />
                                
                            </Items>
                        </XAxis>
                        <Series>
                            <telerik:RadarLineSeries>
                                <SeriesItems>
                                    <telerik:CategorySeriesItem Y="34" />
                                    <telerik:CategorySeriesItem Y="76" />
                                    <telerik:CategorySeriesItem Y="23" />
                                    <telerik:CategorySeriesItem Y="15" />
                                    <telerik:CategorySeriesItem Y="84" />
                                </SeriesItems>
                            </telerik:RadarLineSeries>                            
                        </Series>
                    </PlotArea>
                    <Legend>
                        <Appearance Visible="false" />
                    </Legend>
                </telerik:RadHtmlChart>

            </div>

        </div>
        <div class="ctrlBasico">

            <div>

                <table>
                    <tbody>
                        <tr>
                            <td style="width: 50px;">&nbsp;</td>
                            <td style="width: 200px; text-align: left; font-weight: bold; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;">Coeficiente intelectual</td>
                            <td style="width: 150px; text-align: right; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;"><span id="lblT" style="text-align: right;">Inteligencia superior </span></td>
                        </tr>

                        <tr>
                            <td style="width: 50px;">&nbsp;</td>
                            <td style="width: 200px; text-align: left; font-weight: bold; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;">Capacidad de aprendizaje</td>
                            <td style="width: 150px; text-align: right; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;"><span id="Span1" style="text-align: right;">Superior </span></td>
                        </tr>
                        <tr>
                            <td style="width: 50px;">&nbsp;</td>
                            <td style="width: 200px; text-align: left; font-weight: bold; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;">Puntuación total</td>
                            <td style="width: 150px; text-align: right; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;"><span id="Span2" style="text-align: right;">164  </span></td>
                        </tr>
                        <tr>
                            <td style="width: 50px;">&nbsp;</td>
                            <td style="width: 200px; text-align: left; font-weight: bold; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;">CI</td>
                            <td style="width: 150px; text-align: right; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;"><span id="Span3" style="text-align: right;">111 </span></td>
                        </tr>
                    </tbody>
                </table>

            </div>
        </div>
        <div style="clear: both"></div>
    </div>
</asp:Content>
