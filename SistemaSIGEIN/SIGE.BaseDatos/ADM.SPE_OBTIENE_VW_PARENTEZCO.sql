-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 04/11/2015
-- Description: Obtiene los datos de la tabla VW_PARENTEZCO 
-- =============================================
ALTER PROCEDURE ADM.SPE_OBTIENE_VW_PARENTEZCO 
	@PIN_NB_PARENTEZCO AS varchar(8) = NULL
	
AS   

	SELECT 
		[NB_PARENTEZCO]		
		,'' as DS_FILTRO
	FROM ADM.VW_PARENTEZCO
	WHERE (@PIN_NB_PARENTEZCO IS NULL OR (@PIN_NB_PARENTEZCO IS NOT NULL AND [NB_PARENTEZCO] = @PIN_NB_PARENTEZCO))
			
GO
