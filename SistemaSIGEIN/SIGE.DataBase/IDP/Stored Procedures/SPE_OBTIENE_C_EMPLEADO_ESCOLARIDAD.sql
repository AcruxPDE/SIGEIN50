﻿-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 12/11/2015
-- Description: Obtiene los datos de la tabla C_EMPLEADO_ESCOLARIDAD 
-- =============================================
CREATE PROCEDURE [IDP].[SPE_OBTIENE_C_EMPLEADO_ESCOLARIDAD] 
	@PIN_ID_EMPLEADO_ESCOLARIDAD AS int = NULL
	, @PIN_ID_EMPLEADO AS int = NULL
	, @PIN_ID_CANDIDATO AS int = NULL
	, @PIN_ID_ESCOLARIDAD AS int = NULL
	, @PIN_CL_INSTITUCION AS int = NULL
	, @PIN_NB_INSTITUCION AS nvarchar(50) = NULL
	, @PIN_FE_PERIODO_INICIO AS date = NULL
	, @PIN_FE_PERIODO_FIN AS date = NULL
	, @PIN_CL_ESTADO_ESCOLARIDAD AS nvarchar(10) = NULL
	
AS   
	--SE DEVUELVE LOS REGISTROS EN BASE A LOS PARAMETROS INSERTADOS
	SELECT 
		CM.[ID_EMPLEADO_ESCOLARIDAD]
	  , CM.[ID_EMPLEADO]
	  , CM.[ID_CANDIDATO]
	  , CM.[ID_ESCOLARIDAD]
	  , CM.[CL_INSTITUCION]
	  , CM.[NB_INSTITUCION]
	  , CM.[FE_PERIODO_INICIO]
	  , CM.[FE_PERIODO_FIN]
	  , CM.[CL_ESTADO_ESCOLARIDAD]
	  ,'' as DS_FILTRO
	  , CN.CL_NIVEL_ESCOLARIDAD
	FROM IDP.C_EMPLEADO_ESCOLARIDAD CM
	JOIN ADM.C_ESCOLARIDAD CE ON CM.ID_ESCOLARIDAD = CE.ID_ESCOLARIDAD
	JOIN ADM.C_NIVEL_ESCOLARIDAD CN ON CE.CL_NIVEL_ESCOLARIDAD = CN.CL_NIVEL_ESCOLARIDAD
	WHERE (@PIN_ID_EMPLEADO_ESCOLARIDAD IS NULL OR (@PIN_ID_EMPLEADO_ESCOLARIDAD IS NOT NULL AND CM.[ID_EMPLEADO_ESCOLARIDAD] = @PIN_ID_EMPLEADO_ESCOLARIDAD))
			 AND (@PIN_ID_EMPLEADO IS NULL OR (@PIN_ID_EMPLEADO IS NOT NULL AND CM.[ID_EMPLEADO] = @PIN_ID_EMPLEADO))
			 AND (@PIN_ID_CANDIDATO IS NULL OR (@PIN_ID_CANDIDATO IS NOT NULL AND CM.[ID_CANDIDATO] = @PIN_ID_CANDIDATO))
			 AND (@PIN_ID_ESCOLARIDAD IS NULL OR (@PIN_ID_ESCOLARIDAD IS NOT NULL AND CM.[ID_ESCOLARIDAD] = @PIN_ID_ESCOLARIDAD))
			 AND (@PIN_CL_INSTITUCION IS NULL OR (@PIN_CL_INSTITUCION IS NOT NULL AND CM.[CL_INSTITUCION] = @PIN_CL_INSTITUCION))
			 AND (@PIN_NB_INSTITUCION IS NULL OR (@PIN_NB_INSTITUCION IS NOT NULL AND CM.[NB_INSTITUCION] = @PIN_NB_INSTITUCION))
			 AND (@PIN_FE_PERIODO_INICIO IS NULL OR (@PIN_FE_PERIODO_INICIO IS NOT NULL AND CM.[FE_PERIODO_INICIO] = @PIN_FE_PERIODO_INICIO))
			 AND (@PIN_FE_PERIODO_FIN IS NULL OR (@PIN_FE_PERIODO_FIN IS NOT NULL AND CM.[FE_PERIODO_FIN] = @PIN_FE_PERIODO_FIN))
			 AND (@PIN_CL_ESTADO_ESCOLARIDAD IS NULL OR (@PIN_CL_ESTADO_ESCOLARIDAD IS NOT NULL AND CM.[CL_ESTADO_ESCOLARIDAD] = @PIN_CL_ESTADO_ESCOLARIDAD))
			
