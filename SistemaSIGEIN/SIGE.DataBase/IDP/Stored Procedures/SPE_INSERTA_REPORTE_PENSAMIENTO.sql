﻿-- =============================================
-- Proyecto: SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Gabriel Vázquez
-- CRETAE date: 30/01/2016
-- Description: Inserta las variables de tipo 3 de la prueba de estilo de pensamiento
-- =============================================
CREATE PROCEDURE [IDP].[SPE_INSERTA_REPORTE_PENSAMIENTO]
	 @XML_RESULTADO AS XML = '' OUT
	,@PIN_ID_CUESTIONARIO AS INT
	,@PIN_CL_USUARIO_APP AS NVARCHAR(50)
	,@PIN_NB_PROGRAMA AS NVARCHAR(50)
AS
BEGIN
	--SE DECLARA E INICIALIZA LA VARIABLE QUE NOS INDICARA SI GENERAMOS LA TRANSACCION EN ESTE SP
	DECLARE @V_EXIST_TRAN BIT = 0,
			@V_FE_TODAY DATETIME = GETDATE()
		BEGIN TRY
		--SE VERIFICA SI EXISTE UNA TRANSACCION EN EJECUCION
		IF (@@TRANCOUNT = 0) 
		BEGIN
			--EN CASO DE QUE NO SE INICIALIZA LA TRANSACCION
			BEGIN TRANSACTION
			--SE EDITA LA VARIABLE QUE INDICA QUE SE INICIO LA TRANSACCION EN ESTE BLOQUE PARA CANCELARLA SI ES NECESARIO
			SET @V_EXIST_TRAN = 1
		END

		DECLARE @V_AS AS DECIMAL(13,3),
				@V_LS AS DECIMAL(13,3),
				@V_IS AS DECIMAL(13,3),
				@V_VS AS DECIMAL(13,3) 


		DECLARE @V_AN AS DECIMAL(13,3),
				@V_LN AS DECIMAL(13,3),
				@V_IN AS DECIMAL(13,3),
				@V_VN AS DECIMAL(13,3) 

		DECLARE @T_RESULTADOS TABLE(
			CL_VARIABLE_REPORTE NVARCHAR(50),
			NO_VALOR DECIMAL(13,3)
		)
		/*
		SELECT @PIN_ID_CUESTIONARIO = ID_CUESTIONARIO
		FROM IDP.K_PRUEBA
		WHERE ID_BATERIA = @V_ID_BATERIA
		*/

		DELETE K_RESULTADO WHERE ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO AND CL_TIPO_RESULTADO = 3

		SET @V_AS = (([IDP].[F_OBTIENE_VALORES_ESTILO_PENSAMIENTO](@PIN_ID_CUESTIONARIO, 'A2'))  + ([IDP].[F_OBTIENE_VALORES_ESTILO_PENSAMIENTO]/*A*/(@PIN_ID_CUESTIONARIO, 'A3')) + ([IDP].[F_OBTIENE_VALORES_ESTILO_PENSAMIENTO]/*A*/(@PIN_ID_CUESTIONARIO, 'A5'))) / CAST(10 AS DECIMAL(13,3))
		SET @V_LS = (([IDP].[F_OBTIENE_VALORES_ESTILO_PENSAMIENTO](@PIN_ID_CUESTIONARIO, 'L2'))  + ([IDP].[F_OBTIENE_VALORES_ESTILO_PENSAMIENTO]/*L*/(@PIN_ID_CUESTIONARIO, 'L3')) + ([IDP].[F_OBTIENE_VALORES_ESTILO_PENSAMIENTO]/*L*/(@PIN_ID_CUESTIONARIO, 'L5'))) / CAST(10 AS DECIMAL(13,3))
		SET @V_IS = (([IDP].[F_OBTIENE_VALORES_ESTILO_PENSAMIENTO](@PIN_ID_CUESTIONARIO, 'I2'))  + ([IDP].[F_OBTIENE_VALORES_ESTILO_PENSAMIENTO]/*I*/(@PIN_ID_CUESTIONARIO, 'I3')) + ([IDP].[F_OBTIENE_VALORES_ESTILO_PENSAMIENTO]/*I*/(@PIN_ID_CUESTIONARIO, 'I5'))) / CAST(10 AS DECIMAL(13,3))
		SET @V_VS = (([IDP].[F_OBTIENE_VALORES_ESTILO_PENSAMIENTO](@PIN_ID_CUESTIONARIO, 'V2'))  + ([IDP].[F_OBTIENE_VALORES_ESTILO_PENSAMIENTO]/*V*/(@PIN_ID_CUESTIONARIO, 'V3')) + ([IDP].[F_OBTIENE_VALORES_ESTILO_PENSAMIENTO]/*V*/(@PIN_ID_CUESTIONARIO, 'V5'))) / CAST(10 AS DECIMAL(13,3))

		--SELECT @V_AS, @V_LS, @V_IS, @V_VS

		SET @V_AN = ((@V_AS - 5) * 5)
		SET @V_LN = ((@V_LS - 5) * 5)
		SET @V_IN = ((@V_IS - 5) * 5)
		SET @V_VN = ((@V_VS - 5) * 5)

		--SELECT @V_AN, @V_LN, @V_IN, @V_VN

		INSERT INTO @T_RESULTADOS VALUES('PENSAMIENTO_REP_A', @V_AN)
		INSERT INTO @T_RESULTADOS VALUES('PENSAMIENTO_REP_L', @V_LN)
		INSERT INTO @T_RESULTADOS VALUES('PENSAMIENTO_REP_I', @V_IN)
		INSERT INTO @T_RESULTADOS VALUES('PENSAMIENTO_REP_V', @V_VN)

		INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA])
		SELECT 3 AS CL_TIPO_RESULTADO, v.ID_VARIABLE, r.NO_VALOR, @PIN_ID_CUESTIONARIO, @V_FE_TODAY, @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA
		FROM @T_RESULTADOS r
			INNER JOIN IDP.C_VARIABLE v ON r.CL_VARIABLE_REPORTE = v.CL_VARIABLE


		--SE DEVUELVE LA VARIABLE DE RETORNO INDICANDO QUE TODO SE REALIZO CORRECTAMENTE
		SET @XML_RESULTADO = DBO.F_ERROR_CREAR_ENCABEZADO( @@ROWCOUNT, 1, 'SUCCESSFUL')
		SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_MENSAJES(@XML_RESULTADO, 'Proceso exitoso', 'ES')
		SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_MENSAJES(@XML_RESULTADO, 'Successful Process', 'EN')
		--SI SE GENERO UNA TRANSACCION EN ESTE BLOQUE LA TERMINARA
		IF (@@TRANCOUNT > 0 AND @V_EXIST_TRAN = 1)
			COMMIT
	END TRY
	BEGIN CATCH
		--SI OCURRIO UN ERROR Y SE INICIO UNA TRANSACCION ENE ESTE BLOQUE SE CANCELARA LA TRANSACCION
		IF (@@TRANCOUNT > 0 AND @V_EXIST_TRAN = 1)
			ROLLBACK
		--SE INDICA EN LA VARIABLE DE RETORNO QUE OCURRIO UN ERROR
		--SET @POUT_CLAVE_RETORNO = 0
		--SE INSERTA EL ERROR EN LA TABLA
		DECLARE @ERROR_CLAVE INT  = ERROR_NUMBER()
		DECLARE @ERROR_MENSAJE NVARCHAR(250)  = ERROR_MESSAGE()
		
		SET @XML_RESULTADO = DBO.F_ERROR_CREAR_ENCABEZADO( @@ROWCOUNT, @ERROR_CLAVE, 'ERROR')
		SET @XML_RESULTADO = DBO.F_ERROR_MENSAJES(@ERROR_CLAVE,@ERROR_MENSAJE)
		SET @XML_RESULTADO = DBO.F_ERROR_MENSAJES(@ERROR_CLAVE,@ERROR_MENSAJE)			
	END CATCH
END