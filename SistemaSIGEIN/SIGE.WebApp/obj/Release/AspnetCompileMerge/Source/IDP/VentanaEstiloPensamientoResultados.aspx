<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaEstiloPensamientoResultados.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaEstiloPensamientoResultados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <h4>Estilo de pensamiento</h4>
    <div style="height: 10px; clear: both"></div>

    <div>
        <div class="ctrlBasico">

            <div>
                <telerik:RadHtmlChart runat="server" ID="PolarLineChart" Width="400" Height="400" Transitions="true" Skin="Silk">
                    <PlotArea>
                        <Series>
                            <telerik:PolarLineSeries Name="Antenna">
                                <TooltipsAppearance Color="#ffffff"></TooltipsAppearance>
                                <SeriesItems>
                                    <telerik:PolarSeriesItem Angle="0" Radius="0"></telerik:PolarSeriesItem>
                                    <telerik:PolarSeriesItem Angle="10" Radius="0"></telerik:PolarSeriesItem>
                                    <telerik:PolarSeriesItem Angle="20" Radius="0"></telerik:PolarSeriesItem>
                                    <telerik:PolarSeriesItem Angle="30" Radius="-1"></telerik:PolarSeriesItem>
                                    <telerik:PolarSeriesItem Angle="40" Radius="-2"></telerik:PolarSeriesItem>
                                    <telerik:PolarSeriesItem Angle="50" Radius="-3"></telerik:PolarSeriesItem>
                                    <telerik:PolarSeriesItem Angle="60" Radius="-5"></telerik:PolarSeriesItem>
                                    <telerik:PolarSeriesItem Angle="70" Radius="-7"></telerik:PolarSeriesItem>
                                    <telerik:PolarSeriesItem Angle="80" Radius="-10"></telerik:PolarSeriesItem>
                                    <telerik:PolarSeriesItem Angle="90" Radius="-13"></telerik:PolarSeriesItem>
                                    <telerik:PolarSeriesItem Angle="100" Radius="-16"></telerik:PolarSeriesItem>
                                    <telerik:PolarSeriesItem Angle="110" Radius="-20"></telerik:PolarSeriesItem>
                                    <telerik:PolarSeriesItem Angle="250" Radius="-20"></telerik:PolarSeriesItem>
                                    <telerik:PolarSeriesItem Angle="260" Radius="-16"></telerik:PolarSeriesItem>
                                    <telerik:PolarSeriesItem Angle="270" Radius="-13"></telerik:PolarSeriesItem>
                                    <telerik:PolarSeriesItem Angle="280" Radius="-10"></telerik:PolarSeriesItem>
                                    <telerik:PolarSeriesItem Angle="290" Radius="-7"></telerik:PolarSeriesItem>
                                    <telerik:PolarSeriesItem Angle="300" Radius="-5"></telerik:PolarSeriesItem>
                                    <telerik:PolarSeriesItem Angle="310" Radius="-3"></telerik:PolarSeriesItem>
                                    <telerik:PolarSeriesItem Angle="320" Radius="-2"></telerik:PolarSeriesItem>
                                    <telerik:PolarSeriesItem Angle="330" Radius="-1"></telerik:PolarSeriesItem>
                                    <telerik:PolarSeriesItem Angle="340" Radius="0"></telerik:PolarSeriesItem>
                                    <telerik:PolarSeriesItem Angle="350" Radius="0"></telerik:PolarSeriesItem>
                                    <telerik:PolarSeriesItem Angle="0" Radius="0"></telerik:PolarSeriesItem>
                                </SeriesItems>
                                <Appearance FillStyle-BackgroundColor="#00adcc"></Appearance>
                                <MarkersAppearance Visible="false"></MarkersAppearance>
                            </telerik:PolarLineSeries>
                        </Series>
                        <XAxis Color="#b3b3b3" Reversed="false" Step="30" StartAngle="90">
                            <LabelsAppearance RotationAngle="0" DataFormatString="{0}&#176;"></LabelsAppearance>
                            <MajorGridLines Width="1"></MajorGridLines>
                            <MinorGridLines Visible="false"></MinorGridLines>
                        </XAxis>
                        <YAxis Color="#b3b3b3" MajorTickSize="1" MajorTickType="Outside" Reversed="false">
                            <LabelsAppearance DataFormatString="{0}dBi" RotationAngle="0" Skip="0" Step="1"></LabelsAppearance>
                            <MajorGridLines Width="1"></MajorGridLines>
                            <MinorGridLines Visible="false"></MinorGridLines>
                        </YAxis>
                    </PlotArea>
                    <ChartTitle Text="Patch Antenna Directivity Pattern">
                        <Appearance Align="Center" Position="Top">
                        </Appearance>
                    </ChartTitle>
                    <Legend>
                        <Appearance Visible="false">
                        </Appearance>
                    </Legend>
                </telerik:RadHtmlChart>

            </div>

        </div>
        <div class="ctrlBasico">

            <div>
                <table>
                    <tbody>
                        <tr>
                            <td style="width: 145px;">&nbsp;</td>
                            <td style="width: 20px; text-align: left; font-weight: bold; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;">Análisis</td>
                            <td style="width: 80px; text-align: right; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;"><span id="lblT" style="text-align: right;">65.50</span></td>
                        </tr>

                        <tr>
                            <td style="width: 145px;">&nbsp;</td>
                            <td style="width: 20px; text-align: left; font-weight: bold; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;">Visión</td>
                            <td style="width: 80px; text-align: right; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;"><span id="Span1" style="text-align: right;">57.00</span></td>
                        </tr>
                        <tr>
                            <td style="width: 145px;">&nbsp;</td>
                            <td style="width: 20px; text-align: left; font-weight: bold; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;">Intuición</td>
                            <td style="width: 80px; text-align: right; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;"><span id="Span2" style="text-align: right;">58.50</span></td>
                        </tr>
                        <tr>
                            <td style="width: 145px;">&nbsp;</td>
                            <td style="width: 20px; text-align: left; font-weight: bold; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;">Lógica</td>
                            <td style="width: 80px; text-align: right; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;"><span id="Span3" style="text-align: right;">65.50</span></td>
                        </tr>
                    </tbody>
                </table>
            </div>

        </div>
        <div style="clear: both;height:10px"></div>

        <div>

            <table border="0">
									<tbody>
										
										<tr>
											
											<td>
												<p style="font-size: 11pt; font-weight: bold;">Visión</p>
												<p style="font-size: 11pt;">
													Esta persona es un soñador de mente abierta en el sentido positivo de la palabra.
													Posiblemente es un pensador que usa la gestalt para visualizar la imagen panorámica.
													Tiende a sintetizar la información que le llega de diversas fuentes para crear nuevos conceptos.
													Esta persona está dispuesta a arriesgarse, experimentando con ideas y relaciones hasta descubrir la comunicación correcta.
													Se llega al estado de lo correcto intuitivamente y con frecuencia es un estado que no se puede explicar.
													El aprendizaje es de tipo visual, pictorial y experiencial, generalmente sin análisis y por lo tanto, se capta una compresión
													superficial del asunto aunque le falta detalle. Su habla puede carecer de sintaxis y brincar de un pensamiento a otro,
													de inspiración a inspiración.
												</p>
											</td>
										</tr>  
										<tr>
											
											<td>
												<p style="font-size: 11pt; font-weight: bold;">Lógica</p>
												<p style="font-size: 11pt;">
													Su estilo de pensamiento está vinculado con dar forma, conservar los recursos, organizar y administrar
													recursos de manera conservadora. Posee un pensamiento normativo, trae el orden a su ambiente. Los problemas son
													abordados como un recetario de cocina, las soluciones con frecuencia se basan en su instinto y se apoya en la lógica.
													Esta persona exhibe una forma de pensar conservadora y el comportamiento adecuado. Los hechos, las cifras y el detalle.
													Es gente que piensa como un capataz exacto y preciso, recolectando datos, prescribiendo métodos y ayudando a llevar la cuenta.
													Esta persona tiene un sexto sentido de lo correcto. Su habla es articulada y se centra en los datos, procesos,
													procedimientos y tareas más que en los sentimientos y la gente.
												</p>
											</td>
										</tr>  
									    <tr><td><p style="font-size: 11pt; font-weight: bold;">Intuición</p><p style="font-size: 11pt;">
									Es un pensador intuitivo, emotivo y centrado en los sentimientos. Para él los sentimientos son hechos:
									los sentimientos son más importantes que la llamada prueba científica. Esta persona sabe instintivamente e intuitivamente,
									sin lógica o razonamiento. Tiene un radar social que le ayuda a juzgar el tenor de las relaciones sociales o interpersonales.
									Debido a esta elevada conciencia o responsabilidad empática esta persona con frecuencia emigra hacia otros puestos que
									tienen mucho involucramiento con la gente. Sus conversaciones están orientadas emocionalmente y son perceptivas.
									Tiene un sentido intuitivo de la armonía y evita los conflictos que puedan afectar la retroinformación que necesita
									de los demás. Es un pensador que aprende por observación y no tiende a disecar y reacomodar la información.
									La información se acepta como se presenta así como es el mundo. Esta persona se sintoniza con su ambiente.
								</p></td></tr>  <tr><td><p style="font-size: 11pt; font-weight: bold;">Analítico</p><p style="font-size: 11pt;">
									Lógico, analítico, objetivo. Este estilo de pensamiento se alimenta de la información técnica y de los hechos.
									La toma de decisiones se  basa en una razón sin emotividad y en los hechos relevantes. La persona que exhibe este
									tipo de pensamiento analítico tiende a sospechar de las personas y sistemas que carecen de validación científica.
									Aprende mediante el análisis de los conceptos y la información, examinando críticamente a cada componente.
									Su habla se mueve ordenadamente de punto a punto; los demás con frecuencia etiquetan a esta persona de intelectual.
									La cuantificación, las pruebas, la lógica, la matemática, etc. son cosas sumamente valorizadas por estas personas.
									Tienden a  alegar, como medio para extraer información de los demás. Prefiere que la información se le presente verbalmente.
								</p></td></tr>  <tr><td><p style="font-size: 11pt; font-weight: bold;">Analítico - Visión</p><p style="font-size: 11pt;">
									Esta persona tiene una marcada preferencia  por el pensamiento cognoscitivo, teórico e intelectual y evita el estilo 
									de procesamiento visceral, afectivo y emocional.  Dependiendo de la situación, esta persona tendrá una preferencia 
									igual por la información que involucre probabilidades, hechos, datos, lógica, conceptos, síntesis e intuición. 
									Tiene un  gran respeto  por las teorías, las ideas y la lógica pero poco respeto a lo estructurado, controlado, 
									planeado al detalle, normativo e interpersonal. En él es característico un procesamiento mental emocional – experiencial.
								</p></td></tr>  <tr><td><p style="font-size: 11pt; font-weight: bold;">Análisis - Lógico</p><p style="font-size: 11pt;">
									Tiene una marcada preferencia por el pensamiento del cerebro izquierdo, esto lo conduce a concentrarse en datos,
									hechos y tareas. Los procesos de pensamiento fluyen lógicamente de punto a punto, en contraste con las personas
									de cerebro derecho cuyos pensamientos brincan de una idea a otra. La confianza en las políticas y los procedimientos
									se combinan con el análisis, la cuantificación, la programación y la toma lógica de decisiones. Los procesos
									de pensamiento  están basados en emociones, sentimientos y síntesis. Es un pensador bien organizado y disciplinado,
									efectivo en la administración de sistemas y disciplinas técnicas o científicas.
								</p></td></tr>  <tr><td><p style="font-size: 11pt; font-weight: bold;">Visión - Intuición</p><p style="font-size: 11pt;">
									La gente que tiene una marcada preferencia hacia el cerebro derecho tiende a depender de los procesos de pensamiento 
									visuales, espaciales, intuitivos, conceptuales y sensibles. Es creativo, de mente abierta, experimental y holístico 
									lo que ayuda en la planeación de largo plazo y a ver la imagen panorámica. Disfruta de las experiencias interpersonales, 
									emocionales, espirituales y musicales.  Las funciones analíticas de pensamiento lógico requeridas por las funciones de 
									investigación o contabilidad serán percibidas por esta persona como limitantes de sus abordajes preferidos de tipo exploratorio 
									y experimental. Esta persona tiende a soñar las ideas y dejar su implementación  o el análisis de costo / beneficio a las 
									personas de cerebro izquierdo.
								</p></td></tr>  <tr><td><p style="font-size: 11pt; font-weight: bold;">Visión - Lógica</p><p style="font-size: 11pt;">
									Trabaja ambos hemisferios cerebrales de diferente forma.  Busca organizar, conservar y administrar adecuadamente 
									los recursos de manera  conservadora. Trae el orden a su ambiente.  Se basa con frecuencia en su instinto y se apoya 
									en la lógica. Es de mente abierta y soñador en el sentido positivo de la palabra, tiende a sistematizar la información 
									que le llega de diversas fuentes para crear nuevos conceptos. Esta persona está dispuesta a experimentar con ideas nuevas 
									y relacionar hasta lograr la combinación correcta. El aprendizaje es de tipo visual y experiencial. Los hechos, las cifras 
									y el detalle son importantes. Piensa como un capataz exacto y preciso, recolectando datos, prescribiendo métodos y 
									ayudando a llevar la cuenta.
								</p></td></tr>  <tr><td><p style="font-size: 11pt; font-weight: bold;">Intuición - Lógica</p><p style="font-size: 11pt;">
									Esta persona tiene un énfasis dominante en la región afectiva y visceral del cerebro, esta persona exhibe distinta 
									preferencia por el orden, la seguridad y los sistemas así como también lo interpersonal. Expresa vulnerabilidad 
									emocional. La toma de decisiones se basa en el instinto y / o la intuición que opaca la lógica y la razón, o a una 
									integración e innovación de mente más abierta. Los procesos de pensamiento que esta persona evita son la gestalt, 
									el holismo, la creatividad, el análisis y el examen crítico. Se ocupa mucho de los asuntos concretos, es decir, 
									las estructuras y los sentimientos. Los planes y la gente.
								</p></td></tr>  <tr><td><p style="font-size: 11pt; font-weight: bold;">Análisis - Lógica - Intuición</p><p style="font-size: 11pt;">
									Esta persona tiene una marcada preferencia por el pensamiento del cerebro izquierdo, esto lo conduce a concentrarse 
									en datos, hechos y tareas. Los procesos de pensamiento fluyen lógicamente de punto a punto, en contraste con las 
									personas de cerebro derecho, sin embargo esta persona ha desarrollado la cualidad de la intuición por lo que todo 
									análisis será juzgado por su intuición como un radar social que le ayuda a juzgar el tenor de las relaciones 
									sociales o interpersonales. Debido a esta elevada conciencia o responsabilidad empática, esta persona con frecuencia 
									emigra hacia puestos que tienen mucho involucramiento con la gente.
								</p></td></tr>  <tr><td><p style="font-size: 11pt; font-weight: bold;">Análisis - Visión - Lógica - Intuición</p><p style="font-size: 11pt;">
									Esta persona usa los cuatro cuadrantes de cerebro con la misma facilidad. Su preferencia se acomoda a la situación 
									que tiene enfrente de él, de manera que pueda analizar los conceptos con la misma facilidad con que puede 
									organizar y sintonizarse con los sentimientos de las personas involucradas. Esta persona puede soñar una visión y 
									llevarla a rendir frutos. Es percibido por la mayoría de la gente como verdaderamente ingenioso y creativo. 
									Con frecuencia se dice de esta persona: “si tienes un problema, llévaselo a...”.
								</p></td></tr>  
								</tbody>
								</table>
							

        </div>
    </div>
</asp:Content>
