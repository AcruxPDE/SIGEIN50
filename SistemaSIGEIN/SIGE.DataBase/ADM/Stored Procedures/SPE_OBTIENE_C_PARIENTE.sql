﻿-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 02/11/2015
-- Description: Obtiene los datos de la tabla C_PARIENTE 
-- =============================================
CREATE PROCEDURE ADM.SPE_OBTIENE_C_PARIENTE 
	@PIN_ID_PARIENTE AS int = NULL
	, @PIN_NB_PARIENTE AS nvarchar(50) = NULL
	, @PIN_CL_PARENTEZCO AS nvarchar(20) = NULL
	, @PIN_CL_GENERO AS nvarchar(1) = NULL
	, @PIN_FE_NACIMIENTO AS date = NULL
	, @PIN_ID_EMPLEADO AS int = NULL
	, @PIN_ID_CANDIDATO AS int = NULL
	, @PIN_ID_BITACORA AS int = NULL
	, @PIN_CL_OCUPACION AS nvarchar(50) = NULL
	, @PIN_FG_DEPENDIENTE AS bit = NULL
	, @PIN_FG_ACTIVO AS bit = NULL
	
AS   
	--SE DEVUELVE LOS REGISTROS EN BASE A LOS PARAMETROS INSERTADOS
	SELECT 
		ID_PARIENTE
		, NB_PARIENTE
		, CL_PARENTESCO
		, CL_GENERO
		, FE_NACIMIENTO
		, ID_EMPLEADO
		, ID_CANDIDATO
		, ID_BITACORA
		, CL_OCUPACION
		, FG_DEPENDIENTE
		, FG_ACTIVO
		
		,'' as DS_FILTRO
	FROM ADM.C_PARIENTE
	WHERE (@PIN_ID_PARIENTE IS NULL OR (@PIN_ID_PARIENTE IS NOT NULL AND ID_PARIENTE = @PIN_ID_PARIENTE))
			 AND (@PIN_NB_PARIENTE IS NULL OR (@PIN_NB_PARIENTE IS NOT NULL AND NB_PARIENTE = @PIN_NB_PARIENTE))
			 AND (@PIN_CL_PARENTEZCO IS NULL OR (@PIN_CL_PARENTEZCO IS NOT NULL AND CL_PARENTESCO = @PIN_CL_PARENTEZCO))
			 AND (@PIN_CL_GENERO IS NULL OR (@PIN_CL_GENERO IS NOT NULL AND CL_GENERO = @PIN_CL_GENERO))
			 AND (@PIN_FE_NACIMIENTO IS NULL OR (@PIN_FE_NACIMIENTO IS NOT NULL AND FE_NACIMIENTO = @PIN_FE_NACIMIENTO))
			 AND (@PIN_ID_EMPLEADO IS NULL OR (@PIN_ID_EMPLEADO IS NOT NULL AND ID_EMPLEADO = @PIN_ID_EMPLEADO))
			 AND (@PIN_ID_CANDIDATO IS NULL OR (@PIN_ID_CANDIDATO IS NOT NULL AND ID_CANDIDATO = @PIN_ID_CANDIDATO))
			 AND (@PIN_ID_BITACORA IS NULL OR (@PIN_ID_BITACORA IS NOT NULL AND ID_BITACORA = @PIN_ID_BITACORA))
			 AND (@PIN_CL_OCUPACION IS NULL OR (@PIN_CL_OCUPACION IS NOT NULL AND CL_OCUPACION = @PIN_CL_OCUPACION))
			 AND (@PIN_FG_DEPENDIENTE IS NULL OR (@PIN_FG_DEPENDIENTE IS NOT NULL AND FG_DEPENDIENTE = @PIN_FG_DEPENDIENTE))
			 AND (@PIN_FG_ACTIVO IS NULL OR (@PIN_FG_ACTIVO IS NOT NULL AND FG_ACTIVO = @PIN_FG_ACTIVO))
			
