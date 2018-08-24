﻿-- =============================================
-- Proyecto: Sigein 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 14/09/2015
-- Description: Obtiene los C_SECUENCIA 
-- =============================================
CREATE PROCEDURE ADM.SPE_OBTIENE_C_SECUENCIA 
	     @PIN_CL_SECUENCIA AS nvarchar(20) = NULL,
        @PIN_CL_PREFIJO AS nvarchar(5) = NULL,
        @PIN_NO_ULTIMO_VALOR AS int = NULL,
        @PIN_NO_VALOR_MAXIMO AS int = NULL,
        @PIN_CL_SUFIJO AS nvarchar(5) = NULL,
        @PIN_NO_DIGITOS AS tinyint = NULL

AS   
	SELECT 
		[CL_SECUENCIA],
		[CL_PREFIJO],
		[NO_ULTIMO_VALOR],
		[NO_VALOR_MAXIMO],
		[CL_SUFIJO],
		[NO_DIGITOS]
		,'' as DS_FILTRO
	FROM ADM.C_SECUENCIA
	WHERE (@PIN_CL_SECUENCIA IS NULL OR (@PIN_CL_SECUENCIA IS NOT NULL AND [CL_SECUENCIA] = @PIN_CL_SECUENCIA)) AND 
			(@PIN_CL_PREFIJO IS NULL OR (@PIN_CL_PREFIJO IS NOT NULL AND [CL_PREFIJO] = @PIN_CL_PREFIJO)) AND 
			(@PIN_NO_ULTIMO_VALOR IS NULL OR (@PIN_NO_ULTIMO_VALOR IS NOT NULL AND [NO_ULTIMO_VALOR] = @PIN_NO_ULTIMO_VALOR)) AND 
			(@PIN_NO_VALOR_MAXIMO IS NULL OR (@PIN_NO_VALOR_MAXIMO IS NOT NULL AND [NO_VALOR_MAXIMO] = @PIN_NO_VALOR_MAXIMO)) AND 
			(@PIN_CL_SUFIJO IS NULL OR (@PIN_CL_SUFIJO IS NOT NULL AND [CL_SUFIJO] = @PIN_CL_SUFIJO)) AND 
			(@PIN_NO_DIGITOS IS NULL OR (@PIN_NO_DIGITOS IS NOT NULL AND [NO_DIGITOS] = @PIN_NO_DIGITOS)) 
