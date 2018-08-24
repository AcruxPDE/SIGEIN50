﻿
-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Abraham Ramirez
-- CREATE date: 21/09/2015
-- Description: Obtiene los datos de la tabla C_GRUPO_PREGUNTA 
-- =============================================
CREATE PROCEDURE ADM.SPE_OBTIENE_C_GRUPO_PREGUNTA 
	@PIN_ID_GRUPO_PREGUNTA AS int = NULL
	, @PIN_CL_GRUPO_PREGUNTA AS nvarchar(20) = NULL
	, @PIN_NB_GRUPO_PREGUNTA AS nvarchar(200) = NULL
	, @PIN_ID_PREGUNTA AS int = NULL
	
AS   
	--SE DEVUELVE LOS REGISTROS EN BASE A LOS PARAMETROS INSERTADOS
	SELECT 
		[ID_GRUPO_PREGUNTA]
		, [CL_GRUPO_PREGUNTA]
		, [NB_GRUPO_PREGUNTA]
		, [ID_PREGUNTA]
		,'' as DS_FILTRO
	FROM ADM.C_GRUPO_PREGUNTA
	WHERE (@PIN_ID_GRUPO_PREGUNTA IS NULL OR (@PIN_ID_GRUPO_PREGUNTA IS NOT NULL AND [ID_GRUPO_PREGUNTA] = @PIN_ID_GRUPO_PREGUNTA))
			 AND (@PIN_CL_GRUPO_PREGUNTA IS NULL OR (@PIN_CL_GRUPO_PREGUNTA IS NOT NULL AND [CL_GRUPO_PREGUNTA] = @PIN_CL_GRUPO_PREGUNTA))
			 AND (@PIN_NB_GRUPO_PREGUNTA IS NULL OR (@PIN_NB_GRUPO_PREGUNTA IS NOT NULL AND [NB_GRUPO_PREGUNTA] = @PIN_NB_GRUPO_PREGUNTA))
			 AND (@PIN_ID_PREGUNTA IS NULL OR (@PIN_ID_PREGUNTA IS NOT NULL AND [ID_PREGUNTA] = @PIN_ID_PREGUNTA))
			