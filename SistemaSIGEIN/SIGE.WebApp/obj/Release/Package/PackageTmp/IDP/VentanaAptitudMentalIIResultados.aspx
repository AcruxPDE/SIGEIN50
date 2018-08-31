<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaAptitudMentalIIResultados.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaAptitudMentalIIResultados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <style>
        td {
            margin: 5px;
        }

        .sombra {
            -webkit-box-shadow: 4px 4px 17px 0px rgba(0,0,0,0.75);
            -moz-box-shadow: 4px 4px 17px 0px rgba(0,0,0,0.75);
            box-shadow: 4px 4px 17px 0px rgba(0,0,0,0.75);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <h4>Aptitud Mental I</h4>
    <div style="clear: both; height: 10px"></div>

    <div style=" border:1px solid #ddd; width:30%; padding-left:10px;">
        <table>
            <tr>
                <td>Puntuación directa:
                </td>
                <td>65
                </td>
            </tr>
            <tr>
                <td>Coeficiente intelectual (CI):
                </td>
                <td>114
                </td>
            </tr>
            <tr>
                <td>Inteligencia:
                </td>
                <td style="background: rgba(0, 128, 0, 0.28)">Inteligencia superior
                </td>
            </tr>
        </table>
    </div>

    <div style="clear: both; height: 10px"></div>

    <div class="ctrlBasico" style="border:1px solid #ddd">
    <div>
        <div>
            <label style="color: red">Magnitud de problemas que puede resolver de forma satisfactoria: Inteligencia</label>
        </div>
        <div style="clear: both; height: 10px"></div>

        <div style="position: relative">
            <div class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 45px; margin-right: 10px; width: 100px; text-align: center; line-height: 43px; margin-top: 5px">Simples 
                <div style="clear: both"></div>
                <div style="text-align: center;font-size: 12px;width: 46px;">-79</div>
            </div>
            <div class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 45px; margin-right: 10px; width: 100px; text-align: center; line-height: 43px; margin-top: 5px">Comunes 
                <div style="clear: both"></div>
                <div style="text-align: center;font-size: 12px;width: 46px;">80-89</div>
            </div>
            <div class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 45px; margin-right: 10px; width: 100px; text-align: center; margin-top: 5px">Término medio 
                <div style="clear: both"></div>
                <div style="text-align: center;font-size: 12px;width: 46px; line-height: 49px;">90-109</div>
            </div>
            <div class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 45px; margin-right: 10px; width: 100px; text-align: center; line-height: 43px; margin-top: 5px">Difíciles
                <div style="clear: both"></div>
                <div style="text-align: center;font-size: 12px;width: 46px;">110-119</div>
            </div>
            <div class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 45px; margin-right: 10px; width: 100px; text-align: center; margin-top: 5px">De lo más complejo  
                <div style="clear: both"></div>
                <div style="text-align: center;font-size: 12px;width: 46px; line-height: 49px;">120+</div>
            </div>
            <div style="clear: both"></div>
        </div>

    </div>

    <div style="clear: both; height: 40px"></div>

    <div>
        <div>
            <label style="color: red">Comparativamente es más inteligente que este porcentaje de las personas</label>
        </div>
        <div style="clear: both; height: 10px"></div>

        <div style="position: relative">
            <div class="ctrlBasico sombra" style="border: 1px solid #ddd;line-height: 40px; height: 40px; margin-right: 15px; width: 40px; text-align: center; margin-top: 5px">10 
                <div style="clear: both"></div>
                <div style="text-align: center;font-size: 12px;width: 46px;">-69</div>
            </div>
            <div class="ctrlBasico sombra" style="border: 1px solid #ddd;line-height: 40px; height: 40px; margin-right: 15px; width: 40px; text-align: center; margin-top: 5px">10 
                <div style="clear: both"></div>
                <div style="text-align: center;font-size: 12px;width: 46px;">70-89</div>
            </div>
            <div class="ctrlBasico sombra" style="border: 1px solid #ddd;line-height: 40px; height: 40px; margin-right: 15px; width: 40px; text-align: center; margin-top: 5px">20 
                <div style="clear: both"></div>
                <div style="text-align: center;font-size: 12px;width: 46px;">80-84</div>
            </div>
            <div class="ctrlBasico sombra" style="border: 1px solid #ddd;line-height: 40px; height: 40px; margin-right: 15px; width: 40px; text-align: center; margin-top: 5px">30
                <div style="clear: both"></div>
                <div style="text-align: center;font-size: 12px;width: 46px;">85-90</div>
            </div>
            <div class="ctrlBasico sombra" style="border: 1px solid #ddd;line-height: 40px; height: 40px; margin-right: 15px; width: 40px; text-align: center; margin-top: 5px">40  
                <div style="clear: both"></div>
                <div style="text-align: center;font-size: 12px;width: 46px;">90-99</div>
            </div>
            <div class="ctrlBasico sombra" style="border: 1px solid #ddd;line-height: 40px; height: 40px; margin-right: 15px; width: 40px; text-align: center; margin-top: 5px">50  
                <div style="clear: both"></div>
                <div style="text-align: center;font-size: 12px;width: 46px;">100-109</div>
            </div>
            <div class="ctrlBasico sombra" style="border: 1px solid #ddd;line-height: 40px; height: 40px; margin-right: 15px; width: 40px; text-align: center; margin-top: 5px">60  
                <div style="clear: both"></div>
                <div style="text-align: center;font-size: 12px;width: 46px;">110-114</div>
            </div>
            <div class="ctrlBasico sombra" style="border: 1px solid #ddd;line-height: 40px; height: 40px; margin-right: 15px; width: 40px; text-align: center; margin-top: 5px">70  
                <div style="clear: both"></div>
                <div style="text-align: center;font-size: 12px;width: 46px;">115-119</div>
            </div>
            <div class="ctrlBasico sombra" style="border: 1px solid #ddd;line-height: 40px; height: 40px; margin-right: 15px; width: 40px; text-align: center; margin-top: 5px">80  
                <div style="clear: both"></div>
                <div style="text-align: center;font-size: 12px;width: 46px;">120-124</div>
            </div>
            <div class="ctrlBasico sombra" style="border: 1px solid #ddd;line-height: 40px; height: 40px; margin-right: 15px; width: 40px; text-align: center; margin-top: 5px">90  
                <div style="clear: both"></div>
                <div style="text-align: center;font-size: 12px;width: 46px;">125+</div>
            </div>
            <div style="clear: both"></div>
        </div>

    </div>


    <div style="clear: both; height: 50px"></div>

    <div>
        <div>
            <label style="color: red"> Su forma de resolver problemas </label>
        </div>
        <div style="clear: both; height: 10px"></div>

        <div style="position: relative">
            <div class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 45px; margin-right: 10px; width: 100px; text-align: center; margin-top: 5px">Sin orden Disperso 
                <div style="clear: both"></div>
                <div style="text-align: center;font-size: 12px;width: 46px;line-height: 49px;">9+</div>
            </div>
            <div class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 45px; margin-right: 10px; width: 100px; text-align: center; margin-top: 5px">Tiende a ser Disperso 
                <div style="clear: both"></div>
                <div style="text-align: center;font-size: 12px;width: 46px;line-height: 49px;">6-8</div>
            </div>
            <div class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 45px; margin-right: 10px; width: 100px; text-align: center; margin-top: 5px">Ningún extremo 
                <div style="clear: both"></div>
                <div style="text-align: center;font-size: 12px;width: 46px; line-height: 49px;">3-5</div>
            </div>
            <div class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 45px; margin-right: 10px; width: 100px; text-align: center; margin-top: 5px">Tiende a ser metódico
                <div style="clear: both"></div>
                <div style="text-align: center;font-size: 12px;width: 46px;line-height: 49px;">1-2</div>
            </div>
            <div class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 45px; margin-right: 10px; width: 100px; text-align: center; margin-top: 5px">Metódico organizado  
                <div style="clear: both"></div>
                <div style="text-align: center;font-size: 12px;width: 46px; line-height: 49px;">0</div>
            </div>
            <div style="clear: both"></div>
        </div>

    </div>


    <div style="clear: both; height: 50px"></div>

    <div>
        <div>
            <label style="color: red"> Su velocidad en tareas mentales </label>
        </div>
        <div style="clear: both; height: 10px"></div>

        <div style="position: relative">
            <div class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 59px; margin-right: 10px; width: 100px; text-align: center; margin-top: 5px"> 39 <br />Muy lento 
                <div style="clear: both"></div>
                <div style="text-align: center;font-size: 12px;width: 46px;line-height: 81px;">30 min</div>
            </div>
            <div class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 59px; margin-right: 10px; width: 100px; text-align: center; margin-top: 5px"> 40-59 <br />Lento 
                <div style="clear: both"></div>
                <div style="text-align: center;font-size: 12px;width: 46px;line-height: 81px;">30 min</div>
            </div>
            <div class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 59px; margin-right: 10px; width: 100px; text-align: center; margin-top: 5px">60-74 <br />Ningun extremo 
                <div style="clear: both"></div>
                <div style="text-align: center;font-size: 12px;width: 46px; line-height: 42px;">30 min</div>
            </div>
            <div class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 59px; margin-right: 10px; width: 100px; text-align: center; margin-top: 5px">75 <br />Rápido
                <div style="clear: both"></div>
                <div style="text-align: center;font-size: 12px;width: 46px;line-height: 81px;">30 min</div>
            </div>
            <div class="ctrlBasico sombra" style="border: 1px solid #ddd; height: 59px; margin-right: 10px; width: 100px; text-align: center; margin-top: 5px">>75 <br />Muy rápido  
                <div style="clear: both"></div>
                <div style="text-align: center;font-size: 12px;width: 46px; line-height: 81px;">25 min</div>
            </div>
            <div style="clear: both"></div>
        </div>

    </div>

    </div>

    <div class="ctrlBasico">

        <div style="text-align:center; margin: 0 auto">

            <telerik:RadHtmlChart runat="server" ID="ChartWithColumnsAndLineSeries" Width="500px" Height="400px">
                    <ChartTitle Text="Chart with Line and Column series">
                    </ChartTitle>
                    <PlotArea>
                        <Series>
                            <telerik:ColumnSeries>
                                <SeriesItems>
                                    <telerik:CategorySeriesItem Y="34" />
                                    <telerik:CategorySeriesItem Y="76" />
                                    <telerik:CategorySeriesItem Y="23" />
                                    <telerik:CategorySeriesItem Y="15" />
                                    <telerik:CategorySeriesItem Y="84" />
                                </SeriesItems>
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
    <div style="clear:both"></div>
</asp:Content>
