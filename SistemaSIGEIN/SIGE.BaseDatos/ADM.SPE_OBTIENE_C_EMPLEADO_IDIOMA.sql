-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margaritra Salcedo
-- CREATE date: 11/11/2015
-- Description: Obtiene los datos de la tabla C_EMPLEADO_IDIOMA 
-- =============================================
CREATE PROCEDURE [ADM].[SPE_OBTIENE_C_EMPLEADO_IDIOMA] 
	@PIN_ID_EMPLEADO_IDIOMA AS int = NULL
	, @PIN_ID_EMPLEADO AS int = NULL
	, @PIN_ID_CANDIDATO AS int = NULL
	, @PIN_ID_IDIOMA AS int = NULL
	, @PIN_PR_LECTURA AS decimal(5,2) = NULL
	, @PIN_PR_ESCRITURA AS decimal(5,2) = NULL
	, @PIN_PR_CONVERSACIONAL AS decimal(5,2) = NULL
	, @PIN_CL_INSTITUCION AS int = NULL
	, @PIN_NO_PUNTAJE AS decimal(10,3) = NULL
	
AS   
	--SE DEVUELVE LOS REGISTROS EN BASE A LOS PARAMETROS INSERTADOS
	SELECT 
		CE.[ID_EMPLEADO_IDIOMA]
		, CE.[ID_EMPLEADO]
		, CE.[ID_CANDIDATO]
		, CE.[ID_IDIOMA]
		, CE.[PR_LECTURA]
		, CE.[PR_ESCRITURA]
		, CE.[PR_CONVERSACIONAL]
		, CE.[CL_INSTITUCION]
		, CE.[NO_PUNTAJE]		
		,'' as DS_FILTRO
		, CI.NB_IDIOMA 
		, CC.CL_CATALOGO_VALOR 
	FROM ADM.C_EMPLEADO_IDIOMA CE
	JOIN ADM.C_IDIOMA CI ON CE.ID_IDIOMA = CI.CL_IDIOMA
	LEFT OUTER JOIN ADM.C_CATALOGO_VALOR CC ON CE.CL_INSTITUCION = CC.ID_CATALOGO_VALOR 
	WHERE (@PIN_ID_EMPLEADO_IDIOMA IS NULL OR (@PIN_ID_EMPLEADO_IDIOMA IS NOT NULL AND [ID_EMPLEADO_IDIOMA] = @PIN_ID_EMPLEADO_IDIOMA))
			 AND (@PIN_ID_EMPLEADO IS NULL OR (@PIN_ID_EMPLEADO IS NOT NULL AND [ID_EMPLEADO] = @PIN_ID_EMPLEADO))
			 AND (@PIN_ID_CANDIDATO IS NULL OR (@PIN_ID_CANDIDATO IS NOT NULL AND [ID_CANDIDATO] = @PIN_ID_CANDIDATO))
			 AND (@PIN_ID_IDIOMA IS NULL OR (@PIN_ID_IDIOMA IS NOT NULL AND CE.[ID_IDIOMA] = @PIN_ID_IDIOMA))
			 AND (@PIN_PR_LECTURA IS NULL OR (@PIN_PR_LECTURA IS NOT NULL AND [PR_LECTURA] = @PIN_PR_LECTURA))
			 AND (@PIN_PR_ESCRITURA IS NULL OR (@PIN_PR_ESCRITURA IS NOT NULL AND [PR_ESCRITURA] = @PIN_PR_ESCRITURA))
			 AND (@PIN_PR_CONVERSACIONAL IS NULL OR (@PIN_PR_CONVERSACIONAL IS NOT NULL AND [PR_CONVERSACIONAL] = @PIN_PR_CONVERSACIONAL))
			 AND (@PIN_CL_INSTITUCION IS NULL OR (@PIN_CL_INSTITUCION IS NOT NULL AND [CL_INSTITUCION] = @PIN_CL_INSTITUCION))
			 AND (@PIN_NO_PUNTAJE IS NULL OR (@PIN_NO_PUNTAJE IS NOT NULL AND [NO_PUNTAJE] = @PIN_NO_PUNTAJE))
			
