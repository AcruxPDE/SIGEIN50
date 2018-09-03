-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 04/11/2015
-- Description: Obtiene los datos de la tabla VW_OBTIENE_MESES 
-- =============================================
CREATE PROCEDURE ADM.SPE_OBTIENE_VW_OBTIENE_MESES 
	@PIN_NUMERO AS int = NULL
  , @PIN_MES AS varchar(10) = NULL
	
AS   
	--SE DEVUELVE LOS REGISTROS EN BASE A LOS PARAMETROS INSERTADOS
	SELECT 
		[NUMERO]
	   ,[MES]
	FROM ADM.VW_OBTIENE_MESES
	WHERE (@PIN_NUMERO IS NULL OR (@PIN_NUMERO IS NOT NULL AND [NUMERO] = @PIN_NUMERO))
			 AND (@PIN_MES IS NULL OR (@PIN_MES IS NOT NULL AND [MES] = @PIN_MES))
			
