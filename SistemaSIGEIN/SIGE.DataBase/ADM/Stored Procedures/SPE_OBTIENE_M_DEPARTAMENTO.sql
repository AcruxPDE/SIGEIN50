﻿-- =============================================
-- Proyecto: Sigein 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 14/09/2015
-- Description: Obtiene los M_DEPARTAMENTO 
-- =============================================
CREATE PROCEDURE [ADM].[SPE_OBTIENE_M_DEPARTAMENTO] 
	    @PIN_ID_DEPARTAMENTO AS int = NULL,
        @PIN_FG_ACTIVO AS bit = NULL,
        @PIN_FE_INACTIVO AS datetime = NULL,
        @PIN_CL_DEPARTAMENTO AS nvarchar(30) = NULL,
        @PIN_NB_DEPARTAMENTO AS nvarchar(100) = NULL,
        @PIN_XML_CAMPOS_ADICIONALES AS xml = NULL

AS   
	SELECT 
		[ID_DEPARTAMENTO],
		[FG_ACTIVO]
		,(CASE WHEN FG_ACTIVO=1 THEN 'Si'
			WHEN FG_ACTIVO=0 THEN 'No'
		END)AS NB_ACTIVO
		,[FE_INACTIVO],
		[CL_DEPARTAMENTO],
		[NB_DEPARTAMENTO],
		[XML_CAMPOS_ADICIONALES]
		,'' as DS_FILTRO
	FROM ADM.M_DEPARTAMENTO
	WHERE (@PIN_ID_DEPARTAMENTO IS NULL OR (@PIN_ID_DEPARTAMENTO IS NOT NULL AND [ID_DEPARTAMENTO] = @PIN_ID_DEPARTAMENTO)) AND 
			(@PIN_FG_ACTIVO IS NULL OR (@PIN_FG_ACTIVO IS NOT NULL AND [FG_ACTIVO] = @PIN_FG_ACTIVO)) AND 
			(@PIN_FE_INACTIVO IS NULL OR (@PIN_FE_INACTIVO IS NOT NULL AND [FE_INACTIVO] = @PIN_FE_INACTIVO)) AND 
			(@PIN_CL_DEPARTAMENTO IS NULL OR (@PIN_CL_DEPARTAMENTO IS NOT NULL AND [CL_DEPARTAMENTO] = @PIN_CL_DEPARTAMENTO)) AND 
			(@PIN_NB_DEPARTAMENTO IS NULL OR (@PIN_NB_DEPARTAMENTO IS NOT NULL AND [NB_DEPARTAMENTO] = @PIN_NB_DEPARTAMENTO))

