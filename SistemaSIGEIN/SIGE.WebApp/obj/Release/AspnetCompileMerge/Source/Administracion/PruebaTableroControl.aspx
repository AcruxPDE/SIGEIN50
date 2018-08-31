<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="PruebaTableroControl.aspx.cs" Inherits="SIGE.WebApp.Administracion.PruebaTableroControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script src="../Assets/js/jquery.min.js"></script>
    <style>
        table {
            *border-collapse: collapse; /* IE7 and lower */
            border-spacing: 0;
        }

        th:first-child {
            border-radius: 6px 0 0 0;
        }

        th:last-child {
            border-radius: 0 6px 0 0;
        }

        th:only-child {
            border-radius: 6px 6px 0 0;
        }


        .bordered tr:hover {
            background: #fbf8e9;
            transition: all 0.1s ease-in-out;
        }

        .highlight {
            background: #fbf8e9;
            transition: all 0.1s ease-in-out;
        }

        .zebra tbody tr:nth-child(even) {
            background: #f5f5f5;
            box-shadow: 0 1px 0 rgba(255,255,255,.8) inset;
        }


        .bordered {
            border: solid #ccc 1px;
            -moz-border-radius: 6px;
            -webkit-border-radius: 6px;
            border-radius: 6px;
            -webkit-box-shadow: 0 1px 1px #ccc;
            -moz-box-shadow: 0 1px 1px #ccc;
            box-shadow: 0 1px 1px #ccc;
        }

            .bordered th {
                background-color: #dce9f9;
                background-image: -webkit-gradient(linear, left top, left bottom, from(#ebf3fc), to(#dce9f9));
                background-image: -webkit-linear-gradient(top, #ebf3fc, #dce9f9);
                background-image: -moz-linear-gradient(top, #ebf3fc, #dce9f9);
                background-image: -ms-linear-gradient(top, #ebf3fc, #dce9f9);
                background-image: -o-linear-gradient(top, #ebf3fc, #dce9f9);
                background-image: linear-gradient(top, #ebf3fc, #dce9f9);
                -webkit-box-shadow: 0 1px 0 rgba(255,255,255,.8) inset;
                -moz-box-shadow: 0 1px 0 rgba(255,255,255,.8) inset;
                box-shadow: 0 1px 0 rgba(255,255,255,.8) inset;
                border-top: none;
                text-shadow: 0 1px 0 rgba(255,255,255,.5);
            }

            .bordered tr:last-child td:first-child {
                -moz-border-radius: 0 0 0 6px;
                -webkit-border-radius: 0 0 0 6px;
                border-radius: 0 0 0 6px;
            }

            .bordered tr:last-child td:last-child {
                -moz-border-radius: 0 0 6px 0;
                -webkit-border-radius: 0 0 6px 0;
                border-radius: 0 0 6px 0;
            }

            .bordered tr:hover {
                background: #fbf8e9;
                -o-transition: all 0.1s ease-in-out;
                -webkit-transition: all 0.1s ease-in-out;
                -moz-transition: all 0.1s ease-in-out;
                -ms-transition: all 0.1s ease-in-out;
                transition: all 0.1s ease-in-out;
            }

            .bordered td, .bordered th {
                border-left: 1px solid #ccc;
                border-top: 1px solid #ccc;
                padding: 10px;
                text-align: left;
            }

                .bordered td:first-child, .bordered th:first-child {
                    border-left: none;
                }

                .bordered th:first-child {
                    -moz-border-radius: 6px 0 0 0;
                    -webkit-border-radius: 6px 0 0 0;
                    border-radius: 6px 0 0 0;
                }

                .bordered th:last-child {
                    -moz-border-radius: 0 6px 0 0;
                    -webkit-border-radius: 0 6px 0 0;
                    border-radius: 0 6px 0 0;
                }

                .bordered th:only-child {
                    -moz-border-radius: 6px 6px 0 0;
                    -webkit-border-radius: 6px 6px 0 0;
                    border-radius: 6px 6px 0 0;
                }

        .NoBorder {
            border: 0, 0, 0, 0;
            background-color: white;
        }

        .divNecesario {
            height: 30px;
            width: 15px;
            border-radius: 5px;
            background: red;
            float: left;
        }

        .divIntermedio {
            height: 30px;
            width: 15px;
            border-radius: 5px;
            background: gold;
            float: left;
        }

        .divBajo {
            height: 30px;
            width: 15px;
            border-radius: 5px;
            background: green;
            float: left;
        }

          .divNa {
            height: 30px;
            width: 15px;
            border-radius: 5px;
            background: gray;
            float: left;
        }
    </style>
    <script type="text/javascript">

        $('.bordered tr').mouseover(function () {
            $(this).addClass('highlight');
        }).mouseout(function () {
            $(this).removeClass('highlight');
        });

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadSplitter ID="rsPlantilla" Width="100%" Height="100%" BorderSize="0" runat="server">
        <telerik:RadPane ID="rpGridEvaluados" runat="server" Height="100%" ShowContentDuringLoad="false">
            <div style="width:100%; height: calc(100% - 50px);">
            <table class="bordered">
                <thead>
                    <tr>
                        <th colspan="2" style="background-color: white;"></th>
                        <th style="text-align: center; color: #4D8900; border-color:#4D8900;">
                                Resultados De Pruebas
                                <br />
                                Compatibilidad Vs. Puesto
                        </th>
                        <th style="text-align: center; color: #FF7400;" colspan="3">Evaluación De Competencias<br />
                            Compatibilidad con el puesto</th>
                        <th style="text-align: center; color: #A20804;" colspan="3">Evaluación De Desempeño<br />
                            Porcentaje de cumplimieto</th>
                        <th style="text-align: center; color: #A20804;">Clima Laboral<br />
                            Resultado</th>
                        <th style="text-align: center; color: #0087CF;">Evaluación Del Sueldo<br />
                            Situación salarial</th>
                        <th colspan="2" style="text-align: center;">Tendencia</th>                        
                        <th rowspan="2" class="bordered_th" style="width: 150px; text-align: center;">Promedio</th>
                        <th rowspan="2" class="bordered_th" style="width: 150px; text-align: center;">Bono</th>
                    </tr>
                    <tr>
                        <th class="bordered_th">#</th>
                        <th class="bordered_th" style="width: 350px;">Nombre/Puesto</th>
                        <th class="bordered_th" style="text-align: center;">CH002</th>
                        <th class="bordered_th" style="width: 100px; text-align: center;">Periodo 4</th>
                        <th class="bordered_th" style="width: 100px; text-align: center;">Periodo 5</th>
                        <th class="bordered_th" style="width: 100px; text-align: center;">Periodo 7</th>
                        <th class="bordered_th" style="width: 100px; text-align: center;">Periodo 1</th>
                        <th class="bordered_th" style="width: 100px; text-align: center;">Periodo 2</th>
                        <th class="bordered_th" style="width: 100px; text-align: center;">Periodo 9</th>
                        <th class="bordered_th" style="width: 100px; text-align: center;">Periodo 4</th>
                        <th class="bordered_th" style="width: 100px; text-align: center;">Versión Tab ADM 13</th>
                        <th class="bordered_th" style="width:100px; text-align:center;">Evaluacion de competencias</th>
                        <th class="bordered_th" style="width:100px; text-align:center;">Evaluacion de desempeño</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>1</td>
                        <td>Larrea, Miriam<br />
                            Coordinador de capacitación y desarrollo
                        </td>
                        <td>
                            <div style="width: 75%; float: left; text-align: center;">85.1% </div>
                            <div class="divIntermedio"></div>
                        </td>
                        <td>
                            <div style="width: 75%; float: left; text-align: center;">83.0% </div>
                            <div class="divIntermedio"></div>
                        </td>
                        <td>
                            <div style="width: 75%; float: left; text-align: center;">86.1% </div>
                            <div class="divIntermedio"></div>
                        </td>
                        <td>
                            <div style="width: 75%; float: left; text-align: center;">N/A </div>
                            <div class="divNa"></div>
                        </td>
                        <td>
                            <div style="width: 75%; float: left; text-align: center;">60.0% </div>
                            <div class="divIntermedio"></div>
                        </td>
                        <td>
                            <div style="width: 75%; float: left; text-align: center;">15.0% </div>
                            <div class="divNecesario"></div>
                        </td>
                        <td>
                            <div style="width: 75%; float: left; text-align: center;">88.8% </div>
                            <div class="divBajo"></div>
                        </td>
                        <td>
                            <div style="width: 75%; float: left; text-align: center;">62.5% </div>
                            <div class="divIntermedio"></div>
                        </td>
                        <td>
                            <div style="width: 75%; float: left; text-align: center;">-75.7% </div>
                            <div class="divNecesario"></div>
                        </td>
                        <td>
                            <div style="width: 75%; float: left; text-align: center;">Alta </div>
                            <div class="divBajo"></div>
                        </td>
                        <td>
                            <div style="width: 75%; float: left; text-align: center;">Alta </div>
                            <div class="divBajo"></div>
                        </td>
                        <td>
                            <div style="width: 75%; float: left; text-align: center;">56.7% </div>
                            <div class="divNecesario"></div>
                        </td>
                        <td>
                            <div style="width: 90%; float: left; text-align: center;">$ 3,465.96 </div>
                        </td>
                    </tr>
                </tbody>
            </table>
            </div>
            <div style="clear:both; height:5px;"></div>
            <div class="divControlDerecha">
                <telerik:RadButton runat="server" ID="btnGuardar" Text="Guardar" AutoPostBack="false"></telerik:RadButton>
            </div>
            </telerik:RadPane>
            <telerik:RadPane ID="rpAyuda" runat="server" Width="22px" ShowContentDuringLoad="false">
                <telerik:RadSlidingZone ID="rszPrograma" runat="server" SlideDirection="Left" Width="22px">
                    <telerik:RadSlidingPane ID="rspNuevoPrograma" runat="server" Title="Configuración de tablero" Width="340px" RenderMode="Mobile" Height="200">
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <label>Resultados de pruebas:</label>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <telerik:RadTextBox runat="server" ID="txtClavePrograma" Width="150px" AutoPostBack="false"></telerik:RadTextBox>
                        </div>
                        <div style="clear: both; height: 5px;"></div>
                        <div class="ctrlBasico">
                            <label>Evaluación de competencias</label>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <telerik:RadTextBox runat="server" ID="txtNombrePrograma" Width="150px" AutoPostBack="false"></telerik:RadTextBox>
                        </div>
                        <div style="clear: both; height: 5px;"></div>
                        <div class="ctrlBasico">
                            <label>Evaluación de desempeño</label>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <telerik:RadTextBox runat="server" ID="RadTextBox1" Width="150px" AutoPostBack="false"></telerik:RadTextBox>
                        </div>
                        <div style="clear: both; height: 5px;"></div>
                        <div class="ctrlBasico">
                            <label>Clima laboral</label>
                        </div>
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <telerik:RadTextBox runat="server" ID="RadTextBox2" Width="150px" AutoPostBack="false"></telerik:RadTextBox>
                        </div>
                        <div style="clear: both; height: 5px;"></div>
                        <div class="ctrlBasico">
                            <telerik:RadButton runat="server" ID="btnGuardarPrograma" Text="Cambiar ponderación" AutoPostBack="false" CssClass="btnGuardarPrograma" ToolTip="Esta opción te permite crear un programa de capacitación a partir de los resultados obtenidos en un periodo de Detección de Necesidades de Capacitación (DNC). Selecciona el periodo deseado:"></telerik:RadButton>
                        </div>
                        <div style="clear: both; height: 5px;"></div>
                        <div class="ctrlBasico">
                            <telerik:RadButton runat="server" ID="RadButton1" Text="Recalcular" AutoPostBack="false" CssClass="btnGuardarPrograma" ToolTip="Esta opción te permite crear un programa de capacitación a partir de los resultados obtenidos en un periodo de Detección de Necesidades de Capacitación (DNC). Selecciona el periodo deseado:"></telerik:RadButton>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
    </telerik:RadSplitter>
</asp:Content>
