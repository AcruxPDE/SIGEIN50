-- =============================================
-- Proyecto: Sigein 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 14/09/2015
-- Description: Obtiene los C_ESTADO 
-- =============================================
CREATE PROCEDURE [ADM].[SPE_OBTIENE_C_ESTADO] 
	    @PIN_ID_ESTADO AS int = NULL,
        @PIN_CL_PAIS AS nvarchar(10) = NULL,
        @PIN_CL_ESTADO AS nvarchar(10) = NULL,
        @PIN_NB_ESTADO AS nvarchar(100) = NULL

AS   
	SELECT 
		[ID_ESTADO],
		[CL_PAIS],
		[CL_ESTADO],
		[NB_ESTADO]
		,'' as DS_FILTRO
	FROM ADM.C_ESTADO
	WHERE (@PIN_ID_ESTADO IS NULL OR (@PIN_ID_ESTADO IS NOT NULL AND [ID_ESTADO] = @PIN_ID_ESTADO)) AND 
			(@PIN_CL_PAIS IS NULL OR (@PIN_CL_PAIS IS NOT NULL AND [CL_PAIS] = @PIN_CL_PAIS)) AND 
			(@PIN_CL_ESTADO IS NULL OR (@PIN_CL_ESTADO IS NOT NULL AND [CL_ESTADO] = @PIN_CL_ESTADO)) AND 
			(@PIN_NB_ESTADO IS NULL OR (@PIN_NB_ESTADO IS NOT NULL AND [NB_ESTADO] = @PIN_NB_ESTADO)) 
	order by [NB_ESTADO] asc

