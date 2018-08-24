﻿-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Juan De Dios Pérez Pedroza
-- CREATE date: 18/01/2016
-- Description: Variables correspondientes a las respuestas de las pruebas
-- =============================================
CREATE PROCEDURE [IDP].[SPE_INSERTA_RESULTADOS_INGLES]
	@XML_RESULTADO XML = '' OUT      --APLICA PARA REGRESAR UN NÚMERO 0 PARA ERROR Y 1 PARA CORRECTO
	,@PIN_XML_RESULTADOS AS XML
	,@PIN_ID_CUESTIONARIO AS INT 
	,@PIN_ID_PRUEBA AS INT
	,@PIN_NB_PRUEBA AS NVARCHAR(20)
	,@PIN_CL_USUARIO_APP AS NVARCHAR(50)
	,@PIN_NB_PROGRAMA AS NVARCHAR(50)

AS
BEGIN  
	--SE DECLARA E INICIALIZA LA VARIABLE QUE NOS INDICARA SI GENERAMOS LA TRANSACCION EN ESTE SP
	DECLARE @V_EXIST_TRAN BIT = 0,
	        @RESULTADOS_RESPUESTAS INT =0
    	BEGIN TRY
		--SE VERIFICA SI EXISTE UNA TRANSACCION EN EJECUCION
		IF (@@TRANCOUNT = 0) 
		BEGIN
			--EN CASO DE QUE NO SE INICIALIZA LA TRANSACCION
			BEGIN TRANSACTION
			--SE EDITA LA VARIABLE QUE INDICA QUE SE INICIO LA TRANSACCION EN ESTE BLOQUE PARA CANCELARLA SI ES NECESARIO
			SET @V_EXIST_TRAN = 1
		END	
		--SE VERIFICA SI SE INSERTA EL REGISTRO O SE ACTUALIZARA SEGUN LA VARIABLE DE TIPO DE TRANSACCION  QUE RECIBE EL SP
	

		IF @PIN_ID_CUESTIONARIO IS NULL 
		BEGIN

		SELECT @PIN_ID_CUESTIONARIO = ID_CUESTIONARIO FROM IDP.K_PRUEBA WHERE ID_PRUEBA = @PIN_ID_PRUEBA
			--INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 865 AS ID_VARIABLE, SUM(NO_VALOR), ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE BETWEEN 869 AND 898 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO GROUP BY ID_CUESTIONARIO
			--INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 866 AS ID_VARIABLE, SUM(NO_VALOR), ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE BETWEEN 899 AND 928 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO GROUP BY ID_CUESTIONARIO
			--INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 867 AS ID_VARIABLE, SUM(NO_VALOR), ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE BETWEEN 929 AND 958 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO GROUP BY ID_CUESTIONARIO
			--INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 868 AS ID_VARIABLE, SUM(NO_VALOR), ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE BETWEEN 959 AND 988 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO GROUP BY ID_CUESTIONARIO
				INSERT INTO [IDP].[K_RESULTADO] ([CL_TIPO_RESULTADO] ,[ID_VARIABLE] ,[NO_VALOR], [ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA])
					SELECT 2, res.ID_VARIABLE, res.NO_VALOR_RESPUESTA, res.ID_CUESTIONARIO, GETDATE(),@PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA
					FROM (
							SELECT
							    KP.ID_CUESTIONARIO,
								d.value('@ID_VARIABLE', 'INT') AS ID_VARIABLE,
								d.value('@NO_VALOR', 'INT') AS NO_VALOR_RESPUESTA
							FROM @PIN_XML_RESULTADOS.nodes('RESULTADOS/RESULTADO') AS T(d)
							JOIN IDP.K_PRUEBA KP ON KP.ID_PRUEBA = d.value('@ID_PRUEBA', 'INT')
					) AS res

					
							-------------------------------ACTUALIZA EL ESTADO DE LA PRUEBA A TERMINADA-----------------------------------
				  UPDATE KP SET
				  KP.[CL_ESTADO] ='TERMINADO',
				  KP.[FE_TERMINO] =GETDATE()
				, [FE_MODIFICACION] = GETDATE()
				, [CL_USUARIO_APP_MODIFICA] = 'sqlserver'
				, [NB_PROGRAMA_MODIFICA] = 'Ingles manual'
			     FROM IDP.K_PRUEBA KP
				 INNER JOIN @PIN_XML_RESULTADOS.nodes('/RESULTADOS/RESULTADO') tbl(COLUMNA) ON
				  COLUMNA.value('@ID_PRUEBA', 'INT') = KP.ID_PRUEBA;

				EXEC [IDP].[SPE_INSERTA_REPORTES_INGLES] @XML_RESULTADO,@PIN_ID_PRUEBA,@PIN_ID_CUESTIONARIO,NULL,@PIN_CL_USUARIO_APP,@PIN_NB_PROGRAMA
			
		END 
		
		IF @PIN_ID_CUESTIONARIO IS NOT NULL 
		BEGIN
		 SELECT @RESULTADOS_RESPUESTAS =	
							SUM(COLUMNA.value('@NO_VALOR_RESPUESTA', 'INT'))
							FROM @PIN_XML_RESULTADOS.nodes('/RESPUESTAS/RESPUESTA') tbl(COLUMNA)
		---------------------------------------------------------------------------------------------------
			IF @PIN_NB_PRUEBA = 'INGLES-1'
			BEGIN 
			DELETE FROM [IDP].[K_RESULTADO]  WHERE ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO AND CL_TIPO_RESULTADO =2
						INSERT INTO 
						[IDP].[K_RESULTADO] 
						([CL_TIPO_RESULTADO] ,
						[ID_VARIABLE] ,
						[NO_VALOR],
						[ID_CUESTIONARIO],
						[FE_CREACION],
						[CL_USUARIO_APP_CREA],
						[NB_PROGRAMA_CREA])
						VALUES 
						(2,
						 865,--INGLES_RES_S1
						 @RESULTADOS_RESPUESTAS,
						 @PIN_ID_CUESTIONARIO,
						 GETDATE(),
						@PIN_CL_USUARIO_APP,
						@PIN_NB_PROGRAMA
						)
						EXEC [IDP].[SPE_INSERTA_REPORTES_INGLES] @XML_RESULTADO,NULL,@PIN_ID_CUESTIONARIO,@PIN_NB_PRUEBA,@PIN_CL_USUARIO_APP,@PIN_NB_PROGRAMA


			END
		---------------------------------------------------------------------------------------------------
		
		 IF @PIN_NB_PRUEBA = 'INGLES-2'
			BEGIN 
				DELETE FROM [IDP].[K_RESULTADO]  WHERE ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO AND CL_TIPO_RESULTADO =2
						INSERT INTO 
						[IDP].[K_RESULTADO] 
						([CL_TIPO_RESULTADO] ,
						[ID_VARIABLE] ,
						[NO_VALOR],
						[ID_CUESTIONARIO],
						[FE_CREACION],
						[CL_USUARIO_APP_CREA],
						[NB_PROGRAMA_CREA])
						VALUES 
						(2,
						 866,--INGLES_RES_S1
						 @RESULTADOS_RESPUESTAS,
						 @PIN_ID_CUESTIONARIO,
						 GETDATE(),
						@PIN_CL_USUARIO_APP,
						@PIN_NB_PROGRAMA
						)
						EXEC [IDP].[SPE_INSERTA_REPORTES_INGLES] @XML_RESULTADO,NULL,@PIN_ID_CUESTIONARIO,@PIN_NB_PRUEBA,@PIN_CL_USUARIO_APP,@PIN_NB_PROGRAMA

			END
		---------------------------------------------------------------------------------------------------
				
		 IF @PIN_NB_PRUEBA = 'INGLES-3'
			BEGIN 
				DELETE FROM [IDP].[K_RESULTADO]  WHERE ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO AND CL_TIPO_RESULTADO =2
						INSERT INTO 
						[IDP].[K_RESULTADO] 
						([CL_TIPO_RESULTADO] ,
						[ID_VARIABLE] ,
						[NO_VALOR],
						[ID_CUESTIONARIO],
						[FE_CREACION],
						[CL_USUARIO_APP_CREA],
						[NB_PROGRAMA_CREA])
						VALUES 
						(2,
						 867, --INGLES_RES_S1
						 @RESULTADOS_RESPUESTAS,
						 @PIN_ID_CUESTIONARIO,
						 GETDATE(),
						@PIN_CL_USUARIO_APP,
						@PIN_NB_PROGRAMA
						)

						EXEC [IDP].[SPE_INSERTA_REPORTES_INGLES] @XML_RESULTADO,NULL,@PIN_ID_CUESTIONARIO,@PIN_NB_PRUEBA,@PIN_CL_USUARIO_APP,@PIN_NB_PROGRAMA

			END
		---------------------------------------------------------------------------------------------------
				
			 IF @PIN_NB_PRUEBA = 'INGLES-4'
			BEGIN 
				DELETE FROM [IDP].[K_RESULTADO]  WHERE ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO AND CL_TIPO_RESULTADO =2
						INSERT INTO 
						[IDP].[K_RESULTADO] 
						([CL_TIPO_RESULTADO] ,
						[ID_VARIABLE] ,
						[NO_VALOR],
						[ID_CUESTIONARIO],
						[FE_CREACION],
						[CL_USUARIO_APP_CREA],
						[NB_PROGRAMA_CREA])
						VALUES 
						(2,
						 868,--INGLES_RES_S1
						 @RESULTADOS_RESPUESTAS,
						 @PIN_ID_CUESTIONARIO,
						 GETDATE(),
						@PIN_CL_USUARIO_APP,
						@PIN_NB_PROGRAMA
						)

						EXEC [IDP].[SPE_INSERTA_REPORTES_INGLES] @XML_RESULTADO,NULL,@PIN_ID_CUESTIONARIO,@PIN_NB_PRUEBA,@PIN_CL_USUARIO_APP,@PIN_NB_PROGRAMA
			END
		---------------------------------------------------------------------------------------------------
		
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
		DECLARE @ERROR_MENSAJE NVARCHAR(250)  = ERROR_MESSAGE()
		
		SET @XML_RESULTADO = DBO.F_ERROR_CREAR_ENCABEZADO( @@ROWCOUNT, @ERROR_CLAVE, 'ERROR')
		SET @XML_RESULTADO = DBO.F_ERROR_MENSAJES(@ERROR_CLAVE,@ERROR_MENSAJE)
		SET @XML_RESULTADO = DBO.F_ERROR_MENSAJES(@ERROR_CLAVE,@ERROR_MENSAJE)			
	END CATCH
END

