﻿-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Gabriel Vázquez
-- CREATE date: 11/02/2016
-- Description: Procedimiento que manda a ejecuta todos los procedimientos que generan variables de baremos
-- =============================================
CREATE PROCEDURE [IDP].[SPE_GENERA_VARIABLES_BAREMOS]
	@XML_RESULTADO XML = '' OUT      --APLICA PARA REGRESAR UN NÚMERO 0 PARA ERROR Y 1 PARA CORRECTO
	,@PIN_ID_BATERIA AS INT
	,@PIN_CL_USUARIO_APP AS NVARCHAR(50)
	,@PIN_NB_PROGRAMA AS NVARCHAR(50)

AS
BEGIN  
	--SE DECLARA E INICIALIZA LA VARIABLE QUE NOS INDICARA SI GENERAMOS LA TRANSACCION EN ESTE SP
	DECLARE @V_EXIST_TRAN BIT = 0,
			@V_FE_SISTEMA AS DATETIME = GETDATE(),
			@V_NB_PRUEBA AS NVARCHAR(50)

		BEGIN TRY
		--SE VERIFICA SI EXISTE UNA TRANSACCION EN EJECUCION
		IF (@@TRANCOUNT = 0) 
		BEGIN
			--EN CASO DE QUE NO SE INICIALIZA LA TRANSACCION
			BEGIN TRANSACTION
			--SE EDITA LA VARIABLE QUE INDICA QUE SE INICIO LA TRANSACCION EN ESTE BLOQUE PARA CANCELARLA SI ES NECESARIO
			SET @V_EXIST_TRAN = 1
		END

			DECLARE @V_ID_CUESTIONARIO AS INT,
					@V_NO_PRUEBAS AS INT,
					@V_NO_PRUEBAS_LABORAL1 AS INT,
					@V_NO_PRUEBAS_LABORAL2 AS INT
					

			SELECT @V_ID_CUESTIONARIO = ID_CUESTIONARIO_BAREMOS
			FROM IDP.K_BATERIA_PRUEBA
			WHERE ID_BATERIA = @PIN_ID_BATERIA

			DELETE IDP.K_RESULTADO WHERE ID_CUESTIONARIO = @V_ID_CUESTIONARIO

			SET @V_NB_PRUEBA = 'LABORAL1'
			EXECUTE [IDP].[SPE_INSERTA_BAREMOS_LABORAL1] @PIN_ID_BATERIA, @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA

			SET @V_NB_PRUEBA = 'LABORAL2'
			EXECUTE [IDP].[SPE_INSERTA_BAREMOS_LABORAL2] @PIN_ID_BATERIA, @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA

			SET @V_NB_PRUEBA = 'PENSAMIENTO'
			EXECUTE [IDP].[SPE_INSERTA_BAREMOS_PENSAMIENTO] @PIN_ID_BATERIA, @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA

			SET @V_NB_PRUEBA = 'INTERES'
			EXECUTE [IDP].[SPE_INSERTA_BAREMOS_INTERES_PERSONAL] @PIN_ID_BATERIA, @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA

			SET @V_NB_PRUEBA = 'APTITUD1'
			EXECUTE [IDP].[SPE_INSERTA_BAREMOS_APTITUD1] @PIN_ID_BATERIA, @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA

			SET @V_NB_PRUEBA = 'TECNICAPC'
			EXECUTE [IDP].[SPE_INSERTA_BAREMOS_TECNICAPC] @PIN_ID_BATERIA, @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA

			-- AQUI FALTA ORTOGRAFIA Y REDACCION
			SET @V_NB_PRUEBA = 'ORTOGRAFIAS'
			EXEC [IDP].[SPE_INSERTA_BAREMOS_ORTOGRAFIAS] @PIN_ID_BATERIA, @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA


			SET @V_NB_PRUEBA = 'ADAPTACION'
			EXECUTE [IDP].[SPE_INSERTA_BAREMOS_ADAPTACION] @PIN_ID_BATERIA, @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA

			-- SI LA BATERIA TIENE LA PRUEBA DE APTITUD MENTAL 1, NO SE GENERAN LOS BAREMOS PARA APTITUD MENTAL 2
			IF (SELECT COUNT(ID_VARIABLE) FROM IDP.K_RESULTADO WHERE ID_VARIABLE IN (1277, 1278, 1279, 1280, 1281, 1282, 1283, 1284, 1285, 1286, 1287, 1288) AND ID_CUESTIONARIO = @V_ID_CUESTIONARIO) = 0 BEGIN

				SET @V_NB_PRUEBA = 'APTITUD2'
				EXECUTE [IDP].[SPE_INSERTA_BAREMOS_APTITUD2] @PIN_ID_BATERIA, @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA

			END

			SET @V_NB_PRUEBA = 'TIVA'
			EXECUTE [IDP].[SPE_INSERTA_BAREMOS_TIVA] @PIN_ID_BATERIA, @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA

			-- AQUI HAY QUE REVISAR LA SUSTITUCION DE LABORAL 1 Y 2. sigein 4.9 hace una sustitucion de variables dependiendo de algunos factores
			-- AQUI SACAMOS EL NUMERO DE PRUEBAS QUE ESTAN REALIZADAS, TIENEN QUE ESTAR LAS 32 PRUEBAS
			;WITH T_VARIABLES AS (
			SELECT 'PN-ANÁLISIS' AS CL_VARIABLE UNION ALL
			SELECT 'PN-VISIÓN' AS CL_VARIABLE UNION ALL
			SELECT 'PN-INTUICIÓN' AS CL_VARIABLE UNION ALL
			SELECT 'PN-LÓGICA' AS CL_VARIABLE UNION ALL
			SELECT 'IN-TEÓRICO' AS CL_VARIABLE UNION ALL
			SELECT 'IN-ECONÓMICO' AS CL_VARIABLE UNION ALL
			SELECT 'IN-ARTÍSTICO ESTÉTICO' AS CL_VARIABLE UNION ALL
			SELECT 'IN-SOCIAL' AS CL_VARIABLE UNION ALL
			SELECT 'IN-POLÍTICO' AS CL_VARIABLE UNION ALL
			SELECT 'IN-REGULATORIO' AS CL_VARIABLE UNION ALL
			SELECT 'AT-CULTURA GENERAL' AS CL_VARIABLE UNION ALL
			SELECT 'AT-CAPACIDAD DE JUICIO' AS CL_VARIABLE UNION ALL
			SELECT 'AT-CAPACIDAD DE ANÁLISIS Y SÍNTESIS' AS CL_VARIABLE UNION ALL
			SELECT 'AT-CAPACIDAD DE ABSTRACCIÓN' AS CL_VARIABLE UNION ALL
			SELECT 'AT-CAPACIDAD DE RAZONAMIENTO' AS CL_VARIABLE UNION ALL
			SELECT 'AT-SENTIDO COMÚN' AS CL_VARIABLE UNION ALL
			SELECT 'AT-PENSAMIENTO ORGANIZADO' AS CL_VARIABLE UNION ALL
			SELECT 'AT-CAPACIDAD DE PLANEACIÓN' AS CL_VARIABLE UNION ALL
			SELECT 'AT-CAPACIDAD DE DISCRIMINACIÓN' AS CL_VARIABLE UNION ALL
			SELECT 'AT-CAPACIDAD DE DEDUCCIÓN' AS CL_VARIABLE UNION ALL
			SELECT 'AT-INTELIGENCIA' AS CL_VARIABLE UNION ALL
			SELECT 'AT-APRENDIZAJE' AS CL_VARIABLE UNION ALL
			SELECT 'OT-REGLAS ORTOGRÁFICAS' AS CL_VARIABLE UNION ALL
			SELECT 'TP-SOFTWARE' AS CL_VARIABLE UNION ALL
			SELECT 'TP-INTERNET' AS CL_VARIABLE UNION ALL
			SELECT 'TP-HARDWARE' AS CL_VARIABLE UNION ALL
			SELECT 'TP-COMUNICACIONES' AS CL_VARIABLE UNION ALL
			SELECT 'TV-PERSONAL' AS CL_VARIABLE UNION ALL
			SELECT 'TV-LEYES Y REGLAMENTOS' AS CL_VARIABLE UNION ALL
			SELECT 'TV-INTEGRIDAD Y ÉTICA LABORAL' AS CL_VARIABLE UNION ALL
			SELECT 'TV-CÍVICA' AS CL_VARIABLE UNION ALL
			SELECT 'TV-TOTAL' AS CL_VARIABLE
			)

			SELECT @V_NO_PRUEBAS = COUNT(TV.CL_VARIABLE)
			FROM T_VARIABLES TV
				INNER JOIN IDP.C_VARIABLE CV ON TV.CL_VARIABLE = CV.CL_VARIABLE
				INNER JOIN IDP.K_RESULTADO KR ON CV.ID_VARIABLE = KR.ID_VARIABLE
			WHERE KR.ID_CUESTIONARIO = @V_ID_CUESTIONARIO

			IF @V_NO_PRUEBAS = 32 BEGIN
			-- AQUI VAMOS A HACER LA SUSTITUCION DE LAS PRUEBAS DE LABORAL EN CASO DE QUE EXISTAN

				SELECT @V_NO_PRUEBAS_LABORAL1 =  COUNT(KR.ID_RESULTADO)
				FROM IDP.K_RESULTADO KR
					INNER JOIN IDP.C_VARIABLE CV ON KR.ID_VARIABLE = CV.ID_VARIABLE
				WHERE CV.CL_VARIABLE IN ('L1-EMPUJE', 'L1-INFLUENCIA', 'L1-CONSTANCIA', 'L1-CUMPLIMIENTO') AND ID_CUESTIONARIO = @V_ID_CUESTIONARIO

				SELECT @V_NO_PRUEBAS_LABORAL2 = COUNT(KR.ID_RESULTADO)
				FROM IDP.K_RESULTADO KR
					INNER JOIN IDP.C_VARIABLE CV ON KR.ID_VARIABLE = CV.ID_VARIABLE
				WHERE CV.CL_VARIABLE IN ('L2-DA Y APOYA', 'L2-TOMA Y CONTROLA', 'L2-MANTIENE Y CONSERVA', 'L2-ADAPTA Y NEGOCIA') AND ID_CUESTIONARIO = @V_ID_CUESTIONARIO


				IF @V_NO_PRUEBAS_LABORAL1 = 0 AND @V_NO_PRUEBAS_LABORAL2 = 4 BEGIN

					;WITH T_VARIABLES AS (
						SELECT 'L1-EMPUJE'		 AS CL_VARIABLE_LABORAL1, 'L2-DA Y APOYA'			AS CL_VARIABLE_LABORAL2 UNION ALL
						SELECT 'L1-INFLUENCIA'	 AS CL_VARIABLE_LABORAL1, 'L2-TOMA Y CONTROLA'		AS CL_VARIABLE_LABORAL2 UNION ALL
						SELECT 'L1-CONSTANCIA'	 AS CL_VARIABLE_LABORAL1, 'L2-MANTIENE Y CONSERVA'	AS CL_VARIABLE_LABORAL2 UNION ALL
						SELECT 'L1-CUMPLIMIENTO' AS CL_VARIABLE_LABORAL1, 'L2-ADAPTA Y NEGOCIA'	AS CL_VARIABLE_LABORAL2)
					
					INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA])
					SELECT CV.CL_TIPO_VARIABLE, CV1.ID_VARIABLE, KR.NO_VALOR, @V_ID_CUESTIONARIO, @V_FE_SISTEMA, @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA
					FROM T_VARIABLES TV
						INNER JOIN IDP.C_VARIABLE CV ON TV.CL_VARIABLE_LABORAL2 = CV.CL_VARIABLE
						INNER JOIN IDP.K_RESULTADO KR ON CV.ID_VARIABLE = KR.ID_VARIABLE
						INNER JOIN IDP.C_VARIABLE CV1 ON TV.CL_VARIABLE_LABORAL1 = CV.CL_VARIABLE
					WHERE KR.ID_CUESTIONARIO = @V_ID_CUESTIONARIO

				END
			END



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
		DECLARE @ERROR_MENSAJE NVARCHAR(250)  = @V_NB_PRUEBA + ' ' + ERROR_MESSAGE()
		
		SET @XML_RESULTADO = DBO.F_ERROR_CREAR_ENCABEZADO( @@ROWCOUNT, @ERROR_CLAVE, 'ERROR')
		SET @XML_RESULTADO = DBO.F_ERROR_MENSAJES(@ERROR_CLAVE,@ERROR_MENSAJE)
		SET @XML_RESULTADO = DBO.F_ERROR_MENSAJES(@ERROR_CLAVE,@ERROR_MENSAJE)			
	END CATCH
END

