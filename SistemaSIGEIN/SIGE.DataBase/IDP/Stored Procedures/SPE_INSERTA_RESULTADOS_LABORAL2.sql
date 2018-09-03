﻿-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Gabriel Vázquez
-- CREATE date: 18/01/2016
-- Description: Variables correspondientes a las respuestas de las pruebas
-- =============================================
CREATE PROCEDURE [IDP].[SPE_INSERTA_RESULTADOS_LABORAL2]
	@XML_RESULTADO XML = '' OUT      --APLICA PARA REGRESAR UN NÚMERO 0 PARA ERROR Y 1 PARA CORRECTO
	,@PIN_XML_RESULTADOS AS XML
	,@PIN_ID_CUESTIONARIO AS INT 
	,@PIN_ID_PRUEBA AS INT
	,@PIN_CL_USUARIO_APP AS NVARCHAR(50)
	,@PIN_NB_PROGRAMA AS NVARCHAR(50)

AS
BEGIN  
	--SE DECLARA E INICIALIZA LA VARIABLE QUE NOS INDICARA SI GENERAMOS LA TRANSACCION EN ESTE SP
	DECLARE @V_EXIST_TRAN BIT = 0,
			@V_FE_SISTEMA AS DATETIME = GETDATE()
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
		IF @PIN_ID_CUESTIONARIO IS NULL BEGIN

			SELECT @PIN_ID_CUESTIONARIO = ID_CUESTIONARIO
			FROM IDP.K_PRUEBA
			WHERE ID_PRUEBA = @PIN_ID_PRUEBA

		END

		IF @PIN_XML_RESULTADOS IS NULL 
		BEGIN

			;WITH T_VRESPUESTA AS (
				SELECT 'LABORAL2-A-0001' AS CL_VARIABLE_RESPUESTA, 1 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0002' AS CL_VARIABLE_RESPUESTA, 2 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0003' AS CL_VARIABLE_RESPUESTA, 3 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0004' AS CL_VARIABLE_RESPUESTA, 4 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0005' AS CL_VARIABLE_RESPUESTA, 5 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0006' AS CL_VARIABLE_RESPUESTA, 6 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0007' AS CL_VARIABLE_RESPUESTA, 7 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0008' AS CL_VARIABLE_RESPUESTA, 8 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0009' AS CL_VARIABLE_RESPUESTA, 9 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0010' AS CL_VARIABLE_RESPUESTA, 10 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0011' AS CL_VARIABLE_RESPUESTA, 11 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0012' AS CL_VARIABLE_RESPUESTA, 12 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0013' AS CL_VARIABLE_RESPUESTA, 13 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0014' AS CL_VARIABLE_RESPUESTA, 14 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0015' AS CL_VARIABLE_RESPUESTA, 15 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0016' AS CL_VARIABLE_RESPUESTA, 16 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0017' AS CL_VARIABLE_RESPUESTA, 17 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0018' AS CL_VARIABLE_RESPUESTA, 18 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0019' AS CL_VARIABLE_RESPUESTA, 19 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0020' AS CL_VARIABLE_RESPUESTA, 20 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0021' AS CL_VARIABLE_RESPUESTA, 21 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0022' AS CL_VARIABLE_RESPUESTA, 22 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0023' AS CL_VARIABLE_RESPUESTA, 23 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0024' AS CL_VARIABLE_RESPUESTA, 24 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0025' AS CL_VARIABLE_RESPUESTA, 1 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0026' AS CL_VARIABLE_RESPUESTA, 2 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0027' AS CL_VARIABLE_RESPUESTA, 3 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0028' AS CL_VARIABLE_RESPUESTA, 4 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0029' AS CL_VARIABLE_RESPUESTA, 5 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0030' AS CL_VARIABLE_RESPUESTA, 6 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0031' AS CL_VARIABLE_RESPUESTA, 7 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0032' AS CL_VARIABLE_RESPUESTA, 8 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0033' AS CL_VARIABLE_RESPUESTA, 9 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0034' AS CL_VARIABLE_RESPUESTA, 10 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0035' AS CL_VARIABLE_RESPUESTA, 11 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0036' AS CL_VARIABLE_RESPUESTA, 12 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0037' AS CL_VARIABLE_RESPUESTA, 13 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0038' AS CL_VARIABLE_RESPUESTA, 14 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0039' AS CL_VARIABLE_RESPUESTA, 15 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0040' AS CL_VARIABLE_RESPUESTA, 16 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0041' AS CL_VARIABLE_RESPUESTA, 17 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0042' AS CL_VARIABLE_RESPUESTA, 18 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0043' AS CL_VARIABLE_RESPUESTA, 19 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0044' AS CL_VARIABLE_RESPUESTA, 20 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0045' AS CL_VARIABLE_RESPUESTA, 21 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0046' AS CL_VARIABLE_RESPUESTA, 22 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0047' AS CL_VARIABLE_RESPUESTA, 23 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0048' AS CL_VARIABLE_RESPUESTA, 24 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0049' AS CL_VARIABLE_RESPUESTA, 1 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0050' AS CL_VARIABLE_RESPUESTA, 2 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0051' AS CL_VARIABLE_RESPUESTA, 3 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0052' AS CL_VARIABLE_RESPUESTA, 4 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0053' AS CL_VARIABLE_RESPUESTA, 5 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0054' AS CL_VARIABLE_RESPUESTA, 6 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0055' AS CL_VARIABLE_RESPUESTA, 7 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0056' AS CL_VARIABLE_RESPUESTA, 8 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0057' AS CL_VARIABLE_RESPUESTA, 9 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0058' AS CL_VARIABLE_RESPUESTA, 10 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0059' AS CL_VARIABLE_RESPUESTA, 11 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0060' AS CL_VARIABLE_RESPUESTA, 12 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0061' AS CL_VARIABLE_RESPUESTA, 13 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0062' AS CL_VARIABLE_RESPUESTA, 14 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0063' AS CL_VARIABLE_RESPUESTA, 15 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0064' AS CL_VARIABLE_RESPUESTA, 16 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0065' AS CL_VARIABLE_RESPUESTA, 17 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0066' AS CL_VARIABLE_RESPUESTA, 18 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0067' AS CL_VARIABLE_RESPUESTA, 19 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0068' AS CL_VARIABLE_RESPUESTA, 20 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0069' AS CL_VARIABLE_RESPUESTA, 21 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0070' AS CL_VARIABLE_RESPUESTA, 22 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0071' AS CL_VARIABLE_RESPUESTA, 23 AS NO_GRUPO UNION ALL
				SELECT 'LABORAL2-A-0072' AS CL_VARIABLE_RESPUESTA, 24 AS NO_GRUPO
			), T_VRESULTADO AS (
				SELECT 'LABORAL2-RES-0001' AS CL_VARIABLE_RESPUESTA, 1 AS NO_VARIABLE UNION ALL
				SELECT 'LABORAL2-RES-0002' AS CL_VARIABLE_RESPUESTA, 2 AS NO_VARIABLE UNION ALL
				SELECT 'LABORAL2-RES-0003' AS CL_VARIABLE_RESPUESTA, 3 AS NO_VARIABLE UNION ALL
				SELECT 'LABORAL2-RES-0004' AS CL_VARIABLE_RESPUESTA, 4 AS NO_VARIABLE UNION ALL
				SELECT 'LABORAL2-RES-0005' AS CL_VARIABLE_RESPUESTA, 5 AS NO_VARIABLE UNION ALL
				SELECT 'LABORAL2-RES-0006' AS CL_VARIABLE_RESPUESTA, 6 AS NO_VARIABLE UNION ALL
				SELECT 'LABORAL2-RES-0007' AS CL_VARIABLE_RESPUESTA, 7 AS NO_VARIABLE UNION ALL
				SELECT 'LABORAL2-RES-0008' AS CL_VARIABLE_RESPUESTA, 8 AS NO_VARIABLE UNION ALL
				SELECT 'LABORAL2-RES-0009' AS CL_VARIABLE_RESPUESTA, 9 AS NO_VARIABLE UNION ALL
				SELECT 'LABORAL2-RES-0010' AS CL_VARIABLE_RESPUESTA, 10 AS NO_VARIABLE UNION ALL
				SELECT 'LABORAL2-RES-0011' AS CL_VARIABLE_RESPUESTA, 11 AS NO_VARIABLE UNION ALL
				SELECT 'LABORAL2-RES-0012' AS CL_VARIABLE_RESPUESTA, 12 AS NO_VARIABLE UNION ALL
				SELECT 'LABORAL2-RES-0013' AS CL_VARIABLE_RESPUESTA, 13 AS NO_VARIABLE UNION ALL
				SELECT 'LABORAL2-RES-0014' AS CL_VARIABLE_RESPUESTA, 14 AS NO_VARIABLE UNION ALL
				SELECT 'LABORAL2-RES-0015' AS CL_VARIABLE_RESPUESTA, 15 AS NO_VARIABLE UNION ALL
				SELECT 'LABORAL2-RES-0016' AS CL_VARIABLE_RESPUESTA, 16 AS NO_VARIABLE UNION ALL
				SELECT 'LABORAL2-RES-0017' AS CL_VARIABLE_RESPUESTA, 17 AS NO_VARIABLE UNION ALL
				SELECT 'LABORAL2-RES-0018' AS CL_VARIABLE_RESPUESTA, 18 AS NO_VARIABLE UNION ALL
				SELECT 'LABORAL2-RES-0019' AS CL_VARIABLE_RESPUESTA, 19 AS NO_VARIABLE UNION ALL
				SELECT 'LABORAL2-RES-0020' AS CL_VARIABLE_RESPUESTA, 20 AS NO_VARIABLE UNION ALL
				SELECT 'LABORAL2-RES-0021' AS CL_VARIABLE_RESPUESTA, 21 AS NO_VARIABLE UNION ALL
				SELECT 'LABORAL2-RES-0022' AS CL_VARIABLE_RESPUESTA, 22 AS NO_VARIABLE UNION ALL
				SELECT 'LABORAL2-RES-0023' AS CL_VARIABLE_RESPUESTA, 23 AS NO_VARIABLE UNION ALL
				SELECT 'LABORAL2-RES-0024' AS CL_VARIABLE_RESPUESTA, 24 AS NO_VARIABLE
			), T_VG AS (
				SELECT TVR.NO_GRUPO, ISNULL(SUM(KR.NO_VALOR),0) AS NO_VALOR
				FROM T_VRESPUESTA TVR
					INNER JOIN IDP.C_VARIABLE CV ON TVR.CL_VARIABLE_RESPUESTA = CV.CL_VARIABLE
					INNER JOIN IDP.K_RESULTADO KR ON CV.ID_VARIABLE = KR.ID_VARIABLE
				WHERE ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
				GROUP BY TVR.NO_GRUPO
			)
				INSERT INTO [IDP].[K_RESULTADO] ([CL_TIPO_RESULTADO] ,[ID_VARIABLE] ,[NO_VALOR], [ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA])
				SELECT 2 AS CL_TIPO_RESULTADO, CV.ID_VARIABLE, TVG.NO_VALOR, @PIN_ID_CUESTIONARIO, @V_FE_SISTEMA, @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA
				FROM T_VRESULTADO TVR
					INNER JOIN IDP.C_VARIABLE CV ON TVR.CL_VARIABLE_RESPUESTA = CV.CL_VARIABLE
					INNER JOIN T_VG TVG ON TVR.NO_VARIABLE = TVG.NO_GRUPO

		END 
		
		IF @PIN_XML_RESULTADOS IS NOT NULL 
		BEGIN

			INSERT INTO [IDP].[K_RESULTADO] ([CL_TIPO_RESULTADO] ,[ID_VARIABLE] ,[NO_VALOR], [ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA])
					SELECT 2, res.ID_VARIABLE, res.NO_VALOR_RESPUESTA, @PIN_ID_CUESTIONARIO, GETDATE(),@PIN_CL_USUARIO_APP,@PIN_NB_PROGRAMA
					FROM (
							SELECT
								d.value('@ID_VARIABLE', 'INT') AS ID_VARIABLE,
								d.value('@NO_VALOR', 'INT') AS NO_VALOR_RESPUESTA
							FROM @PIN_XML_RESULTADOS.nodes('RESULTADOS/RESULTADO') AS T(d)
					) AS res

				

		END

		EXEC [IDP].[SPE_INSERTA_REPORTE_LABORAL2]  @XML_RESULTADO, @PIN_ID_CUESTIONARIO, @PIN_CL_USUARIO_APP , @PIN_NB_PROGRAMA

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


