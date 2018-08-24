-- =============================================
-- Proyecto: Sigein 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 15/09/2015
-- Description: Obtiene los C_ESCOLARIDAD 
-- =============================================
ALTER PROCEDURE ADM.SPE_OBTIENE_C_ESCOLARIDAD 
	    @PIN_ID_ESCOLARIDAD AS int = NULL,
        @PIN_NB_ESCOLARIDAD AS nvarchar(100) = NULL,
        @PIN_DS_ESCOLARIDAD AS nvarchar(200) = NULL,
        @PIN_CL_NIVEL_ESCOLARIDAD AS nvarchar(20) = NULL,
        @PIN_FG_ACTIVO AS bit = NULL

AS   
	SELECT 
			[ID_ESCOLARIDAD],
			[NB_ESCOLARIDAD],
			[DS_ESCOLARIDAD],
			[CL_NIVEL_ESCOLARIDAD],
			[FG_ACTIVO]
			,'' as DS_FILTRO
	FROM ADM.C_ESCOLARIDAD
	WHERE (@PIN_ID_ESCOLARIDAD IS NULL OR (@PIN_ID_ESCOLARIDAD IS NOT NULL AND [ID_ESCOLARIDAD] = @PIN_ID_ESCOLARIDAD)) AND 
			(@PIN_NB_ESCOLARIDAD IS NULL OR (@PIN_NB_ESCOLARIDAD IS NOT NULL AND [NB_ESCOLARIDAD] = @PIN_NB_ESCOLARIDAD)) AND 
			(@PIN_DS_ESCOLARIDAD IS NULL OR (@PIN_DS_ESCOLARIDAD IS NOT NULL AND [DS_ESCOLARIDAD] = @PIN_DS_ESCOLARIDAD)) AND 
			(@PIN_CL_NIVEL_ESCOLARIDAD IS NULL OR (@PIN_CL_NIVEL_ESCOLARIDAD IS NOT NULL AND [CL_NIVEL_ESCOLARIDAD] = @PIN_CL_NIVEL_ESCOLARIDAD)) AND 
			(@PIN_FG_ACTIVO IS NULL OR (@PIN_FG_ACTIVO IS NOT NULL AND [FG_ACTIVO] = @PIN_FG_ACTIVO)) 

GO