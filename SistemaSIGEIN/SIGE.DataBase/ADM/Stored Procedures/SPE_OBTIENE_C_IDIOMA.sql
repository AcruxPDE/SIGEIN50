-- =============================================
-- Proyecto: Sigein 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 15/09/2015
-- Description: Obtiene los C_IDIOMA 
-- =============================================
CREATE PROCEDURE [ADM].[SPE_OBTIENE_C_IDIOMA] 
	    @PIN_ID_IDIOMA AS int = NULL,
        @PIN_CL_IDIOMA AS nvarchar(20) = NULL,
        @PIN_NB_IDIOMA AS nvarchar(200) = NULL,
        @PIN_FG_ACTIVO AS bit = NULL

AS   
	SELECT 
		[ID_IDIOMA],
		[CL_IDIOMA],
		[NB_IDIOMA],
		[FG_ACTIVO],
		'' as DS_FILTRO,
		CASE FG_ACTIVO WHEN 1 THEN 'Sí' ELSE 'No' END AS CL_ACTIVO
	FROM ADM.C_IDIOMA
	WHERE (@PIN_ID_IDIOMA IS NULL OR (@PIN_ID_IDIOMA IS NOT NULL AND [ID_IDIOMA] = @PIN_ID_IDIOMA)) AND 
			(@PIN_CL_IDIOMA IS NULL OR (@PIN_CL_IDIOMA IS NOT NULL AND [CL_IDIOMA] = @PIN_CL_IDIOMA)) AND 
			(@PIN_NB_IDIOMA IS NULL OR (@PIN_NB_IDIOMA IS NOT NULL AND [NB_IDIOMA] = @PIN_NB_IDIOMA)) AND 
    		(@PIN_FG_ACTIVO IS NULL OR (@PIN_FG_ACTIVO IS NOT NULL AND [FG_ACTIVO] = @PIN_FG_ACTIVO)) 

