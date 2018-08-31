<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaInteresesPersonalesResultados.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaInteresesPersonalesResultados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <h4>Intereses Personales</h4>
    <div style="clear:both;height:10px"></div>
    <div>
        <div class="ctrlBasico">
            <table>
                <tbody>
                    <tr>
                        <td style="width: 145px;">&nbsp;</td>
                        <td style="width: 20px; text-align: left; font-weight: bold; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;">Teórico</td>
                        <td style="width: 80px; text-align: right; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;"><span id="lblT" style="text-align: right;">76.67</span></td>
                    </tr>
                    <tr>
                        <td style="width: 145px;">&nbsp;</td>
                        <td style="width: 20px; text-align: left; font-weight: bold; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;">Económico</td>
                        <td style="width: 80px; text-align: right; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;"><span id="lblE" style="text-align: right;">41.67</span></td>
                    </tr>
                    <tr>
                        <td style="width: 145px;">&nbsp;</td>
                        <td style="width: 20px; text-align: left; font-weight: bold; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;">Artístico</td>
                        <td style="width: 80px; text-align: right; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;"><span id="lblA" style="text-align: right;">73.33</span></td>
                    </tr>
                    <tr>
                        <td style="width: 145px;">&nbsp;</td>
                        <td style="width: 20px; text-align: left; font-weight: bold; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;">Social</td>
                        <td style="width: 80px; text-align: right; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;"><span id="lblS" style="text-align: right;">56.67</span></td>
                    </tr>
                    <tr>
                        <td style="width: 145px;">&nbsp;</td>
                        <td style="width: 20px; text-align: left; font-weight: bold; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;">Político</td>
                        <td style="width: 80px; text-align: right; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;"><span id="lblP" style="text-align: right;">48.33</span></td>
                    </tr>
                    <tr>
                        <td style="width: 145px;">&nbsp;</td>
                        <td style="width: 20px; text-align: left; font-weight: bold; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;">Regulatorio</td>
                        <td style="width: 80px; text-align: right; border-bottom-color: gray; border-bottom-width: 1px; border-bottom-style: dotted;"><span id="lblR" style="text-align: right;">53.33</span></td>
                    </tr>
                </tbody>
            </table>
        </div>

        <div style="clear: both"></div>

        <div class="ctrlBasico">

            <div>

                <telerik:RadHtmlChart runat="server" ID="LineChart" Width="432px" Height="247px" Transitions="true" Skin="Silk">
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
                            <p>
                                <b>TEORICO ALTO:</b><br>
                                Su interés dominante es la búsqueda de la verdad. Está interesado en un proceso de razonamiento lógico y secuencial. Tiene una alta puntuación mostrándose como intelectual, pone en orden las cosas e interrelaciona todo dentro de un sistema lógico. Más que aceptar las cosas por el valor que aparenten, es objetivo, crítico y busca los hechos. Tiende a preferir ideas o cosas, en lugar de personas. Trata de ordenar y sistematizar el conocimiento con investigación y validación.
                            </p>

                            <p>
                                <b>ECONOMICO ALTO:</b><br>
                                Esta persona se caracteriza por un interés común hacia las ganancias económicas. Ve todos los objetos, ideas y cosas de su medio ambiente como parte de una estructura materialista. Es práctico y tiende a fijar un valor monetario sobre las cosas. Busca la utilidad y la inversión potencial. Tiene un deseo personal de ganancia material y en el sentido del trabajo, tiene una apreciación hacia los resultados positivos como centro de utilidad. Respeta los logros económicos de otros.
                            </p>

                            <p>
                                <b>ESTETICO ALTO:</b><br>
                                Esta persona busca la belleza artística o la creatividad en áreas de expresión cultural. Busca la forma y la armonía, la gracia y la simetría. Sus percepciones pueden variar desde individualista. Es apto para ser perfeccionista en el diseño, el color y los detalles. Demanda libertad para crear sus propias cosas. Su aguda sensibilidad por lo bello, puede estar acompañada  por su intolerancia a lo feo. Objeta la falta de sensibilidad refinada cuando es vista en un comercialismo grueso. En las ventas puede tener éxito porque esta persona sirve a otros con un interés similar. En la administración, tiende a tener relaciones armoniosas.
                            </p>


                            <p>
                                <b>SOCIAL ALTO:</b><br>
                                Este valor implica un sentimiento altruista por la gente, tanto extraños como conocidos. Esta persona busca desinteresadamente mejorar el bienestar de otros sirviéndoles. Procura ayudar a toda clase de personas: quienes están aventajados y/o se sientan maltratados. Sus simpatizantes son impulsados a la acción por un sentido de justicia social. Sus juicios son subjetivos, matizados de emociones e idealismo. La indignación social frecuentemente causa conflictos con el individuo de valores económicos.
                            </p>

                            <p>
                                <b>POLITICO ALTO:</b><br>
                                Esta persona busca status y poder de una naturaleza individual. Busca ser colocado por encima de otros en la organización jerárquica y estructural. Disfruta siendo influyente y se siente estimulado, más que turbado por el reconocimiento personal. El puntaje político alto, está fuertemente motivado para ganar status, reconocimiento y control. 
                            </p>

                            <p>
                                <b>REGULATORIO ALTO:</b><br>
                                Esta persona busca identificarse con una fuerza reconocida por el bien o gobernar su vida por un código de conducta que prometerá aprobación o aceptación por una alta autoridad. Busca unidad en su propio cosmos y una relación con esa totalidad. Lo correcto o incorrecto es importante para esta persona y tiende a hacer juicios morales de conformidad con ellos. Quiere estar en lo correcto. Generalmente tiende a ser cooperativo, controlar y a observar estándares establecidos.
                            </p>

                            <p>
                                <b>TEORICO BAJO:</b><br>
                                Este individuo se forma opiniones de aspectos o situaciones rápidamente. Siente que sus instintos son correctos y que una gran cantidad de investigación no es requerida. Dominado por la emoción, tiende a aceptar las cosas por su valor aparente. Prefiere tratar más con las emociones de la gente, que con la investigación científica de los hechos. Frecuentemente piensa que el tiempo utilizado en cuestionamientos del cómo y del por qué de una situación, es tiempo perdido. Por esto, prefiere expresar sentimientos y opiniones, más que hechos científicamente validados.                         
                            </p>

                            <p>
                                <b>ECONOMICO BAJO:</b><br>
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
                                <b>POLITICO BAJO:</b><br>
                                El ejercicio del poder no vale la pena, debido a las adversidades que uno debe afrontar al ganarlo. Una persona con puntuaciones bajas en esta área está particularmente consciente de los riesgos del manejo del poder y de que requiere de contacto con personas o situaciones no deseables. Sin embargo, está dispuesto a ejercer el liderazgo detrás de escena para ganar una causa.
                            </p>

                            <p>
                                <b>REGULATORIO BAJO:</b><br>
                                Este individuo es independiente e individualista. Quiere tomar decisiones independientes a códigos o costumbres. Se resiste a ser cuidadoso y resentirá que otros prescriban por él. Puede interpretar la ley para sus propias necesidades y racionalizar para justificar sus acciones individuales.
                            </p>

                            <p>
                                <b>TEORICO – REGULATORIO:</b><br>
                                Esta persona tiene juicios morales basados en un fundamento teórico puro. Preocupado por el mantenimiento del orden de su cosmos, esta persona continuamente expondrá sus creencias a un riguroso examen. Cuestionador y lógico, irá detrás del reconocimiento de un orden existente para  preguntar ¿Por qué?. Es generalmente un individuo cooperativo y disciplinado, guiado por un profundo sentido de moralidad.
                            </p>

                            <p>
                                <b>POLITICO –ECONOMICO:</b><br>
                                Esta persona disfruta estando a cargo de las empresas, pues disfruta ejerciendo su autoridad y dirigiendo a otros hacia fines o resultados. También disfruta la naturaleza competitiva de los negocios, luchando duro para ganar. Dotado de una alta dosis de pragmatismo y de un agudo sentido de los valores, concentra sus recursos donde tiene más posibilidades de ganar. Usualmente disfruta del reconocimiento y el status y puede encontrar maneras tangibles de publicar su posición por medio de símbolos de status como automóviles caros y oficinas grandes y bien amuebladas.
                            </p>

                            <p>
                                <b>ARTISTICO - ESTETICO:</b><br>
                                Busca la belleza artística o la creatividad en áreas de expresión cultural. Busca la forma y la armonía, la gracia y la simetría. Sus percepciones pueden variar. Esta persona es apta para ser perfeccionista en el diseño, el color y los detalles. Demanda libertad para crear sus propias cosas. Su aguda sensibilidad por lo bello, puede estar acompañada por su intolerancia a lo feo. Objeta la falta de sensibilidad refinada cuando es vista en un comercialismo grueso.
                            </p>

                            <p>
                                <b>TEORICO – ECONOMICO:</b><br>
                                Esta persona disfruta descubriendo ideas nuevas y útiles. Cae dentro del mundo de la ciencia o de los conceptos. Este estilo se refuerza por encontrar aplicaciones prácticas y útiles para sus ideas. Aplica frecuentemente sistemas lógicos a la administración, esforzándose por hacer una ciencia más que un arte. Mientras esta persona es frecuentemente muy creativa, usualmente canaliza su creatividad hacia lo tangible y lo objetivo y la mayoría de las veces mide sus éxitos en pesos y centavos.
                            </p>

                            <p>
                                <b>TEORICO -  POLITICO:</b><br>
                                Esta persona está enfocada al éxito y a tener influencia sobre los otros. Utiliza el conocimiento y el cálculo de opciones para favorecer sus ambiciones. Es personalmente ambicioso, atraído por el mundo de la razón y de los hechos. Puede parecer no estar consciente de las consecuencias sociales de sus actos. Por esta razón puede ser muy efectivo en situaciones que requieren de un razonamiento frío asociado con acciones firmes y positivas. Usualmente son respetados por su fuerza ante la adversidad y su habilidad para pensar bajo presión.
                            </p>

                            <p>
                                <b>TEORICO- SOCIAL:</b><br>
                                Este individuo tiende a ser altruista y desinteresado, pues busca ayudar a su prójimo a través del uso del conocimiento y la razón. Convencido de que el conocimiento y el entendimiento puede proporcionar las mejores soluciones a los problemas, esta persona propone sistemas y lógica para enfrentar la mayoría de los problemas. Su deseo de ayudar a otros, va acompañado de una curiosidad innata, llevándole a confiar en la investigación y en la aplicación de métodos científicos para la solución de problemas humanos.
                            </p>

                            <p>
                                <b>ECONOMICO – REGULATORIO:</b><br>
                                Tiende a guiarse por principios y un estricto sentido del orden. Motivado a maximizar beneficios, aplica sistemas y disciplinas que monitorean y controlan la actividad económica. Tiende a creer que toda la actividad de la empresa debe ser guiada por un plan o un grupo de estándares y principios predeterminados que sirvan como modelo para la acción. Aunque es generalmente cooperativo, puede revelarse a algo que contradice su noción de lo que es correcto. 
                            </p>

                            <p>
                                <b>ESTETICO – POLITICO:</b><br>
                                Este individuo puede sentirse dividido en ocasiones entre su deseo de poder y su deseo de paz y armonía. Tiende a ser sensitivo al color, la luz, el sonido y la forma, y quizá especialmente talentoso en el empleo de los mismos para expresar sus pensamientos y sus sentimientos. Usualmente ambicioso, empujará hasta llegar a la cumbre de su campo o para asegurar que sus enfoques o puntos de vista sean los ganadores del día.
                            </p>

                            <p>
                                <b>TEORICO – ESTETICO:</b><br>
                                Este individuo es generalmente gracioso y armonioso. Es reflexivo y naturalmente inquisitivo, muestra una alta sensibilidad al color, el diseño, el balance y la forma. El idealismo caracteriza a este estilo, que es devoto de la armonía y la belleza, así como de la dedicación a aprender la verdad. Frecuentemente trabaja con facilidad, pues usa su conocimiento para preservar la existencia de balance o crear un nuevo orden en el nombre de la armonía o la belleza.
                            </p>

                            <p>
                                <b>ECONOMICO – ESTETICO:</b><br>
                                Este individuo tiende a ser armonioso, busca un ambiente sereno y elegante. Tiende a coleccionar objetos bellos de valor. Aprecia la gracia y la belleza de las cosas, pero también busca algo de utilidad. Para esta persona, la belleza por sí misma es fría. Sin embargo, toda la vida es más rica cuando la belleza es parte de ella. Generalmente es sensitivo al color, la forma y los sonidos y se esfuerza por lograr el balance y la armonía. De este modo, sus relaciones con otros son usualmente positivas y pacíficas.
                            </p>

                            <p>
                                <b>ECONOMICO – SOCIAL:</b><br>
                                Este individuo tiende a seguir la creencia de proveer el máximo beneficio, por el máximo número y el menor costo. De este modo, extiende sus recursos hasta el límite para promover el bienestar social. Tiene un buen sentido del valor y se inclina hacia el manejo o solución de problemas de tipo práctico. No obstante, es sensitivo y se preocupa por la gente. Frecuentemente dedica su tiempo y energía desinteresadamente a ayudar a otros.
                            </p>

                            <p>
                                <b>ESTETICO – SOCIAL:</b><br>
                                Este individuo tiende a buscar la expresión artística de sus inquietudes o amor hacia los demás. Generalmente es sensitivo y gracioso, por lo que establece contacto con los demás de una manera cálida y personal, llegándose a ver envuelto en el caos de la vida de otros. Altamente armonizante con la belleza de su mundo, ve como una fuerte de felicidad que disfruta al participar con otra gente.
                            </p>

                            <p>
                                <b>ESTETICO REGULATORIO:</b><br>
                                Este individuo es sereno y disciplinado, aprecia el balance y el orden de su universo. Busca la forma, la gracia y la simetría. Es cooperativo y auto controlado, tiende a gobernarse así mismo por un profundo sentido de lo correcto y lo incorrecto. Se identifica con una fuerza reconocida del bien y busca la unidad en el cosmos.
                            </p>

                            <p>
                                <b>POLITICO – SOCIAL:</b><br>
                                Este individuo es activista social, el campeón de los desvalidos. Profundamente sensitivo al sufrimiento humano, este individuo se siente impulsado a tomar la acción, hacer cambios y corregir errores. Es ambicioso y disfruta teniendo el poder y el control para imponer sus ideas. Generalmente interesado en promover el bienestar de otros. Compite agresivamente por su causa. Tales personas están listas para comprometerse en una controversia para favorecer sus objetivos.
                            </p>

                            <p>
                                <b>SOCIAL – REGULATORIO:</b><br>
                                Este individuo tiende a ser ordenado, así como cálido y amistoso. Generalmente es un individuo guiado por la ética y por un profundo sentido de lo correcto o lo incorrecto. Tiende a poner los problemas humanos en la perspectiva de algún orden cósmico. Motivado a promover la armonía y a ser cooperativo, se resistirá a cualquier acción que afecte sus creencias o estándares.
                            </p>

                            <p>
                                <b>POLITICO – REGULATORIO:</b><br>
                                Este individuo busca status e imponer sus ideas de acuerdo a lo correcto o incorrecto. Tiende a gobernarse a sí mismo por un código de conducta en obediencia y una alta autoridad. Busca unidad en su propio cosmos y una relación con esta totalidad. Busca control sobre otros, tratando de hacerlo desde su propio punto de vista acerca de lo que el mundo debería ser. Dispuesto a pelear por sus  creencias, desafía cualquier oponente en el nombre de lo moralmente justo y distingue el bien del mal, luchando siempre del lado del bien.
                            </p>

                        </td>
                    </tr>
                </tbody>
            </table>

        </div>
    </div>



</asp:Content>
