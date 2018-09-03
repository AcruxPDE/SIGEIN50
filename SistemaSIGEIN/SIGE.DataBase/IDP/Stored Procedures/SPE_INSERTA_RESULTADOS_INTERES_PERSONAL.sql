﻿-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Gabriel Vázquez
-- CREATE date: 18/01/2016
-- Description: Variables correspondientes a las respuestas de las pruebas
-- =============================================
CREATE PROCEDURE [IDP].[SPE_INSERTA_RESULTADOS_INTERES_PERSONAL]
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

		
		IF @PIN_XML_RESULTADOS IS NULL BEGIN

			;WITH T_VARIABLES AS (
				SELECT 'INTERES-A0001' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_A1' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-A0002' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_A2' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-A0003' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_A3' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-A0004' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_A4' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-A0005' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_A5' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-A0006' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_A6' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-B0001' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_B1' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-B0002' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_B2' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-B0003' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_B3' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-B0004' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_B4' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-B0005' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_B5' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-B0006' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_B6' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-C0001' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_C1' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-C0002' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_C2' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-C0003' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_C3' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-C0004' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_C4' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-C0005' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_C5' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-C0006' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_C6' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-D0001' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_D1' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-D0002' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_D2' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-D0003' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_D3' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-D0004' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_D4' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-D0005' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_D5' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-D0006' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_D6' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-E0001' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_E1' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-E0002' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_E2' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-E0003' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_E3' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-E0004' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_E4' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-E0005' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_E5' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-E0006' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_E6' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-F0001' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_F1' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-F0002' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_F2' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-F0003' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_F3' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-F0004' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_F4' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-F0005' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_F5' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-F0006' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_F6' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-G0001' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_G1' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-G0002' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_G2' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-G0003' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_G3' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-G0004' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_G4' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-G0005' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_G5' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-G0006' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_G6' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-H0001' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_H1' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-H0002' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_H2' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-H0003' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_H3' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-H0004' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_H4' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-H0005' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_H5' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-H0006' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_H6' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-I0001' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_I1' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-I0002' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_I2' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-I0003' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_I3' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-I0004' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_I4' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-I0005' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_I5' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-I0006' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_I6' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-J0001' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_J1' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-J0002' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_J2' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-J0003' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_J3' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-J0004' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_J4' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-J0005' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_J5' AS CL_VARIABLE_RESULTADO UNION ALL
				SELECT 'INTERES-J0006' AS CL_VARIABLE_RESPUESTA, 'INTERES_RES_J6' AS CL_VARIABLE_RESULTADO
			)
				INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA])
				SELECT 2 AS CL_TIPO_RESULTADO, CV1.ID_VARIABLE, KR.NO_VALOR, @PIN_ID_CUESTIONARIO, @V_FE_SISTEMA, @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA 
				FROM T_VARIABLES TV
					INNER JOIN IDP.C_VARIABLE CV ON TV.CL_VARIABLE_RESPUESTA = CV.CL_VARIABLE
					INNER JOIN IDP.K_RESULTADO KR ON CV.ID_VARIABLE = KR.ID_VARIABLE
					INNER JOIN IDP.C_VARIABLE CV1 ON TV.CL_VARIABLE_RESULTADO = CV1.CL_VARIABLE
				WHERE KR.ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO

			/*
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 650 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 590 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 651 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 591 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 652 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 592 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 653 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 593 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 654 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 594 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 655 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 595 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 656 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 596 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 657 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 597 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 658 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 598 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 659 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 599 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 660 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 600 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 661 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 601 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 662 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 602 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 663 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 603 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 664 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 604 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 665 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 605 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 666 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 606 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 667 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 607 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 668 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 608 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 669 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 609 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 670 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 610 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 671 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 611 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 672 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 612 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 673 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 613 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 674 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 614 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 675 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 615 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 676 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 616 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 677 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 617 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 678 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 618 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 679 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 619 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 680 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 620 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 681 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 621 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 682 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 622 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 683 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 623 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 684 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 624 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 685 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 625 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 686 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 626 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 687 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 627 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 688 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 628 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 689 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 629 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 690 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 630 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 691 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 631 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 692 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 632 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 693 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 633 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 694 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 634 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 695 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 635 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 696 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 636 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 697 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 637 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 698 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 638 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 699 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 639 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 700 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 640 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 701 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 641 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 702 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 642 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 703 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 643 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 704 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 644 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 705 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 645 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 706 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 646 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 707 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 647 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 708 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 648 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			INSERT INTO [IDP].[K_RESULTADO]([CL_TIPO_RESULTADO],[ID_VARIABLE],[NO_VALOR],[ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA]) SELECT 2 AS CL_TIPO_RESULTADO, 709 AS ID_VARIABLE, NO_VALOR, ID_CUESTIONARIO, GETDATE(), @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA FROM IDP.K_RESULTADO WHERE ID_VARIABLE = 649 AND ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO
			*/
			
		
		END 
		
		IF @PIN_XML_RESULTADOS IS NOT NULL 
		BEGIN
	

			INSERT INTO [IDP].[K_RESULTADO] ([CL_TIPO_RESULTADO] ,[ID_VARIABLE] ,[NO_VALOR], [ID_CUESTIONARIO],[FE_CREACION],[CL_USUARIO_APP_CREA],[NB_PROGRAMA_CREA])
					SELECT 2, res.ID_VARIABLE, res.NO_VALOR_RESPUESTA, @PIN_ID_CUESTIONARIO, GETDATE(),@PIN_CL_USUARIO_APP,@PIN_NB_PROGRAMA
					FROM (
							SELECT
							    cv.ID_VARIABLE,
								d.value('@NO_VALOR_RESPUESTA', 'INT') AS NO_VALOR_RESPUESTA
							FROM @PIN_XML_RESULTADOS.nodes('RESPUESTAS/RESPUESTA') AS T(d)
							JOIN IDP.C_VARIABLE cv ON cv.CL_VARIABLE = d.value('@CL_PREGUNTA', 'NVARCHAR(20)')
					) AS res

		--EXEC [IDP].SPE_INSERTA_REPORTES_INTERES_PERSONAL @XML_RESULTADO OUT,@PIN_XML_RESULTADOS , @PIN_ID_CUESTIONARIO, NULL, @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA

		END
	
		EXEC [IDP].SPE_INSERTA_REPORTES_INTERES_PERSONAL @XML_RESULTADO OUT,@PIN_XML_RESULTADOS , @PIN_ID_CUESTIONARIO, NULL, @PIN_CL_USUARIO_APP, @PIN_NB_PROGRAMA


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


