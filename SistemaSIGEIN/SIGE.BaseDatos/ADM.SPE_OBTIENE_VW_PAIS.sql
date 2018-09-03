-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 09/11/2015
-- Description: Obtiene los datos de la tabla VW_PAIS 
-- =============================================
ALTER PROCEDURE ADM.SPE_OBTIENE_VW_PAIS 
	@PIN_CL_PAIS AS varchar(6) = NULL
	, @PIN_NB_PAIS AS varchar(6) = NULL
	
AS   
	--SE DEVUELVE LOS REGISTROS EN BASE A LOS PARAMETROS INSERTADOS
	SELECT 
		[CL_PAIS]
		, [NB_PAIS]
	FROM ADM.VW_PAIS
	WHERE (@PIN_CL_PAIS IS NULL OR (@PIN_CL_PAIS IS NOT NULL AND [CL_PAIS] = @PIN_CL_PAIS))
			 AND (@PIN_NB_PAIS IS NULL OR (@PIN_NB_PAIS IS NOT NULL AND [NB_PAIS] = @PIN_NB_PAIS))
			
GO
