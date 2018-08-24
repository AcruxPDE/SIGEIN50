-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Juan De Dios Pérez Pedroza
-- CREATE date: 27/01/2016
-- Description: Obtiene los Resultados de las pruebas
-- =============================================
CREATE PROCEDURE [IDP].[SPE_OBTIENE_RESULTADOS_BATERIA] 
     @XML_RESULTADO XML = '' OUT  
	,  @PIN_ID_BATERIA int = NULL
	, @PIN_CL_TOKEN_BATERIA AS UNIQUEIDENTIFIER = NULL	
   
AS   


------------------------------------------------INSERTAR MENSAJES LABORAL I-------------------------------------------------------------------

	DECLARE @RESULTADOS TABLE ( -- TABLA TEMPORAL DE RESULTADOS
		 ID_BATERIA INT
		, CL_PRUEBA NVARCHAR(30)
		, NO_VALOR DECIMAL(7,2)
		, CL_VARIABLE NVARCHAR(30)
		, CL_LETRA NCHAR(1)
		, TIPO NVARCHAR(30)
		, NO_ORDEN_ASC INT
		, NO_ORDEN_DESC INT
		
	)

	DECLARE @RESULT XML  = NULL 
    DECLARE @CLAVE_PRUEBAS XML    = NULL
    DECLARE @XML_MENSAJES XML    = NULL
	DECLARE @MESSAGES NVARCHAR(20) =''
	DECLARE @NB_PRUEBA NVARCHAR(10) = 'LABORAL-1'  --CLAVE DE PRUEBA A BUSCAR
		
	------------------------------------------MENSAJES LABORAL I------------------------------------------------------------
	
	INSERT INTO @RESULTADOS  -- LLENAMOS LA TABLA TEMPORAL CON LOS RESULTADOS DE LAS VARIABLES TIPO 3
	SELECT  KBP.ID_BATERIA,CP.CL_PRUEBA,kr.NO_VALOR,CV.CL_VARIABLE,null,null,null,null
	FROM IDP.K_BATERIA_PRUEBA KBP 
	JOIN IDP.K_PRUEBA KP ON KP.ID_BATERIA = KBP.ID_BATERIA
	JOIN IDP.C_PRUEBA CP ON KP.ID_PRUEBA_PLANTILLA = CP.ID_PRUEBA
	LEFT OUTER JOIN IDP.K_RESULTADO KR ON KP.ID_CUESTIONARIO = KR.ID_CUESTIONARIO
	LEFT OUTER JOIN IDP.C_VARIABLE CV ON CV.ID_VARIABLE = KR.ID_VARIABLE
    WHERE (@PIN_ID_BATERIA IS NULL OR (@PIN_ID_BATERIA IS NOT NULL AND KBP.ID_BATERIA = @PIN_ID_BATERIA))
	AND (@PIN_CL_TOKEN_BATERIA IS NULL OR (@PIN_CL_TOKEN_BATERIA IS NOT NULL AND KBP.[CL_TOKEN] = @PIN_CL_TOKEN_BATERIA))
    AND KR.CL_TIPO_RESULTADO = 3
	

	IF @NB_PRUEBA  IN (SELECT CL_PRUEBA FROM @RESULTADOS) -- COMPARAMOS QUE EXISTA EL NOMBRE DE LA PRUEBA EN LA TABLA TEMPORAL DE RESULTADOS
	BEGIN

		DECLARE @MENSAJES TABLE ( -- SI EXISTE CREAMOS UNA TABLA TEMPORAL DE MENSAJES EN DONDE SE ALMACENARAN TODOS LOS MENSAJES Y TODAS LAS COMBINACIONES POSIBLES
		  CL_MENSAJE NVARCHAR(10)
		, NB_TITULO NVARCHAR(200)
		, DS_MENSAJE NVARCHAR(MAX)
		, TIPO_MENSAJE NVARCHAR(30)
		, SECCION  NVARCHAR(20)
	)

	DECLARE @CONFIGURACION_CLAVES TABLE ( -- TABLA DE CONFIGURACION DONDE LE DECIMOS A @RESULTADOS QUIEN ES D, I, S, C
		  CL_VARIABLE NVARCHAR(30)
		, CL_LETRA NCHAR(1)
		, TIPO NVARCHAR(30)
	)

	INSERT INTO @CONFIGURACION_CLAVES 
	SELECT 'LABORAL1-REP-COTIDIANO-D' AS CL_RESULTADO, 'D'  AS NB_LETRA, 'COTIDIANO' AS TIPO_MENSAJE UNION ALL
	SELECT 'LABORAL1-REP-COTIDIANO-I' AS CL_RESULTADO, 'I' AS NB_LETRA, 'COTIDIANO' AS TIPO_MENSAJE UNION ALL
	SELECT 'LABORAL1-REP-COTIDIANO-S' AS CL_RESULTADO, 'S' AS NB_LETRA, 'COTIDIANO' AS TIPO_MENSAJE UNION ALL
	SELECT 'LABORAL1-REP-COTIDIANO-C' AS CL_RESULTADO, 'C' AS NB_LETRA, 'COTIDIANO' AS TIPO_MENSAJE UNION ALL
	SELECT 'LABORAL1-REP-MOTIVANTE-D' AS CL_RESULTADO, 'D' AS NB_LETRA, 'MOTIVANTE' AS TIPO_MENSAJE UNION ALL
	SELECT 'LABORAL1-REP-MOTIVANTE-I' AS CL_RESULTADO, 'I' AS NB_LETRA, 'MOTIVANTE' AS TIPO_MENSAJE UNION ALL
	SELECT 'LABORAL1-REP-MOTIVANTE-S' AS CL_RESULTADO, 'S' AS NB_LETRA, 'MOTIVANTE' AS TIPO_MENSAJE UNION ALL
	SELECT 'LABORAL1-REP-MOTIVANTE-C' AS CL_RESULTADO, 'C' AS NB_LETRA, 'MOTIVANTE' AS TIPO_MENSAJE UNION ALL
	SELECT 'LABORAL1-REP-PRESION-D' AS CL_RESULTADO, 'D' AS NB_LETRA, 'PRESION' AS TIPO_MENSAJE UNION ALL
	SELECT 'LABORAL1-REP-PRESION-I' AS CL_RESULTADO, 'I' AS NB_LETRA, 'PRESION' AS TIPO_MENSAJE UNION ALL
	SELECT 'LABORAL1-REP-PRESION-S' AS CL_RESULTADO, 'S' AS NB_LETRA, 'PRESION' AS TIPO_MENSAJE UNION ALL
	SELECT 'LABORAL1-REP-PRESION-C' AS CL_RESULTADO, 'C' AS NB_LETRA, 'PRESION' AS TIPO_MENSAJE 
	

	;WITH  T_MENS_COTIDIANO AS
							(
					SELECT 'DI' AS CL_MENSAJE, 'DI - Creatividad' AS NB_TITULO, '<p style="text-align:justify;">Tiende a ser lógico, crítico e incisivo  en sus enfoques hacia la obtención de metas. Se sentirá retado por problemas que requieren esfuerzos de análisis y originalidad. Será llano y critico con la gente</p>' AS DS_MENSAJE,'C' AS CL_TIPO_MENSAJE,'MENSAJES' AS SECCION UNION ALL
					SELECT 'DS' AS CL_MENSAJE, 'DS - Urgencia-alcance' AS NB_TITULO, '<p style="text-align:justify;">Responde rápidamente a los retos, demuestra movilidad y flexibilidad en sus enfoques, tiende a ser iniciador versátil, respondiendo rápidamente a la competencia.</p>' AS DS_MENSAJE,'C','MENSAJES' AS SECCION UNION ALL
					SELECT 'DC' AS CL_MENSAJE, 'DC - Individualidad' AS NB_TITULO, '<p style="text-align:justify;">Actúa de manera directa y positiva ante la oposición. Es una persona fuerte que toma posición y lucha por mantenerla. Esta dispuesto a tomar riesgos y puede aún ignorar niveles jerárquicos.</p>' AS DS_MENSAJE,'C' AS CL_TIPO_MENSAJE ,'MENSAJES' AS SECCION UNION ALL
					SELECT 'ID' AS CL_MENSAJE, 'ID - Buena voluntad' AS NB_TITULO, '<p style="text-align:justify;">Tiende a comportarse en una forma equilibrada y cordial, desplegando "agresividad social" en situaciones que percibe como favorables y sin amenazas. Tiende a mostrarse simpático y lucha por establecer relaciones armoniosas con la gente desde el primer contacto.</p>' AS DS_MENSAJE,'C' AS CL_TIPO_MENSAJE,'MENSAJES' AS SECCION UNION ALL
					SELECT 'IS' AS CL_MENSAJE, 'IS - Habilidad de contactos' AS NB_TITULO, '<p style="text-align:justify;">Tiende a buscar a la gente con entusiasmo y chispa, es una persona abierta que despliega un optimismo contagioso y trata de ganarse a la gente a través de la persuasión de un acercamiento emotivo.</p>' AS DS_MENSAJE,'C' AS CL_TIPO_MENSAJE,'MENSAJES' AS SECCION UNION ALL
					SELECT 'IC' AS CL_MENSAJE, 'IC - Confianza en si mismo' AS NB_TITULO, '<p style="text-align:justify;">Despliega confianza en sí mismo en la mayoría de sus tratos con otras personas. Aunque siempre lucha por ganarse a la gente, se muestra reacio a ceder su punto de vista. Esta persona siente que no importa la situación presente, él será capaz de actuar de forma exitosa.</p>' AS DS_MENSAJE,'C' AS CL_TIPO_MENSAJE,'MENSAJES' AS SECCION UNION ALL
					SELECT 'SD' AS CL_MENSAJE, 'SD - Paciencia' AS NB_TITULO, '<p style="text-align:justify;">Tiende a ser constante y consistente, prefiriendo tratar un proyecto o tarea a la vez. En general, esta persona dirigirá sus habilidades y experiencias, hacia áreas que requieren profundización y especialización. Ecuánime bajo presión, busca estabilizar su ambiente y reacciona negativamente a los cambios en el mismo.</p>' AS DS_MENSAJE,'C' AS CL_TIPO_MENSAJE,'MENSAJES' AS SECCION UNION ALL
					SELECT 'SI' AS CL_MENSAJE, 'SI - Reflexión (concentración)' AS NB_TITULO, '<p style="text-align:justify;">Tiende a ser un individuo controlado y paciente. Se mueve con moderación y premeditación en la mayoría de las situaciones, con cuidado y concentración.</p>' AS DS_MENSAJE,'C' AS CL_TIPO_MENSAJE ,'MENSAJES' AS SECCION UNION ALL
					SELECT 'SC' AS CL_MENSAJE, 'SC - Persistencia' AS NB_TITULO, '<p style="text-align:justify;">Tiende a ser un individuo persistente y perseverante, que una vez que decide algo, no fácilmente se desvía de su objetivo. Tendera a tomar un ritmo de trabajo y a apegarse a él. Puede ser rígido e independiente cuando se aplica la fuerza para hacerle cambiar; exasperando a otros que requieran de su adaptación.</p>' AS DS_MENSAJE,'C' AS CL_TIPO_MENSAJE,'MENSAJES' AS SECCION UNION ALL
					SELECT 'CD' AS CL_MENSAJE, 'CD - Adaptabilidad' AS NB_TITULO, '<p style="text-align:justify;">Tiende a actuar de una forma cuidadosa y conservadora en general. Está dispuesto a modificar o transigir en su posición con el objeto de lograr sus objetivos. Siendo un estricto observador de las políticas, puede aparecer arbitrario y poco flexible al seguir una regla o formula establecida. Prefiere una atmósfera libre de antagonismos y desea la armonía.</p>' AS DS_MENSAJE,'C' AS CL_TIPO_MENSAJE ,'MENSAJES' AS SECCION UNION ALL
					SELECT 'CI' AS CL_MENSAJE, 'CI - Perfeccionismo' AS NB_TITULO, '<p style="text-align:justify;">Esta persona tiende a ser un seguidor apegado del orden y los sistemas. Toma decisiones basadas en hechos conocidos o procedimientos establecidos. En todas sus actividades, trata meticulosamente de apegarse a los estándares establecidos, ya sea por sí mismo o por los demás.</p>' AS DS_MENSAJE,'C' AS CL_TIPO_MENSAJE ,'MENSAJES' AS SECCION UNION ALL
					SELECT 'CS' AS CL_MENSAJE, 'CS - Sensibilidad' AS NB_TITULO, '<p style="text-align:justify;">Esta persona estará muy consciente en evitar riesgos o problemas. Tiende a buscar significados ocultos. La tensión puede ser evidente particularmente si esta bajo presión por obtener resultados. En general se sentirá intranquilo mientras no tenga una confirmación absoluta, de que su decisión ha sido la correcta.</p>' AS DS_MENSAJE,'C' AS CL_TIPO_MENSAJE ,'MENSAJES' AS SECCION UNION ALL
				
				
				    SELECT 'DH' AS CL_MENSAJE, 'Claves para la motivación de personas con alto grado de empuje' AS NB_TITULO ,
	'<p>La persona con alto grado de empuje desea:</p>
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
            </ol>'AS DS_MENSAJE ,'MOTIVANTE' AS CL_TIPO_MENSAJE  ,'LISTASA' AS SECCION UNION ALL
		SELECT 'DL' AS CL_MENSAJE, 'Claves para la motivación de personas con bajo grado de empuje' AS NB_TITULO, '
             <p>La persona con bajo grado de empuje desea:</p>
            <ol>
                <li>Paz.</li>
                <li>Protección.</li>
                <li>Dirección.</li>
                <li>Ambiente predecible.</li>
                <li>Un líder a quien seguir.</li>
                <li>Un plan que comprenda.</li>
                <li>Métodos.</li>
                <li>Verse libre de conflictos.</li>
                <li>Tiempo para pensar.</li>
                <li>Un futuro seguro.</li>
            </ol>
            <p>Necesita:</p>
            <ol>
                <li>Tareas claras.</li>
                <li>Sanciones por parte de su jefe o del manual.</li>
                <li>Ayuda en tareas nuevas o difíciles.</li>
                <li>Una forma de decir que "no".</li>
                <li>Métodos alternativos.</li>
                <li>Apoyo en situaciones difíciles.</li>
                <li>Técnicas y herramientas para manejar conflictos (armas).</li>
                <li>Un clima participativo (grupos o comités).</li>
                <li>Reconocimiento por el precio pagado por "desempeñarse".</li>
                <li>Métodos para traducir ideas en acciones.</li>
                <li>"Sacudidas ocasionales".</li>
            </ol>' AS DS_MENSAJE,'MOTIVANTE' AS CL_TIPO_MENSAJE ,'LISTASB' AS SECCION UNION ALL

				SELECT 'IH' AS CL_MENSAJE, 'Claves para la motivación de personas con alta influencia' AS NB_TITULO, '
              <p>La persona con alta influencia desea:</p>
                <ol>
                    <li>Popularidad, reconocimiento social</li>
                    <li>Recompensas monetarias para mantener su ritmo de vida</li>
                    <li>Reconocimiento público que indique su habilidad</li>
                    <li>Libertad de palabra, personas con quienes hablar</li>
                    <li>Condiciones favorables de trabajo</li>
                    <li>Actividades con gente fuera del trabajo</li>
                    <li>Relaciones democráticas</li>
                    <li>Libertad de control y detalles</li>
                    <li>Ingreso psicológico</li>
                    <li>Identificación con la compañía</li>
                </ol>
            <p>Necesita:</p>
                <ol>
                    <li>Control de su tiempo</li>
                    <li>Objetividad</li>
                    <li>Énfasis en la utilidad de su empresa</li>
                    <li>Ser menos idealista</li>
                    <li>Un supervisor democrático con quien pueda asociarse</li>
                    <li>Presentarlo como gente influyente</li>
                    <li>Control emocional</li>
                    <li>Sentido de urgencia</li>
                    <li>Control de su desempeño por proyectos</li>
                    <li>Confianza en el producto</li>
                    <li>Datos analizados</li>
                    <li>Administración financiera personal</li>
                    <li>Supervisión más estricta</li>
                    <li>Presentación precisa</li>
            </ol>' AS DS_MENSAJE,'MOTIVANTE' AS CL_TIPO_MENSAJE ,'LISTASA' AS SECCION UNION ALL


			SELECT 'IL' AS CL_MENSAJE, 'Claves para la motivación de personas con baja influencia' AS NB_TITULO, '
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
                </ol>' AS DS_MENSAJE,'MOTIVANTE' AS CL_TIPO_MENSAJE,'LISTASB' AS SECCION UNION ALL

				SELECT 'SH' AS CL_MENSAJE, 'Claves para la motivación de personas con alto grado de constancia' AS NB_TITULO, '
                 <p>La persona con  alto grado de constancia desea:</p>
                <ol>
                <li> Estatus</li>
                <li> Situación segura</li>
                <li> Referencias</li>
                <li> Vida hogareña</li>
                <li> Procedimientos usuales</li>
                <li> Sinceridad</li>
                <li> Territorio limitado</li>
                <li> Largo tiempo para ajustarse</li>
                <li> Apreciación constante</li>
                <li> Identificación constante</li>
                <li> Reconocimiento por servicios continuos</li>
                <li> Productos especiales</li>
                </ol>
            <p>Necesita:</p>
                <ol>
                <li> Condicionamiento anterior al cambio</li>
                <li> Recompensas en términos de cosas</li>
                <li> Beneficios adicionales</li>
                <li> Presentación a nuevos grupos</li>
                <li> Esposa (o) que los respalde</li>
                <li> Métodos que ahorren trabajo</li>
                <li> Enfoques profundos</li>
                <li> Presentaciones comprimidas</li>
                <li> Reafirmación</li>
                <li> Sentimiento de importancia</li>
                <li> Productos de calidad que lo satisfagan</li>
                <li> Mercancías especiales</li>
                <li> Asociados capaces</li>
                </ol>' AS DS_MENSAJE,'MOTIVANTE'  AS CL_TIPO_MENSAJE ,'LISTASA' AS SECCION UNION ALL

		     SELECT 'SL' AS CL_MENSAJE, 'Claves para la motivación de personas con bajo grado de constancia' AS NB_TITULO, '
                  <p>La persona con bajo grado de constancia desea:</p>
                <ol>
                <li> Variedad</li>
                <li> Viajes</li>
                <li> Trabajo de generalista</li>
                <li> Nuevos entornos en los cuales trabajar/jugar</li>
                <li> Una amplia gama</li>
                <li> Libertad de rutina</li>
                <li> Enfoques de gran imagen</li>
                <li> Tropas de apoyo que terminen la labor</li>
                <li> Más tiempo en el día</li>
                <li> Actividades extracurriculares</li>
                </ol>
            <p>Necesita:</p>
                <ol>
                <li> Aprender a establecer un paso razonable</li>
                <li> Vacaciones</li>
                <li> Exámenes médicos anuales</li>
                <li> Apreciación de la gente que se mueve más lentamente que ellos</li>
                <li> Respeto a las prerrogativas y propiedades personales</li>
                <li> Fechas definidas para terminación</li>
                <li> Presupuestos</li>
                <li> Consistencia</li>
                <li> Reanudar lo andado, recapitular</li>
                <li> Sistemas</li>
                </ol>' AS DS_MENSAJE,'MOTIVANTE'  AS CL_TIPO_MENSAJE ,'LISTASB' AS SECCION UNION ALL

				 SELECT 'CH' AS CL_MENSAJE, 'Claves para la motivación de personas con alto grado de cumplimiento' AS NB_TITULO, '
                     <p>La persona con alto grado de cumplimiento desea:</p>
                <ol>
                <li> Procedimientos estandarizados de operación</li>
                <li> Límite en el grado de exposición a otros ambientes</li>
                <li> Seguridad (protección), ambiente protegido</li>
                <li> Referencias</li>
                <li> Reafirmación</li>
                <li> Cambios poco rápidos o abruptos</li>
                <li> Ser parte de un grupo</li>
                <li> Atención personal</li>
                <li> Poca responsabilidad</li>
                <li> Personas a su servicio</li>
                </ol>
                <p>Necesita:</p>
                <ol>
                <li> Trabajo de precisión</li>
                <li> Planeación</li>
                <li> Más confianza</li>
                <li> Más ángulos y mayor perspectiva en sus enfoques</li>
                <li> Argumentos que refuten</li>
                <li> Soporte en las situaciones difíciles</li>
                <li> Explicaciones y más explicaciones</li>
                <li> Participación de equipo</li>
                <li> Recompensas en términos de cosas finas</li>
                <li> Descripción exacta del trabajo</li>
                <li> Presentación de personas</li>
                <li> Ayuda para ser más independiente</li>
                <li> Menos atención a detalles</li>
                <li> Respeto a si mismo</li>
                </ol>' AS DS_MENSAJE,'MOTIVANTE'  AS CL_TIPO_MENSAJE ,'LISTASA' AS SECCION UNION ALL

				 SELECT 'CL' AS CL_MENSAJE, 'Claves para la motivación de personas con bajo grado de cumplimiento' AS NB_TITULO, '
                <p>La persona con bajo grado de cumplimiento desea:</p>
                <ol>
                <li> Libertad</li>
                <li> Tareas excepcionales</li>
                <li> Independencia</li>
                <li> Cero restricciones</li>
                <li> Ser evaluado por resultados</li>
                <li> Cero supervisión</li>
                <li> La oportunidad para divertirse</li>
                <li> Experiencias</li>
                <li> Ventilación</li>
                <li> Emociones fuertes</li>
                </ol>
                <p>Necesita:</p>
                <ol>
                <li> Un jefe tolerante</li>
                <li> Pólizas de seguro de vida, de enfermedades y de accidentes</li>
                <li> Reconocer que existen límites (y por qué)</li>
                <li> Ser evaluado por resultados</li>
                <li> Oportunidad para probar lo nunca antes intentado</li>
                <li> Ayuda con los detalles</li>
                <li> Documentaciones</li>
                <li> Proyectos independientes</li>
                <li> Autoridad</li>
                <li> Restricciones</li>
                </ol>'AS DS_MENSAJE ,'MOTIVANTE'  AS CL_TIPO_MENSAJE ,'LISTASB' AS SECCION UNION ALL


		SELECT 'DH' AS CL_MENSAJE, 'Bajo presión una persona con un alto grado de empuje tiende a' AS NB_TITULO,
				'<ol>
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
            </ol>'AS DS_MENSAJE ,'PRESION'  AS CL_TIPO_MENSAJE ,'LISTASA' AS SECCION UNION ALL

					
		SELECT 'DL' AS CL_MENSAJE, 'Bajo presión una persona con un bajo grado de empuje tiende a' AS NB_TITULO,
				'<ol>
            <li> Renunciar a su posición para evitar controversias</li>
            <li> Dudar como actuar en problemas</li>
            <li> Autodespreciarse</li>
            <li> Evitar responsabilidad</li>
            <li> Retraerse cuando se le confronta</li>
            <li> Ser defensivo</li>
            <li> Permite que se aprovechen de él indebidamente</li>
            <li> Ser dependiente</li>
            <li> Ser demasiado conservador</li>
            <li> Ser evasivo</li>
            </ol>'AS DS_MENSAJE ,'PRESION'  AS CL_TIPO_MENSAJE ,'LISTASB' AS SECCION UNION ALL

				SELECT 'IH' AS CL_MENSAJE, 'Bajo presión una persona con un alto grado de influencia tiende a' AS NB_TITULO,
				'<ol>
            <li> Preocuparse más de su popularidad que de los resultados tangibles</li>
            <li> Ser exageradamente persuasivo</li>
            <li> Actuar impulsivamente, siguiendo su corazón en lugar de su inteligencia</li>
            <li> Ser inconsistente en sus conclusiones</li>
            <li> Tomar decisiones basado en análisis superficiales</li>
            <li> Ser poco realista al evaluar a las personas</li>
            <li> Ser descuidado con los detalles</li>
            <li> Confiar en las personas indiscriminadamente</li>
            <li> Tener dificultad para planear y controlar su tiempo</li>
            <li> Ser superficial</li>
            </ol>'AS DS_MENSAJE ,'PRESION'  AS CL_TIPO_MENSAJE ,'LISTASA' AS SECCION UNION ALL

				SELECT 'IL' AS CL_MENSAJE, 'Bajo presión una persona con un bajo grado de influencia tiende a' AS NB_TITULO,
				'<ol>
            <li> Ser distante</li>
            <li> Ser cortante</li>
            <li> Ser crítico</li>
            <li> Ser suspicaz</li>
            <li> Carecer de empatía</li>
            <li> Lastimar los sentimientos de los demás</li>
            <li> Ser retraído</li>
            <li> Ser ecuánime, frío</li>
            <li> Preferir los objetos a la gente</li>
            <li> Carecer de confianza social</li>
            </ol>'AS DS_MENSAJE ,'PRESION'  AS CL_TIPO_MENSAJE ,'LISTASB' AS SECCION UNION ALL

			SELECT 'SH' AS CL_MENSAJE, 'Bajo presión una persona con un alto grado de constancia tiende a' AS NB_TITULO,
				'<ol>
            <li>  Hacer un esfuerzo para mantener el "statuo quo"</li>
            <li> Requerir mucho tiempo para ajustarse al cambio</li>
            <li> Tener dificultades para cumplir con compromisos</li>
            <li> Necesitar ayuda para cumplir con compromisos</li>
            <li> Carecer de imaginación</li>
            <li> Sentirse contento y cómodo con las cosas tal y como son</li>
            <li> Continuar haciendo las cosas en la forma en que se han hecho</li>
            <li> Conservar resentimientos</li>
            <li> Esperar órdenes antes de actuar</li>
            <li> Tener dificultad para establecer prioridades</li>
            </ol>' AS DS_MENSAJE ,'PRESION'  AS CL_TIPO_MENSAJE ,'LISTASA' AS SECCION UNION ALL

			SELECT 'SL' AS CL_MENSAJE, 'Bajo presión una persona con un bajo grado de constancia tiende a' AS NB_TITULO,
			'<ol>
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
            </ol>' AS DS_MENSAJE ,'PRESION'  AS CL_TIPO_MENSAJE ,'LISTASB' AS SECCION UNION ALL

			SELECT 'CH' AS CL_MENSAJE, 'Bajo presión una persona con un alto grado de cumplimiento tiende a' AS NB_TITULO,
			'<ol>
            <li> Depender de la supervisión</li>
            <li> Dudar antes de actuar sin precedente</li>
            <li> Estar atado a procedimientos y métodos</li>
            <li> Dejarse abrumar por los detalles</li>
            <li> Resistirse a aceptar responsabilidad plena</li>
            <li> Desear explicaciones completas antes de hacer un cambio</li>
            <li> Pasar la responsabilidad a otra persona</li>
            <li> Renunciar a su posición para evitar controversias</li>
            <li> Ponerse a la defensiva al verse amenazado</li>
            <li> Ser sugestionable y fácilmente dirigido</li>
            </ol>' AS DS_MENSAJE ,'PRESION' AS CL_TIPO_MENSAJE ,'LISTASA' AS SECCION UNION ALL

			SELECT 'CL' AS CL_MENSAJE, 'Bajo presión una persona con un bajo grado de cumplimiento tiende a' AS NB_TITULO,
			'<ol>
            <li> Ser poco convencional</li>
            <li> Ignorar las instrucciones y direcciones</li>
            <li> Ser descuidado con los detalles</li>
            <li> Desafiar al peligro</li>
            <li> No documentarse</li>
            <li> Ser propenso a los accidentes</li>
            <li> Ignorar la política</li>
            <li> Provocar ulceras (en los demás)</li>
            <li> Ser temerario</li>
            <li> Ser distraído</li>
            </ol>' AS DS_MENSAJE,'PRESION' AS CL_TIPO_MENSAJE ,'LISTASB' AS SECCION UNION ALL
			SELECT 'DH' AS CL_MENSAJE,'Empuje' AS NB_TITULO, '<p style="text-align:justify;">Para comprender y motivar a la gente, es importante que conozcamos su comportamiento, deseos y lo que necesitan para triunfar, una vez que hemos estudiado a la persona y hemos determinado que su estilo básico de comportamiento es predominantemente alto grado de empuje, entonces los factores abajo anotados, pueden ser claves para el éxito de su motivación, utilizar las listas en forma selectiva y basándonos en los hechos de cada caso especifico.</p>' ,'MOTIVANTE' AS CL_TIPO_MENSAJE, 'HEADA' AS SECCION UNION ALL
			SELECT 'IH' AS CL_MENSAJE,'Influencia' AS NB_TITULO, '<p style="text-align:justify;">Para comprender y motivar a la gente, es importante que conozcamos su comportamiento, deseos y lo que necesitan para triunfar, una vez que hemos estudiado a la persona y hemos determinado que su estilo básico de comportamiento es predominantemente alto grado de influencia, entonces los factores abajo anotados, pueden ser claves para el éxito de su motivación, utilizar las listas en forma selectiva y basándonos en los hechos de cada caso especifico.</p>' ,'MOTIVANTE' AS CL_TIPO_MENSAJE, 'HEADA' AS SECCION  UNION ALL
			SELECT 'SH' AS CL_MENSAJE,'Constancia' AS NB_TITULO, '<p style="text-align:justify;">Para comprender y motivar a la gente, es importante que conozcamos su comportamiento, deseos y lo que necesitan para triunfar, una vez que hemos estudiado a la persona y hemos determinado que su estilo básico de comportamiento es predominantemente alto grado de constancia, entonces los factores abajo anotados, pueden ser claves para el éxito de su motivación, utilizar las listas en forma selectiva y basándonos en los hechos de cada caso especifico.</p>' ,'MOTIVANTE' AS CL_TIPO_MENSAJE, 'HEADA' AS SECCION  UNION ALL
			SELECT 'CH' AS CL_MENSAJE,'Cumplimiento' AS NB_TITULO, '<p style="text-align:justify;">Para comprender y motivar a la gente, es importante que conozcamos su comportamiento, deseos y lo que necesitan para triunfar, una vez que hemos estudiado a la persona y hemos determinado que su estilo básico de comportamiento es predominantemente alto grado de cumplimiento, entonces los factores abajo anotados, pueden ser claves para el éxito de su motivación, utilizar las listas en forma selectiva y basándonos en los hechos de cada caso especifico.</p>' ,'MOTIVANTE' AS CL_TIPO_MENSAJE, 'HEADA' AS SECCION  UNION ALL
            SELECT 'DL' AS CL_MENSAJE,'Empuje' AS NB_TITULO, '<p style="text-align:justify;">Para comprender y motivar a la gente, es importante que conozcamos su comportamiento, deseos y lo que necesitan para triunfar, una vez que hemos estudiado a la persona y hemos determinado que su estilo básico de comportamiento es predominantemente bajo grado de empuje, entonces los factores abajo anotados, pueden ser claves para el éxito de su motivación, utilizar las listas en forma selectiva y basándonos en los hechos de cada caso especifico.</p>' ,'MOTIVANTE' AS CL_TIPO_MENSAJE, 'HEADB' AS SECCION  UNION ALL
			SELECT 'IL' AS CL_MENSAJE,'Influencia' AS NB_TITULO,'<p style="text-align:justify;">Para comprender y motivar a la gente, es importante que conozcamos su comportamiento, deseos y lo que necesitan para triunfar, una vez que hemos estudiado a la persona y hemos determinado que su estilo básico de comportamiento es predominantemente bajo grado de influencia, entonces los factores abajo anotados, pueden ser claves para el éxito de su motivación, utilizar las listas en forma selectiva y basándonos en los hechos de cada caso especifico.</p>' ,'MOTIVANTE' AS CL_TIPO_MENSAJE, 'HEADB' AS SECCION  UNION ALL
			SELECT 'SL' AS CL_MENSAJE,'Constancia' AS NB_TITULO, '<p style="text-align:justify;">Para comprender y motivar a la gente, es importante que conozcamos su comportamiento, deseos y lo que necesitan para triunfar, una vez que hemos estudiado a la persona y hemos determinado que su estilo básico de comportamiento es predominantemente bajo grado de constancia, entonces los factores abajo anotados, pueden ser claves para el éxito de su motivación, utilizar las listas en forma selectiva y basándonos en los hechos de cada caso especifico.</p>' ,'MOTIVANTE' AS CL_TIPO_MENSAJE, 'HEADB' AS SECCION  UNION ALL
			SELECT 'CL' AS CL_MENSAJE,'Cumplimiento' AS NB_TITULO, '<p style="text-align:justify;">Para comprender y motivar a la gente, es importante que conozcamos su comportamiento, deseos y lo que necesitan para triunfar, una vez que hemos estudiado a la persona y hemos determinado que su estilo básico de comportamiento es predominantemente bajo grado de cumplimiento, entonces los factores abajo anotados, pueden ser claves para el éxito de su motivación, utilizar las listas en forma selectiva y basándonos en los hechos de cada caso especifico.</p>' ,'MOTIVANTE' AS CL_TIPO_MENSAJE, 'HEADB' AS SECCION 
							)

			INSERT INTO @MENSAJES
			SELECT  * FROM T_MENS_COTIDIANO

			 UPDATE TR SET -- ACTUALIZAMOS LA TABLA @RESULTADOS CON  LA DE @CONFIGURACION DE CLAVES PARA INDICARLE QUIEN ES D, I, S, C
			 CL_LETRA =CC.CL_LETRA,
			 TIPO = CC.TIPO
			 FROM @RESULTADOS TR
			 INNER JOIN
			 (SELECT * FROM @CONFIGURACION_CLAVES) CC
					  ON  CC.CL_VARIABLE= TR.CL_VARIABLE

		  ;WITH T AS (
	SELECT NO_VALOR,CL_VARIABLE,CL_LETRA, TIPO
		, ROW_NUMBER() OVER (PARTITION BY TIPO ORDER BY TIPO, NO_VALOR DESC) NO_ORDEN_DESC
		, ROW_NUMBER() OVER (PARTITION BY TIPO ORDER BY TIPO, NO_VALOR ) NO_ORDEN_ASC
	FROM @RESULTADOS
)
    UPDATE R
	SET NO_ORDEN_DESC =  T.NO_ORDEN_DESC,
	    NO_ORDEN_ASC =  T.NO_ORDEN_ASC
	FROM @RESULTADOS R
		INNER JOIN T ON T.CL_LETRA = R.CL_LETRA AND T.TIPO = R.TIPO

	SET @XML_MENSAJES =  (
	SELECT  
	MS.CL_MENSAJE AS "@CL_MENSAJE",
	MS.NB_TITULO AS  "@NB_TITULO",
	MS.DS_MENSAJE AS "@DS_MENSAJE",
	MS.TIPO AS "@TIPO_MENSAJE",
	MS.SECCION AS "@SECCION"
	FROM (				 ---COTIDIANO
		SELECT TC.CL_MENSAJE,M.NB_TITULO,DS_MENSAJE,TC.TIPO, M.SECCION FROM (
		  SELECT Mayor.TIPO, CONCAT(Mayor.CL_LETRA,Menor.CL_LETRA)AS CL_MENSAJE  FROM @RESULTADOS Mayor
		          INNER JOIN @RESULTADOS Menor
				   ON Mayor.NO_ORDEN_DESC  < Menor.NO_ORDEN_DESC			
				   where Mayor.TIPO = Menor.TIPO
				   AND Mayor.TIPO = 'COTIDIANO'
		) AS TC
		  INNER JOIN @MENSAJES M ON M.CL_MENSAJE = TC.CL_MENSAJE  
		  UNION ALL
		  ---MOTIVANTE Y PRESION
		  SELECT TMP.CL_MENSAJE ,M.NB_TITULO ,DS_MENSAJE ,TMP.TIPO , M.SECCION FROM 
 		(SELECT Mayor.TIPO,CONCAT(Mayor.CL_LETRA, 'H') AS MAYOR_LETRA,CONCAT(Menor.CL_LETRA,'L') AS MENOR_LETRA, CONCAT(Mayor.CL_LETRA,Menor.CL_LETRA)AS CL_MENSAJE FROM @RESULTADOS Mayor
		          INNER JOIN @RESULTADOS Menor
				   ON Mayor.NO_ORDEN_DESC  = Menor.NO_ORDEN_ASC			
				   where Mayor.TIPO = Menor.TIPO
				   AND (Mayor.TIPO = 'MOTIVANTE' OR Mayor.TIPO = 'PRESION') AND Mayor.NO_ORDEN_DESC = 1
		) TMP
		INNER JOIN @MENSAJES M ON( M.CL_MENSAJE = TMP.MAYOR_LETRA OR  M.CL_MENSAJE = TMP.MENOR_LETRA)AND  TMP.TIPO = M.TIPO_MENSAJE) MS
    FOR XML PATH ('MENSAJE')
	)
 
