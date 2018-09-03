﻿
-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Abraham Ramirez
-- CREATE date: 21/09/2015
-- Description: Obtiene los datos de la tabla C_EMPLEADO_RELACIONADO 
-- =============================================
CREATE PROCEDURE ADM.SPE_OBTIENE_C_EMPLEADO_RELACIONADO 
	@PIN_ID_EMPLEADO AS int = NULL
	, @PIN_ID_EMPLEADO_RELACIONADO AS int = NULL
	, @PIN_CL_TIPO_RELACION AS nvarchar(20) = NULL
	, @PIN_DS_EMPLEADO_RELACIONADO AS nvarchar(500) = NULL
	
AS   
	--SE DEVUELVE LOS REGISTROS EN BASE A LOS PARAMETROS INSERTADOS
	SELECT 
		[ID_EMPLEADO]
		, [ID_EMPLEADO_RELACIONADO]
		, [CL_TIPO_RELACION]
		, [DS_EMPLEADO_RELACIONADO]
		,'' as DS_FILTRO
	FROM ADM.C_EMPLEADO_RELACIONADO
	WHERE (@PIN_ID_EMPLEADO IS NULL OR (@PIN_ID_EMPLEADO IS NOT NULL AND [ID_EMPLEADO] = @PIN_ID_EMPLEADO))
			 AND (@PIN_ID_EMPLEADO_RELACIONADO IS NULL OR (@PIN_ID_EMPLEADO_RELACIONADO IS NOT NULL AND [ID_EMPLEADO_RELACIONADO] = @PIN_ID_EMPLEADO_RELACIONADO))
			 AND (@PIN_CL_TIPO_RELACION IS NULL OR (@PIN_CL_TIPO_RELACION IS NOT NULL AND [CL_TIPO_RELACION] = @PIN_CL_TIPO_RELACION))
			 AND (@PIN_DS_EMPLEADO_RELACIONADO IS NULL OR (@PIN_DS_EMPLEADO_RELACIONADO IS NOT NULL AND [DS_EMPLEADO_RELACIONADO] = @PIN_DS_EMPLEADO_RELACIONADO))
			
