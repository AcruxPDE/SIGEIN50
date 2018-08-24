
-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Juan De Dios Pérez Pedroza
-- CREATE date: 21/09/2015
-- Description: Obtiene los datos de la tabla K_PRUEBA_NIVEL
-- =============================================
CREATE PROCEDURE [IDP].[SPE_OBTIENE_K_PRUEBA_NIVEL] 
	 @PIN_ID_PRUEBA_NIVEL AS int = NULL
	, @PIN_ID_PRUEBA AS int = NULL
AS   
	--SE DEVUELVE LOS REGISTROS EN BASE A LOS PARAMETROS INSERTADOS
	SELECT 
		  [ID_PRUEBA_NIVEL]
		, [ID_PRUEBA]
	
	FROM IDP.K_PRUEBA_NIVEL
	WHERE (@PIN_ID_PRUEBA_NIVEL IS NULL OR (@PIN_ID_PRUEBA_NIVEL IS NOT NULL AND [ID_PRUEBA_NIVEL] = @PIN_ID_PRUEBA_NIVEL))
	  AND (@PIN_ID_PRUEBA IS NULL OR (@PIN_ID_PRUEBA IS NOT NULL AND [ID_PRUEBA] = @PIN_ID_PRUEBA))

