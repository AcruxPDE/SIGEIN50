﻿-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Gabriel Vázquez
-- CREATE date: 18/01/2016
-- Description: inserción de variables de baremos de prueba ESTILO DE PENSAMIENTO
-- =============================================
CREATE PROCEDURE [IDP].[SPE_INSERTA_BAREMOS_PENSAMIENTO]
	 @PIN_ID_BATERIA AS INT
	,@PIN_CL_USUARIO_APP AS NVARCHAR(50)
	,@PIN_NB_PROGRAMA AS NVARCHAR(50)

AS
BEGIN  
	--SE DECLARA E INICIALIZA LA VARIABLE QUE NOS INDICARA SI GENERAMOS LA TRANSACCION EN ESTE SP
	DECLARE @V_EXIST_TRAN BIT = 0,
			@V_FE_SISTEMA AS DATETIME = GETDATE()

	DECLARE @V_ID_CUESTIONARIO AS INT

	--SE VERIFICA SI SE INSERTA EL REGISTRO O SE ACTUALIZARA SEGUN LA VARIABLE DE TIPO DE TRANSACCION  QUE RECIBE EL SP
		
	SELECT @V_ID_CUESTIONARIO = ID_CUESTIONARIO_BAREMOS
	FROM IDP.K_BATERIA_PRUEBA
	WHERE ID_BATERIA = @PIN_ID_BATERIA

	;WITH T_VARIABLES AS (
		SELECT 'PENSAMIENTO_REP_A' AS CL_VARIABLE_REPORTE, 'PN-ANÁLISIS' AS CL_VARIABLE_BAREMOS UNION ALL
		SELECT 'PENSAMIENTO_REP_V' AS CL_VARIABLE_REPORTE, 'PN-VISIÓN' AS CL_VARIABLE_BAREMOS UNION ALL
		SELECT 'PENSAMIENTO_REP_I' AS CL_VARIABLE_REPORTE, 'PN-INTUICIÓN' AS CL_VARIABLE_BAREMOS UNION ALL
		SELECT 'PENSAMIENTO_REP_L' AS CL_VARIABLE_REPORTE, 'PN-LÓGICA' AS CL_VARIABLE_BAREMOS
	), T_RESULTADOS AS (

		SELECT CV.ID_VARIABLE, KR.NO_VALOR, KR.ID_CUESTIONARIO, KBP.ID_BATERIA
		FROM IDP.K_BATERIA_PRUEBA KBP
			INNER JOIN IDP.K_PRUEBA KP ON KBP.ID_BATERIA = KP.ID_BATERIA
			INNER JOIN ADM.K_CUESTIONARIO KC ON KP.ID_CUESTIONARIO = KC.ID_CUESTIONARIO
			INNER JOIN IDP.K_RESULTADO KR ON KC.ID_CUESTIONARIO = KR.ID_CUESTIONARIO
			INNER JOIN IDP.C_VARIABLE CV ON KR.ID_VARIABLE = CV.ID_VARIABLE
		WHERE KBP.ID_BATERIA = @PIN_ID_BATERIA AND CV.CL_TIPO_VARIABLE = 3
	)
		
	INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA])
	SELECT 4 AS CL_TIPO_RESULTADO, CV2.ID_VARIABLE, [IDP].[F_OBTIENE_VALORES_BAREMOS](3, KR.NO_VALOR, NULL), @V_ID_CUESTIONARIO, @V_FE_SISTEMA, @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA
	FROM T_VARIABLES TV
		INNER JOIN IDP.C_VARIABLE CV ON TV.CL_VARIABLE_REPORTE = CV.CL_VARIABLE
		INNER JOIN T_RESULTADOS KR ON CV.ID_VARIABLE = KR.ID_VARIABLE
		INNER JOIN IDP.C_VARIABLE CV2 ON TV.CL_VARIABLE_BAREMOS = CV2.CL_VARIABLE

END


