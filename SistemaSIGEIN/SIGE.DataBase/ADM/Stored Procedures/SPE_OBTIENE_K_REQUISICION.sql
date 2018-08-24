﻿--  select getdate()
-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Jesús Vázquez Pereyra
-- CREATE date: 28/10/2015
-- Description: Obtiene los datos de la tabla K_REQUISICION 
-- =============================================
CREATE PROCEDURE [ADM].[SPE_OBTIENE_K_REQUISICION] 
	@PIN_ID_REQUISICION AS int = NULL
	, @PIN_NO_REQUISICION AS nvarchar(20) = NULL
	, @PIN_FE_SOLICITUD AS datetime = NULL
	, @PIN_FE_REQUISICION AS datetime = NULL
	, @PIN_ID_PUESTO AS int = NULL
	, @PIN_CL_ESTADO AS nvarchar(20) = NULL
	, @PIN_CL_CAUSA AS nvarchar(20) = NULL
	, @PIN_DS_CAUSA AS nvarchar(100) = NULL
	, @PIN_ID_NOTIFICACION AS int = NULL
	, @PIN_ID_SOLICITANTE AS int = NULL
	, @PIN_ID_AUTORIZA AS int = NULL
	, @PIN_ID_VISTO_BUENO AS int = NULL
	, @PIN_ID_EMPRESA AS int = NULL
	
AS   
	--SE DEVUELVE LOS REGISTROS EN BASE A LOS PARAMETROS INSERTADOS
	SELECT 
		R.ID_REQUISICION
		, R.NO_REQUISICION
		, R.FE_SOLICITUD
		, R.ID_PUESTO
		, R.CL_ESTADO
		, R.CL_CAUSA
		, R.DS_CAUSA
		, R.ID_NOTIFICACION
		, R.ID_SOLICITANTE
		, R.ID_AUTORIZA
		, R.ID_VISTO_BUENO
		, R.ID_EMPRESA
		, E.CL_EMPRESA
		, E.NB_EMPRESA
		, E.NB_RAZON_SOCIAL
		, R.FE_REQUERIMIENTO
		 ,P.CL_PUESTO
		 ,P.NB_PUESTO
		,'' as DS_FILTRO
	FROM ADM.K_REQUISICION as R
	LEFT OUTER JOIN ADM.C_EMPRESA AS E ON E.ID_EMPRESA = R.ID_EMPRESA
	LEFT OUTER JOIN ADM.M_PUESTO AS P ON P.ID_PUESTO = R.ID_PUESTO
	WHERE (@PIN_ID_REQUISICION IS NULL OR (@PIN_ID_REQUISICION IS NOT NULL AND R.ID_REQUISICION = @PIN_ID_REQUISICION))
			 AND (@PIN_NO_REQUISICION IS NULL OR (@PIN_NO_REQUISICION IS NOT NULL AND R.NO_REQUISICION = @PIN_NO_REQUISICION))
			 AND (@PIN_FE_SOLICITUD IS NULL OR (@PIN_FE_SOLICITUD IS NOT NULL AND R.FE_SOLICITUD = @PIN_FE_SOLICITUD))
			 AND (@PIN_FE_REQUISICION IS NULL OR (@PIN_FE_REQUISICION IS NOT NULL AND R.FE_REQUERIMIENTO= @PIN_FE_REQUISICION))
			 AND (@PIN_ID_PUESTO IS NULL OR (@PIN_ID_PUESTO IS NOT NULL AND R.ID_PUESTO = @PIN_ID_PUESTO))
			 AND (@PIN_CL_ESTADO IS NULL OR (@PIN_CL_ESTADO IS NOT NULL AND R.CL_ESTADO = @PIN_CL_ESTADO))
			 AND (@PIN_CL_CAUSA IS NULL OR (@PIN_CL_CAUSA IS NOT NULL AND R.CL_CAUSA = @PIN_CL_CAUSA))
			 AND (@PIN_DS_CAUSA IS NULL OR (@PIN_DS_CAUSA IS NOT NULL AND R.DS_CAUSA = @PIN_DS_CAUSA))
			 AND (@PIN_ID_NOTIFICACION IS NULL OR (@PIN_ID_NOTIFICACION IS NOT NULL AND R.ID_NOTIFICACION = @PIN_ID_NOTIFICACION))
			 AND (@PIN_ID_SOLICITANTE IS NULL OR (@PIN_ID_SOLICITANTE IS NOT NULL AND R.ID_SOLICITANTE = @PIN_ID_SOLICITANTE))
			 AND (@PIN_ID_AUTORIZA IS NULL OR (@PIN_ID_AUTORIZA IS NOT NULL AND R.ID_AUTORIZA = @PIN_ID_AUTORIZA))
			 AND (@PIN_ID_VISTO_BUENO IS NULL OR (@PIN_ID_VISTO_BUENO IS NOT NULL AND R.ID_VISTO_BUENO = @PIN_ID_VISTO_BUENO))
			 AND (@PIN_ID_EMPRESA IS NULL OR (@PIN_ID_EMPRESA IS NOT NULL AND R.ID_EMPRESA = @PIN_ID_EMPRESA))
			
			--EXECUTE [ADM].[SPE_OBTIENE_K_REQUISICION] 