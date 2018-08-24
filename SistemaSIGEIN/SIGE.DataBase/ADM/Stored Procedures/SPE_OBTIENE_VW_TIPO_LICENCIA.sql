-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 08/11/2015
-- Description: Obtiene los datos de la tabla VW_TIPO_LICENCIA 
-- =============================================
CREATE PROCEDURE ADM.SPE_OBTIENE_VW_TIPO_LICENCIA 
	@PIN_CL_LICENCIA AS varchar(7) = NULL
	, @PIN_NB_LICENCIA AS varchar(18) = NULL
	
AS   
	--SE DEVUELVE LOS REGISTROS EN BASE A LOS PARAMETROS INSERTADOS
	SELECT 
		[CL_LICENCIA]
		, [NB_LICENCIA]
	FROM ADM.VW_TIPO_LICENCIA
	WHERE (@PIN_CL_LICENCIA IS NULL OR (@PIN_CL_LICENCIA IS NOT NULL AND [CL_LICENCIA] = @PIN_CL_LICENCIA))
			 AND (@PIN_NB_LICENCIA IS NULL OR (@PIN_NB_LICENCIA IS NOT NULL AND [NB_LICENCIA] = @PIN_NB_LICENCIA))
			
