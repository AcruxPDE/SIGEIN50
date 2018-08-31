<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaPersonalidadLabIResultados.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaPersonalidadLabIResultados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <h4>Personalidad Laboral I</h4>
    <div style="">
        <div class="ctrlBasico">
            <table>
                <tbody>
                    <tr>
                        <td style="border: 1px solid gray; width: 100px; background-color: rgba(204, 204, 204, 0.44);">&nbsp;</td>
                        <td style="border: 1px solid gray; width: 100px; text-align: center; font-weight: bold; background-color: rgba(204, 204, 204, 0.44);">D</td>
                        <td style="border: 1px solid gray; width: 100px; text-align: center; font-weight: bold; background-color: rgba(204, 204, 204, 0.44);">I</td>
                        <td style="border: 1px solid gray; width: 100px; text-align: center; font-weight: bold; background-color: rgba(204, 204, 204, 0.44);">S</td>
                        <td style="border: 1px solid gray; width: 100px; text-align: center; font-weight: bold; background-color: rgba(204, 204, 204, 0.44);">C</td>
                    </tr>
                    <tr>
                        <td style="border: 1px solid gray; width: 100px; text-align: center; font-weight: bold; background-color: rgba(204, 204, 204, 0.44);">M</td>
                        <td style="border: 1px solid gray; width: 100px; text-align: center;"><span id="lblDM">8</span></td>
                        <td style="border: 1px solid gray; width: 100px; text-align: center;"><span id="lblIM">3</span></td>
                        <td style="border: 1px solid gray; width: 100px; text-align: center;"><span id="lblSM">6</span></td>
                        <td style="border: 1px solid gray; width: 100px; text-align: center;"><span id="lblCM">4</span></td>
                    </tr>
                    <tr>

                        <td style="border: 1px solid gray; width: 100px; text-align: center; font-weight: bold; background-color: rgba(204, 204, 204, 0.44);">L</td>
                        <td style="border: 1px solid gray; width: 100px; text-align: center;"><span id="lblDL">2</span></td>
                        <td style="border: 1px solid gray; width: 100px; text-align: center;"><span id="lblIL">3</span></td>
                        <td style="border: 1px solid gray; width: 100px; text-align: center;"><span id="lblSL">12</span></td>
                        <td style="border: 1px solid gray; width: 100px; text-align: center;"><span id="lblCL">6</span></td>
                    </tr>
                    <tr>
                        <td style="border: 1px solid gray; width: 100px; text-align: center; font-weight: bold; background-color: rgba(204, 204, 204, 0.44);">Total:</td>
                        <td style="border: 1px solid gray; width: 100px; text-align: center;"><span id="lblDRes">6</span></td>
                        <td style="border: 1px solid gray; width: 100px; text-align: center;"><span id="lblIRes">0</span></td>
                        <td style="border: 1px solid gray; width: 100px; text-align: center;"><span id="lblSRes">-6</span></td>
                        <td style="border: 1px solid gray; width: 100px; text-align: center;"><span id="lblCRes">-2</span></td>
                    </tr>
                </tbody>
            </table>

            <div style="height: 20px"></div>

            <table>
                <tbody>
                    <tr>
                        <td><span id="lblValidez" style="font-weight: bold;">Validez: -2</span></td>
                        <td>&nbsp;</td>
                    </tr>
                </tbody>
            </table>

            <div style="height: 20px"></div>

            <table>
                <tbody>
                    <tr>
                        <td>
                            <table style="border: 1px solid gray; border-image: none; background-color: whitesmoke;">
                                <tbody>
                                    <tr>
                                        <td>Características sobresalientes de personalidad:</td>
                                        <td><span id="lblComb0">DS DI DC CS CI IS </span></td>
                                    </tr>
                                    <tr>
                                        <td>Situación motivante:</td>
                                        <td><span id="lblComb1">DI</span></td>
                                    </tr>
                                    <tr>
                                        <td>Situación presionante:</td>
                                        <td><span id="lblComb2">DS</span></td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>

        </div>
        <div class="ctrlBasico">
            <div>

                <telerik:RadHtmlChart runat="server" ID="LineChart"  Height="254px" Transitions="true" Skin="Silk" Width="398px">
            <Appearance>
                <FillStyle BackgroundColor="Transparent"></FillStyle>
            </Appearance>
            <ChartTitle Text="Server CPU Load By Days">
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
                        <telerik:AxisItem LabelText="Monday"></telerik:AxisItem>
                        <telerik:AxisItem LabelText="Tuesday"></telerik:AxisItem>
                        <telerik:AxisItem LabelText="Wednesday"></telerik:AxisItem>
                        <telerik:AxisItem LabelText="Thursday"></telerik:AxisItem>
                        <telerik:AxisItem LabelText="Friday"></telerik:AxisItem>
                        <telerik:AxisItem LabelText="Saturday"></telerik:AxisItem>
                        <telerik:AxisItem LabelText="Sunday"></telerik:AxisItem>
                    </Items>
                    <LabelsAppearance DataFormatString="{0}" RotationAngle="0" Skip="0" Step="1">
                    </LabelsAppearance>
                    <TitleAppearance Position="Center" RotationAngle="0" Text="Days">
                    </TitleAppearance>
                </XAxis>
                <YAxis AxisCrossingValue="0" Color="black" MajorTickSize="1" MajorTickType="Outside"
                    MaxValue="100" MinorTickSize="1" MinorTickType="Outside" MinValue="0" Reversed="false"
                    Step="25">
                    <LabelsAppearance DataFormatString="{0}%" RotationAngle="0" Skip="0" Step="1">
                    </LabelsAppearance>
                    <TitleAppearance Position="Center" RotationAngle="0" Text="CPU Load">
                    </TitleAppearance>
                </YAxis>
                <Series>
                    <telerik:LineSeries Name="Week 1">
                        <Appearance>
                            <FillStyle BackgroundColor="#5ab7de"></FillStyle>
                        </Appearance>
                        <LabelsAppearance DataFormatString="{0}%" Position="Above">
                        </LabelsAppearance>
                        <LineAppearance Width="1" />
                        <MarkersAppearance MarkersType="Circle" BackgroundColor="White" Size="8" BorderColor="#5ab7de"
                            BorderWidth="2"></MarkersAppearance>
                        <TooltipsAppearance DataFormatString="{0}%"></TooltipsAppearance>
                        <SeriesItems>
                            <telerik:CategorySeriesItem Y="35"></telerik:CategorySeriesItem>
                            <telerik:CategorySeriesItem Y="52"></telerik:CategorySeriesItem>
                            <telerik:CategorySeriesItem Y="18"></telerik:CategorySeriesItem>
                            <telerik:CategorySeriesItem Y="39"></telerik:CategorySeriesItem>
                            <telerik:CategorySeriesItem></telerik:CategorySeriesItem>
                            <telerik:CategorySeriesItem Y="10"></telerik:CategorySeriesItem>
                            <telerik:CategorySeriesItem Y="6"></telerik:CategorySeriesItem>
                        </SeriesItems>
                    </telerik:LineSeries>
                    <telerik:LineSeries Name="Week 2">
                        <Appearance>
                            <FillStyle BackgroundColor="#2d6b99"></FillStyle>
                        </Appearance>
                        <LabelsAppearance DataFormatString="{0}%" Position="Above">
                        </LabelsAppearance>
                        <LineAppearance Width="1" />
                        <MarkersAppearance MarkersType="Square" BackgroundColor="#2d6b99" Size="8" BorderColor="#2d6b99"
                            BorderWidth="2"></MarkersAppearance>
                        <TooltipsAppearance DataFormatString="{0}%"></TooltipsAppearance>
                        <SeriesItems>
                            <telerik:CategorySeriesItem Y="15"></telerik:CategorySeriesItem>
                            <telerik:CategorySeriesItem Y="23"></telerik:CategorySeriesItem>
                            <telerik:CategorySeriesItem Y="50"></telerik:CategorySeriesItem>
                            <telerik:CategorySeriesItem Y="20"></telerik:CategorySeriesItem>
                            <telerik:CategorySeriesItem Y="93"></telerik:CategorySeriesItem>
                            <telerik:CategorySeriesItem Y="43"></telerik:CategorySeriesItem>
                            <telerik:CategorySeriesItem Y="23"></telerik:CategorySeriesItem>
                        </SeriesItems>
                    </telerik:LineSeries>
                </Series>
            </PlotArea>
        </telerik:RadHtmlChart>

            </div>
        </div>

        <div style="clear: both"></div>

        <div>

            <table>
						<tbody><tr>
							
							<td><b>DS&nbsp;-&nbsp;Urgencia-alcance</b><br>
						Responde rápidamente a los retos, demuestra movilidad y flexibilidad en sus enfoques, 
						tiende a ser iniciador versátil, respondiendo rápidamente a la competencia.
					<br><br><b>DI&nbsp;-&nbsp;Creatividad</b><br>
						Tiende a ser lógico, crítico e incisivo  en sus enfoques hacia la obtención de metas.
						Se sentirá retado por problemas que requieren esfuerzos de análisis y originalidad.
						Será llano y critico con la gente
					<br><br><b>DC&nbsp;-&nbsp;Individualidad</b><br>
						Actúa de manera directa y positiva ante la oposición. Es una persona fuerte que toma posición y lucha 
						por mantenerla. Esta dispuesto a tomar riesgos y puede aún ignorar niveles jerárquicos.
					<br><br><b>CS&nbsp;-&nbsp;Sensibilidad</b><br>
						Esta persona estará muy consciente en evitar riesgos o problemas. Tiende a buscar significados ocultos. La tensión puede ser evidente particularmente si esta bajo presión por obtener resultados. En general se sentirá intranquilo mientras no tenga una confirmación absoluta, de que su decisión ha sido la correcta.
					<br><br><b>CI&nbsp;-&nbsp;Perfeccionismo</b><br>
						Esta persona tiende a ser un seguidor apegado del orden y los sistemas. Toma decisiones basadas en hechos conocidos o procedimientos establecidos. En todas sus actividades, trata meticulosamente de apegarse a los estándares establecidos, ya sea por sí mismo o por los demás.
					<br><br><b>IS&nbsp;-&nbsp;Habilidad de contactos</b><br>
						Tiende a buscar a la gente con entusiasmo y chispa, es una persona abierta que despliega un optimismo contagioso y trata de ganarse a la gente a través de la persuasión de un acercamiento emotivo.
					<br><br><br><hr><br><b>Claves para la motivación de personas con alto grado de empuje</b><p>Para comprender y motivar a la gente, es importante que conozcamos su comportamiento, deseos y lo que necesitan para triunfar, una vez que hemos estudiado a la persona y hemos determinado que su estilo básico de comportamiento es predominantemente alto grado de empuje, entonces los factores abajo anotados, pueden ser claves para el éxito de su motivación, utilizar las listas en forma selectiva y basándonos en los hechos de cada caso especifico.</p><br>
						<p>La persona con alto grado de empuje desea:</p>
						<ol>
							<li>Poder, autoridad.</li>
							<li>Posición y prestigio.</li>
							<li>Dinero y cosas materiales.</li>
							<li>Retos.</li>
							<li>Oportunidad de avance.</li>
							<li>Logros, resultados.</li>
							<li>El saber "por qué".</li>
							<li>Amplio margen para operar.</li>
							<li>Respuestas directas.</li>
							<li>Libertad de controles, supervisión y detalle.</li>
							<li>Eficiencia en la operación.</li>
							<li>Actividades nuevas y variadas.</li>
						</ol>
						<p>Necesita:</p>
						<ol>
							<li>Compromisos negociados de igual a igual.</li>
							<li>Identificación con la compañía.</li>
							<li>Desarrollar valores intrínsecos.</li>
							<li>Aprender a tomar su paso y relajarse.</li>
							<li>Tareas difíciles.</li>
							<li>Saber los resultados esperados.</li>
							<li>Entender a las personas, enfoque lógico.</li>
							<li>Empatía.</li>
							<li>Técnicas basadas en experiencias prácticas.</li>
							<li>Conciencia de que las sanciones existen.</li>
							<li>"Sacudidas ocasionales".</li>
						</ol>
					<br><b>Claves para la motivación de personas con baja influencia</b><p>Para comprender y motivar a la gente, es importante que conozcamos su comportamiento, deseos y lo que necesitan para triunfar, una vez que hemos estudiado a la persona y hemos determinado que su estilo básico de comportamiento es predominantemente bajo grado de influencia, entonces los factores abajo anotados, pueden ser claves para el éxito de su motivación, utilizar las listas en forma selectiva y basándonos en los hechos de cada caso especifico.</p><br>
						<p>La persona con baja influencia desea:</p>
							<ol>
							<li> Que se le deje solo</li>
							<li> Un formato lógico</li>
							<li> Hechos</li>
							<li> Actividades sociales limitadas</li>
							<li> Respeto</li>
							<li> Conversación directa</li>
							<li> Enigmas que resolver</li>
							<li> Equipo para operar</li>
							<li> Experiencias emotivas limitadas</li>
							<li> Objetividad</li>
							</ol>
						<p>Necesita:</p>
							<ol>
							<li> Habilidades sociales</li>
							<li> Contactos con la gente</li>
							<li> Reconocimiento de los sentimientos de los demás</li>
							<li> Un jefe objetivo</li>
							<li> Respuestas lógicas</li>
							<li> La oportunidad para hacer preguntas</li>
							<li> Sinceridad, ninguna sofisticación</li>
							<li> Suavizar las asperezas</li>
							<li> Tiempo para pensar</li>
							<li> Retroalimentación (feedback) de los demás</li>
							</ol>
					<br><br><br><hr><br><p><b>Posibles limitaciones</b></p><p>Debemos recordar que ninguna persona es perfecta en todas las situaciones. Las características sobresalientes de comportamiento que resultan en el éxito en un clima compatible, son las mismas características que pueden llegar a ser factores limitantes en una situación de presión o de stress. Todas las personas tenemos limitaciones. El ejecutivo debe comprender estas posibles limitaciones y estar preparado para manejarlas, puesto que tienden a surgir en las ocasiones en que pueden ser más perjudiciales.</p><br><b>Bajo presión una persona con un alto grado de empuje tiende a</b><br>
						<ol>
						<li> Excederse en sus prerrogativas</li>
						<li> Actuar intrépidamente</li>
						<li> Inspirar temor en los demás</li>
						<li> Imponerse a la gente</li>
						<li> Ser cortante y sarcástico con los demás</li>
						<li> Malhumorarse cuando no tiene el primer lugar</li>
						<li> Ser crítico y buscar errores</li>
						<li> Descuidar los "detalles"</li>
						<li> Mostrarse impaciente y descontento con el trabajo de rutina</li>
						<li> Resistirse a participar como parte de un grupo</li>
						</ol>
					<br><b>Bajo presión una persona con un bajo grado de constancia tiende a</b><br>
						<ol>
						<li> Ser inconsistente</li>
						<li> Dejar inconcluso lo que empieza</li>
						<li> Dedicarse a demasiadas actividades al mismo tiempo</li>
						<li> Tratar de abarcar demasiado</li>
						<li> Hacer cambios drásticos frecuentemente, especialmente al inicio de su carrera</li>
						<li> Ser perturbador</li>
						<li> Difícil de localizar</li>
						<li> Tener problemas de familia y/o salud</li>
						<li> Viajar extensa y costosamente</li>
						<li> Faltar al respeto de la propiedad o territorio de los demás</li>
						</ol>
					<br></td>
						</tr>
					</tbody></table>

        </div>
    </div>
</asp:Content>
