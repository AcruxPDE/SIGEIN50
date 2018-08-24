﻿-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 12/11/2015
-- Description: Obtiene los datos de la tabla VW_OBTIENE_PORCENTAJES_IDIOMAS 
-- =============================================
CREATE PROCEDURE IDP.SPE_OBTIENE_VW_OBTIENE_PORCENTAJES_IDIOMAS 
	@PIN_NB_PORCENTAJE AS varchar(14) = NULL
  , @PIN_CL_PORCENTAJE AS numeric(4,2) = NULL
	
AS   
	--SE DEVUELVE LOS REGISTROS EN BASE A LOS PARAMETROS INSERTADOS
	SELECT 
		[NB_PORCENTAJE]
		, [CL_PORCENTAJE]
		,'' as DS_FILTRO
	FROM IDP.VW_OBTIENE_PORCENTAJES_IDIOMAS
	WHERE (@PIN_NB_PORCENTAJE IS NULL OR (@PIN_NB_PORCENTAJE IS NOT NULL AND [NB_PORCENTAJE] = @PIN_NB_PORCENTAJE))
			 AND (@PIN_CL_PORCENTAJE IS NULL OR (@PIN_CL_PORCENTAJE IS NOT NULL AND [CL_PORCENTAJE] = @PIN_CL_PORCENTAJE))
			