END
   	SET @CLAVE_PRUEBAS =(
	SELECT R.CL_PRUEBA,ROW_NUMBER() OVER(ORDER BY R.CL_PRUEBA DESC) AS Row  FROM
	(SELECT DISTINCT
	CL_PRUEBA
	FROM @RESULTADOS) R
	FOR XML RAW ('PRUEBA'), ROOT('PRUEBAS')
	)

	DECLARE @TOTAL_PRUEBAS INT = 0
	SELECT @TOTAL_PRUEBAS = COUNT(R.CL_PRUEBA) FROM (SELECT DISTINCT CL_PRUEBA  FROM @RESULTADOS)R
	DECLARE  @VALORES XML =NULL
	DECLARE @PRUEBA NVARCHAR(20) = ''

	DECLARE @i INT = 1
	WHILE @i <= @TOTAL_PRUEBAS
    BEGIN
				    SELECT
				    @PRUEBA = COLUMNA.value('@CL_PRUEBA', 'NVARCHAR(20)')
					FROM @CLAVE_PRUEBAS.nodes('/PRUEBAS/PRUEBA') tbl(COLUMNA)
					WHERE  COLUMNA.value('@Row', 'INT') =  @i

	   SET @RESULT =(
		SELECT 
		 R.CL_PRUEBA
		,R.NO_VALOR
		,R.CL_VARIABLE
	FROM @RESULTADOS R 
	WHERE R.CL_PRUEBA = @PRUEBA
	FOR XML RAW ('VALORES')
	)

	IF @PRUEBA <> ''
	BEGIN
    SET @CLAVE_PRUEBAS.modify('insert sql:variable("@RESULT") into (/PRUEBAS/PRUEBA[@CL_PRUEBA = sql:variable("@PRUEBA")])[1]');
	SET @CLAVE_PRUEBAS.modify('insert <MENSAJES /> into (/PRUEBAS/PRUEBA[@CL_PRUEBA = sql:variable("@PRUEBA")])[1]');

		IF @PRUEBA = 'LABORAL-1'
		BEGIN
			SET @CLAVE_PRUEBAS.modify('insert sql:variable("@XML_MENSAJES")  into (/PRUEBAS/PRUEBA[@CL_PRUEBA = sql:variable("@PRUEBA")]/MENSAJES)[1]');
		END
	END

   SET @i+=1
   END

   SET @XML_RESULTADO = @CLAVE_PRUEBAS
   --SELECT @XML_RESULTADO
	    --SE DEVUELVE LOS REGISTROS EN BASE A LOS PARAMETROS INSERTADOS
		--SELECT 
		-- R.ID_BATERIA
		--,R.CL_PRUEBA
		--,R.NO_VALOR
		--,R.CL_VARIABLE,
		--@XML_RESULTADO AS XML_RESULTADOS
	--FROM @RESULTADOS R 

--	EXECUTE [SIGEIN].IDP.[SPE_OBTIENE_RESULTADOS_BATERIA] OUT ,73, null
--	EXECUTE [SIGEIN].IDP.[SPE_OBTIENE_RESULTADOS_BATERIA] OUT ,127, null
--	EXECUTE [SIGEIN].IDP.[SPE_OBTIENE_RESULTADOS_BATERIA] OUT ,77, '365435DF-C92F-4A63-B06C-B2B2F9376A14'
