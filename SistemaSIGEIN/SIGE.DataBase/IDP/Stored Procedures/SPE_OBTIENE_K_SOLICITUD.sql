﻿-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Juan Pérez
-- CREATE date: 18/12/2015
-- Description: Obtiene los datos de la tabla K_SOLICITUD 
-- =============================================
CREATE PROCEDURE [IDP].[SPE_OBTIENE_K_SOLICITUD] 
	@PIN_ID_SOLICITUD AS int = NULL
	, @PIN_ID_CANDIDATO AS int = NULL
	, @PIN_ID_EMPLEADO AS int = NULL
	, @PIN_ID_DESCRIPTIVO AS int = NULL
	, @PIN_ID_REQUISICION AS int = NULL
	, @PIN_CL_SOLICITUD AS nvarchar(20) = NULL
	, @PIN_CL_ACCESO_EVALUACION AS nvarchar(40) = NULL
	, @PIN_ID_PLANTILLA_SOLICITUD AS int = NULL
	, @PIN_DS_COMPETENCIAS_ADICIONALES AS nvarchar(1000) = NULL
	, @PIN_CL_SOLICITUD_ESTATUS AS nvarchar(20) = NULL
	, @PIN_FE_SOLICITUD AS DATEtime = NULL
	

AS   
	--SE DEVUELVE LOS REGISTROS EN BASE A LOS PARAMETROS INSERTADOS
	SELECT 
		  KS.[ID_SOLICITUD]
		, KS.[ID_CANDIDATO]
		, KS.[ID_EMPLEADO]
		, KS.[ID_DESCRIPTIVO]
		, KS.[ID_REQUISICION]
		, KS.[CL_SOLICITUD]
		, KS.[CL_ACCESO_EVALUACION]
		, KS.[ID_PLANTILLA_SOLICITUD]
		, CC.[DS_COMPETENCIAS_ADICIONALES]
		, KS.CL_SOLICITUD_ESTATUS
		, KS.FE_SOLICITUD
		
		,CONCAT ( ME.[NB_EMPLEADO], ' ',ME.[NB_APELLIDO_PATERNO],' ' , ME.[NB_APELLIDO_MATERNO] ) AS NB_EMPLEADO_COMPLETO
		,ME.CL_CORREO_ELECTRONICO
		,ME.CL_EMPLEADO
		,ME.MN_SUELDO

		,KR.NO_REQUISICION
		,KR.CL_CAUSA
		,KR.CL_ESTADO
		,KR.FE_SOLICITUD AS REQUISICION_FE_SOLICITUD
		
		,CE.NB_EMPRESA
		,CE.CL_EMPRESA
		,CE.ID_EMPRESA

		,CONCAT ( CC.[NB_CANDIDATO], ' ',CC.[NB_APELLIDO_PATERNO],' ' , CC.[NB_APELLIDO_MATERNO] ) AS NB_CANDIDATO_COMPLETO
		,CC.XML_EGRESOS
		,CC.XML_INGRESOS
		,CC.XML_PATRIMONIO
		,CC.XML_PERFIL_RED_SOCIAL
		,CC.XML_TELEFONOS

	FROM ADM.K_SOLICITUD KS
		LEFT OUTER JOIN ADM.C_CANDIDATO CC ON CC.ID_CANDIDATO = KS.ID_CANDIDATO
		LEFT OUTER JOIN ADM.M_EMPLEADO ME ON ME.ID_EMPLEADO = KS.ID_EMPLEADO
		LEFT OUTER JOIN ADM.M_PUESTO MP ON MP.ID_PUESTO = KS.ID_DESCRIPTIVO
		LEFT OUTER JOIN ADM.K_REQUISICION KR ON KR.ID_REQUISICION = KS.ID_REQUISICION
		LEFT OUTER JOIN ADM.C_EMPRESA CE ON CE.ID_EMPRESA = KR.ID_EMPRESA

	WHERE (@PIN_ID_SOLICITUD IS NULL OR (@PIN_ID_SOLICITUD IS NOT NULL AND KS.[ID_SOLICITUD] = @PIN_ID_SOLICITUD))
			 AND (@PIN_ID_CANDIDATO IS NULL OR (@PIN_ID_CANDIDATO IS NOT NULL AND KS.[ID_CANDIDATO] = @PIN_ID_CANDIDATO))
			 AND (@PIN_ID_EMPLEADO IS NULL OR (@PIN_ID_EMPLEADO IS NOT NULL AND KS.[ID_EMPLEADO] = @PIN_ID_EMPLEADO))
			 AND (@PIN_ID_DESCRIPTIVO IS NULL OR (@PIN_ID_DESCRIPTIVO IS NOT NULL AND KS.[ID_DESCRIPTIVO] = @PIN_ID_DESCRIPTIVO))
			 AND (@PIN_ID_REQUISICION IS NULL OR (@PIN_ID_REQUISICION IS NOT NULL AND KS.[ID_REQUISICION] = @PIN_ID_REQUISICION))
			 AND (@PIN_CL_SOLICITUD IS NULL OR (@PIN_CL_SOLICITUD IS NOT NULL AND KS.[CL_SOLICITUD] = @PIN_CL_SOLICITUD))
			 AND (@PIN_CL_ACCESO_EVALUACION IS NULL OR (@PIN_CL_ACCESO_EVALUACION IS NOT NULL AND KS.[CL_ACCESO_EVALUACION] = @PIN_CL_ACCESO_EVALUACION))
			 AND (@PIN_ID_PLANTILLA_SOLICITUD IS NULL OR (@PIN_ID_PLANTILLA_SOLICITUD IS NOT NULL AND KS.[ID_PLANTILLA_SOLICITUD] = @PIN_ID_PLANTILLA_SOLICITUD))
			 AND (@PIN_DS_COMPETENCIAS_ADICIONALES IS NULL OR (@PIN_DS_COMPETENCIAS_ADICIONALES IS NOT NULL AND CC.[DS_COMPETENCIAS_ADICIONALES] = @PIN_DS_COMPETENCIAS_ADICIONALES))
			
	--EXECUTE [ADM].[SPE_OBTIENE_K_SOLICITUD] 