-- =============================================
-- Proyecto: Sigein 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 14/09/2015
-- Description: Obtiene los S_TIPO_DATO 
-- =============================================
ALTER PROCEDURE ADM.SPE_OBTIENE_S_TIPO_DATO 
	    @PIN_CL_TIPO_DATO AS nvarchar(30) = NULL,
        @PIN_NB_TIPO_DATO AS nvarchar(100) = NULL

AS   
	SELECT 
		[CL_TIPO_DATO],
		[NB_TIPO_DATO]
		,'' as DS_FILTRO
	FROM ADM.S_TIPO_DATO
	WHERE (@PIN_CL_TIPO_DATO IS NULL OR (@PIN_CL_TIPO_DATO IS NOT NULL AND [CL_TIPO_DATO] = @PIN_CL_TIPO_DATO)) AND 
		  (@PIN_NB_TIPO_DATO IS NULL OR (@PIN_NB_TIPO_DATO IS NOT NULL AND [NB_TIPO_DATO] = @PIN_NB_TIPO_DATO)) 

GO