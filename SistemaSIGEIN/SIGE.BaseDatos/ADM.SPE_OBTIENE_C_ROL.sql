-- =============================================
-- Proyecto: Sigein 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 14/09/2015
-- Description: Obtiene los C_ROL 
-- =============================================
ALTER PROCEDURE ADM.SPE_OBTIENE_C_ROL 
	    @PIN_ID_ROL AS int = NULL,
        @PIN_CL_ROL AS nvarchar(30) = NULL,
        @PIN_NB_ROL AS nvarchar(100) = NULL,
        @PIN_XML_AUTORIZACION AS xml = NULL,
        @PIN_FG_ACTIVO AS bit = NULL,
        @PIN_FE_INACTIVO AS datetime = NULL

AS   
	SELECT 
		[ID_ROL],
		[CL_ROL],
		[NB_ROL],
		[XML_AUTORIZACION],
		[FG_ACTIVO],
		[FE_INACTIVO]
		,'' as DS_FILTRO
	FROM ADM.C_ROL
	WHERE (@PIN_ID_ROL IS NULL OR (@PIN_ID_ROL IS NOT NULL AND [ID_ROL] = @PIN_ID_ROL)) AND 
			(@PIN_CL_ROL IS NULL OR (@PIN_CL_ROL IS NOT NULL AND [CL_ROL] = @PIN_CL_ROL)) AND 
			(@PIN_NB_ROL IS NULL OR (@PIN_NB_ROL IS NOT NULL AND [NB_ROL] = @PIN_NB_ROL)) AND 
			(@PIN_FG_ACTIVO IS NULL OR (@PIN_FG_ACTIVO IS NOT NULL AND [FG_ACTIVO] = @PIN_FG_ACTIVO)) AND 
			(@PIN_FE_INACTIVO IS NULL OR (@PIN_FE_INACTIVO IS NOT NULL AND [FE_INACTIVO] = @PIN_FE_INACTIVO)) 

GO