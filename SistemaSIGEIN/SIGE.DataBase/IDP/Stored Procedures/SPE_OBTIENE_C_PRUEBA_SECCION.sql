﻿
-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Juan De Dios Pérez Pedroza
-- CREATE date: 18/01/2016
-- Description: Obtiene los datos de la tabla C_PRUEBA_TIEMPO
-- =============================================
CREATE PROCEDURE [IDP].[SPE_OBTIENE_C_PRUEBA_SECCION] 
	  @PIN_ID_PRUEBA_SECCION int = NULL
	, @PIN_ID_PRUEBA int = NULL
	, @PIN_CL_PRUEBA_SECCION AS nvarchar(20) = NULL
	, @PIN_NB_PRUEBA_SECCION AS nvarchar(200) = NULL
	, @PIN_NO_TIEMPO AS smallint = NULL


AS   
	--SE DEVUELVE LOS REGISTROS EN BASE A LOS PARAMETROS INSERTADOS
	SELECT 
		[ID_PRUEBA_SECCION]
		,[ID_PRUEBA]
		,[CL_PRUEBA_SECCION]
		,[NB_PRUEBA_SECCION]
	    ,[NO_TIEMPO]


	FROM [SIGEIN].[IDP].[C_PRUEBA_SECCION]
	WHERE	     (@PIN_ID_PRUEBA_SECCION IS NULL OR (@PIN_ID_PRUEBA_SECCION IS NOT NULL AND [ID_PRUEBA_SECCION] = @PIN_ID_PRUEBA_SECCION))
	     	 AND (@PIN_ID_PRUEBA IS NULL OR (@PIN_ID_PRUEBA IS NOT NULL AND [ID_PRUEBA] = @PIN_ID_PRUEBA))
			 AND (@PIN_CL_PRUEBA_SECCION IS NULL OR (@PIN_CL_PRUEBA_SECCION IS NOT NULL AND [CL_PRUEBA_SECCION] = @PIN_CL_PRUEBA_SECCION))
			 AND (@PIN_NB_PRUEBA_SECCION IS NULL OR (@PIN_NB_PRUEBA_SECCION IS NOT NULL AND [NB_PRUEBA_SECCION] = @PIN_NB_PRUEBA_SECCION))
			 AND (@PIN_NO_TIEMPO IS NULL OR (@PIN_NO_TIEMPO IS NOT NULL AND [NO_TIEMPO] = @PIN_NO_TIEMPO))
			