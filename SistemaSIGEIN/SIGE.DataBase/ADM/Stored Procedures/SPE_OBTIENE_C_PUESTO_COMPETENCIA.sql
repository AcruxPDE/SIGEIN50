﻿-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 21/09/2015
-- Description: Obtiene los datos de la tabla C_PUESTO_COMPETENCIA 
-- =============================================
CREATE PROCEDURE ADM.SPE_OBTIENE_C_PUESTO_COMPETENCIA 
	@PIN_ID_PUESTO_COMPETENCIA AS int = NULL
	, @PIN_ID_PUESTO AS int = NULL
	, @PIN_ID_COMPETENCIA AS int = NULL
	, @PIN_ID_NIVEL_DESEADO AS decimal(5,2) = NULL
	
AS   
	--SE DEVUELVE LOS REGISTROS EN BASE A LOS PARAMETROS INSERTADOS
	SELECT 
		[ID_PUESTO_COMPETENCIA]
		, [ID_PUESTO]
		, [ID_COMPETENCIA]
		, [ID_NIVEL_DESEADO]
		,'' as DS_FILTRO
	FROM ADM.C_PUESTO_COMPETENCIA
	WHERE (@PIN_ID_PUESTO_COMPETENCIA IS NULL OR (@PIN_ID_PUESTO_COMPETENCIA IS NOT NULL AND [ID_PUESTO_COMPETENCIA] = @PIN_ID_PUESTO_COMPETENCIA))
			 AND (@PIN_ID_PUESTO IS NULL OR (@PIN_ID_PUESTO IS NOT NULL AND [ID_PUESTO] = @PIN_ID_PUESTO))
			 AND (@PIN_ID_COMPETENCIA IS NULL OR (@PIN_ID_COMPETENCIA IS NOT NULL AND [ID_COMPETENCIA] = @PIN_ID_COMPETENCIA))
			 AND (@PIN_ID_NIVEL_DESEADO IS NULL OR (@PIN_ID_NIVEL_DESEADO IS NOT NULL AND [ID_NIVEL_DESEADO] = @PIN_ID_NIVEL_DESEADO))
			