-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 21/09/2015
-- Description: Obtiene los datos de la tabla K_AREA_INTERES 
-- =============================================
ALTER PROCEDURE ADM.SPE_OBTIENE_K_AREA_INTERES 
	@PIN_ID_CANDIDATO_AREA_INTERES AS int = NULL
	, @PIN_ID_CANDIDATO AS int = NULL
	, @PIN_ID_AREA_INTERES AS int = NULL
	
AS   
	--SE DEVUELVE LOS REGISTROS EN BASE A LOS PARAMETROS INSERTADOS
	SELECT 
		[ID_CANDIDATO_AREA_INTERES]
		, [ID_CANDIDATO]
		, [ID_AREA_INTERES]
		,'' as DS_FILTRO
	FROM ADM.K_AREA_INTERES
	WHERE (@PIN_ID_CANDIDATO_AREA_INTERES IS NULL OR (@PIN_ID_CANDIDATO_AREA_INTERES IS NOT NULL AND [ID_CANDIDATO_AREA_INTERES] = @PIN_ID_CANDIDATO_AREA_INTERES))
			 AND (@PIN_ID_CANDIDATO IS NULL OR (@PIN_ID_CANDIDATO IS NOT NULL AND [ID_CANDIDATO] = @PIN_ID_CANDIDATO))
			 AND (@PIN_ID_AREA_INTERES IS NULL OR (@PIN_ID_AREA_INTERES IS NOT NULL AND [ID_AREA_INTERES] = @PIN_ID_AREA_INTERES))
			
GO