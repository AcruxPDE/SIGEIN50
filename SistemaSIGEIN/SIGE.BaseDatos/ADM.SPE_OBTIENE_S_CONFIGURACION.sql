-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 18/09/2015
-- Description: Obtiene los datos de la tabla S_CONFIGURACION 
-- =============================================
ALTER PROCEDURE ADM.SPE_OBTIENE_S_CONFIGURACION 
	  @PIN_XML_CONFIGURACION AS xml = NULL
	, @PIN_CL_USUARIO_MODIFICA AS nvarchar(50) = NULL
	
AS   
	--SE DEVUELVE LOS REGISTROS EN BASE A LOS PARAMETROS INSERTADOS
	SELECT 
		[XML_CONFIGURACION]
		, [CL_USUARIO_MODIFICA]
		,'' as DS_FILTRO
	FROM ADM.S_CONFIGURACION
	WHERE (@PIN_CL_USUARIO_MODIFICA IS NULL OR (@PIN_CL_USUARIO_MODIFICA IS NOT NULL AND [CL_USUARIO_MODIFICA] = @PIN_CL_USUARIO_MODIFICA))
			
GO