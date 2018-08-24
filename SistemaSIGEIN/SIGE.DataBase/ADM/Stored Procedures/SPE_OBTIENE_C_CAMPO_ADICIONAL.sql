﻿-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Jesús Vázquez Pereyra
-- CREATE date: 10/11/2015
-- Description: Obtiene los datos de la tabla C_CAMPO_ADICIONAL 
-- =============================================
CREATE PROCEDURE [ADM].[SPE_OBTIENE_C_CAMPO_ADICIONAL]  
	@PIN_ID_CAMPO AS int = NULL
	, @PIN_CL_CAMPO AS nvarchar(32) = NULL
	, @PIN_NB_CAMPO AS nvarchar(100) = NULL
	, @PIN_DS_CAMPO AS nvarchar(200) = NULL
	, @PIN_FG_REQUERIDO AS bit = NULL
	, @PIN_NO_VALOR_DEFECTO AS nvarchar(200) = NULL
	, @PIN_CL_TIPO_DATO AS nvarchar(20) = NULL
	, @PIN_CL_DIMENSION AS nvarchar(20) = NULL
	, @PIN_CL_TABLA_REFERENCIA AS nvarchar(50) = NULL
	, @PIN_CL_ESQUEMA_REFERENCIA AS nvarchar(10) = NULL
	, @PIN_FG_MOSTRAR AS bit = NULL
	, @PIN_ID_CATALOGO_LIST AS int = NULL
	, @PIN_FG_ADICIONAL AS bit = NULL
	
AS   
	--SE DEVUELVE LOS REGISTROS EN BASE A LOS PARAMETROS INSERTADOS
	SELECT 
		[ID_CAMPO]
		, [CL_CAMPO]
		, [NB_CAMPO]
		, [DS_CAMPO]
		, [FG_REQUERIDO]
		, [NO_VALOR_DEFECTO]
		, [CL_TIPO_DATO]
		, [CL_DIMENSION]
		, [CL_TABLA_REFERENCIA]
		, [CL_ESQUEMA_REFERENCIA]
		, [FG_MOSTRAR]
		, [ID_CATALOGO_LIST]
		, [FG_ADICIONAL]
		,'' as DS_FILTRO
	FROM ADM.C_CAMPO_ADICIONAL
	WHERE (@PIN_ID_CAMPO IS NULL OR (@PIN_ID_CAMPO IS NOT NULL AND [ID_CAMPO] = @PIN_ID_CAMPO))
			 AND (@PIN_CL_CAMPO IS NULL OR (@PIN_CL_CAMPO IS NOT NULL AND [CL_CAMPO] = @PIN_CL_CAMPO))
			 AND (@PIN_NB_CAMPO IS NULL OR (@PIN_NB_CAMPO IS NOT NULL AND [NB_CAMPO] = @PIN_NB_CAMPO))
			 AND (@PIN_DS_CAMPO IS NULL OR (@PIN_DS_CAMPO IS NOT NULL AND [DS_CAMPO] = @PIN_DS_CAMPO))
			 AND (@PIN_FG_REQUERIDO IS NULL OR (@PIN_FG_REQUERIDO IS NOT NULL AND [FG_REQUERIDO] = @PIN_FG_REQUERIDO))
			 AND (@PIN_NO_VALOR_DEFECTO IS NULL OR (@PIN_NO_VALOR_DEFECTO IS NOT NULL AND [NO_VALOR_DEFECTO] = @PIN_NO_VALOR_DEFECTO))
			 AND (@PIN_CL_TIPO_DATO IS NULL OR (@PIN_CL_TIPO_DATO IS NOT NULL AND [CL_TIPO_DATO] = @PIN_CL_TIPO_DATO))
			 AND (@PIN_CL_DIMENSION IS NULL OR (@PIN_CL_DIMENSION IS NOT NULL AND [CL_DIMENSION] = @PIN_CL_DIMENSION))
			 AND (@PIN_CL_TABLA_REFERENCIA IS NULL OR (@PIN_CL_TABLA_REFERENCIA IS NOT NULL AND [CL_TABLA_REFERENCIA] = @PIN_CL_TABLA_REFERENCIA))
			 AND (@PIN_CL_ESQUEMA_REFERENCIA IS NULL OR (@PIN_CL_ESQUEMA_REFERENCIA IS NOT NULL AND [CL_ESQUEMA_REFERENCIA] = @PIN_CL_ESQUEMA_REFERENCIA))
			 AND (@PIN_FG_MOSTRAR IS NULL OR (@PIN_FG_MOSTRAR IS NOT NULL AND [FG_MOSTRAR] = @PIN_FG_MOSTRAR))
			 AND (@PIN_ID_CATALOGO_LIST IS NULL OR (@PIN_ID_CATALOGO_LIST IS NOT NULL AND [ID_CATALOGO_LIST] = @PIN_ID_CATALOGO_LIST))
			 AND (@PIN_FG_ADICIONAL IS NULL OR (@PIN_FG_ADICIONAL IS NOT NULL AND [FG_ADICIONAL] = @PIN_FG_ADICIONAL))
			