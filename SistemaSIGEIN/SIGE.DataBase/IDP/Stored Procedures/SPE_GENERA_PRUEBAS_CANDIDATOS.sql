﻿-- =============================================
-- Proyecto: Sistema SIGEIN  5.0
-- Copyright (c) - Acrux - 2015
-- Author: Juan Castillo
-- CRETAE date: 14/01/2015
-- Description: Inserta las pruebas a los candidatos
-- =============================================
-- =============================================
CREATE PROCEDURE [IDP].[SPE_GENERA_PRUEBAS_CANDIDATOS] 
		  @XML_RESULTADO XML = '' OUT      --APLICA PARA REGRESAR UN NÚMERO 0 PARA ERROR Y 1 PARA CORRECTO    	
		, @PIN_XML_CANDIDATOS AS xml
		, @PIN_XML_PRUEBAS AS xml		
		, @PIN_CL_USUARIO AS nvarchar(50)
		, @PIN_NB_PROGRAMA_CREA AS nvarchar(50)	

AS 
BEGIN  
	--SE DECLARA E INICIALIZA LA VARIABLE QUE NOS INDICARA SI GENERAMOS LA TRANSACCION EN ESTE SP
	DECLARE @V_EXIST_TRAN BIT = 0,
	@FOLIOS NVARCHAR(500);
	DECLARE  @TCANDIDATOS TABLE (
		ID_KEY int,
		ID_CANDIDATO INT
	); 
	DECLARE  @TPRUEBAS TABLE (
		ID_KEY int,
		ID_PRUEBA_PLANTILLA INT,
        CL_PRUEBA nvarchar(20),
        CL_ESTADO nvarchar(20)
	); 

    	BEGIN TRY
		--SE VERIFICA SI EXISTE UNA TRANSACCION EN EJECUCION
		IF (@@TRANCOUNT = 0) 
		BEGIN
			--EN CASO DE QUE NO SE INICIALIZA LA TRANSACCION
			BEGIN TRANSACTION
			--SE EDITA LA VARIABLE QUE INDICA QUE SE INICIO LA TRANSACCION EN ESTE BLOQUE PARA CANCELARLA SI ES NECESARIO
			SET @V_EXIST_TRAN = 1
		END
		PRINT 'INICIO-----------';
			--INSERTAR LOS REGISTROS DE CANDIDATOS EN TABLA TEMPORAL
			INSERT INTO @TCANDIDATOS (ID_KEY,ID_CANDIDATO)
			SELECT ROW_NUMBER() OVER (ORDER BY COLUMNA.value('@ID_CANDIDATO', 'INT') DESC) as ID_KEY, COLUMNA.value('@ID_CANDIDATO', 'INT')
			FROM @PIN_XML_CANDIDATOS.nodes('/CANDIDATOS/CANDIDATO') tbl(COLUMNA);

			--INSERTAR LOS REGISTROS DE PRUEBAS EN TABLA TEMPORAL
			INSERT INTO @TPRUEBAS (ID_KEY,ID_PRUEBA_PLANTILLA,CL_PRUEBA,CL_ESTADO)
			SELECT ROW_NUMBER() OVER (ORDER BY COLUMNA.value('@ID_PRUEBA_PLANTILLA', 'INT') DESC) as ID_KEY,
				 COLUMNA.value('@ID_PRUEBA_PLANTILLA', 'INT'),
				 COLUMNA.value('@CL_PRUEBA', 'NVARCHAR(20)'),
				 COLUMNA.value('@CL_ESTADO', 'NVARCHAR(20)')
			FROM @PIN_XML_PRUEBAS.nodes('/PRUEBAS/PRUEBA') tbl(COLUMNA);

			DECLARE @V_ID_CANDIDATO int,
			@V_ID_PRUEBA_PLANTILLA int,
			@V_ID_PRUEBA int,
			@V_ID_CUESTIONARIO int,
			@V_ID_BATERIA int,
			@V_NO_TIEMPO_PRUEBA int,
			@i int=1,
			@j int=1,
			@V_CL_ESTADO NVARCHAR(20),
			@V_CLAVE VARCHAR(40),
			@V_MENSAJE VARCHAR(200),
			@V_FL_BATERIA VARCHAR(30);
			
			--LOOP DE LA TABLA TEMPORAL DE LOS CANDIDATOS
			WHILE ((SELECT count(*) FROM   @TCANDIDATOS) >= @i)
			begin 
				 Select Top 1 @V_ID_CANDIDATO = ID_CANDIDATO From @TCANDIDATOS WHERE ID_KEY=@i;
				
				--GENERAR BATERIA POR CANDIDATO
				EXEC ADM.SP_OBTIENE_SECUENCIA @V_FL_BATERIA OUT, @V_CLAVE OUT, @V_MENSAJE OUT, 'K_BATERIA_PRUEBA', NULL
				
				INSERT INTO IDP.K_BATERIA_PRUEBA (ID_CANDIDATO,FL_BATERIA,CL_TOKEN,FE_CREACION,CL_USUARIO_APP_CREA,NB_PROGRAMA_CREA)
				VALUES(@V_ID_CANDIDATO,@V_FL_BATERIA,NEWID(),GETDATE(),@PIN_CL_USUARIO,@PIN_NB_PROGRAMA_CREA);	

					SET @V_ID_BATERIA = SCOPE_IDENTITY();
					SET @j= 1;

				--SE AGREGA EL INSERT A LA TABALA K_CUESTIONARIO PARA INSERTAR EL CUESTIONARIO DE BAREMOS, Y TAMBIEN SE ACTUALIZA K_BATERIA_PRUEBA PARA PONER EL ID DEL CUESTIONARIO
				INSERT INTO ADM.K_CUESTIONARIO (ID_EVALUADO,FE_CREACION,CL_USUARIO_APP_CREA,NB_PROGRAMA_CREA)
					 VALUES(@V_ID_CANDIDATO,GETDATE(),@PIN_CL_USUARIO,@PIN_NB_PROGRAMA_CREA);

				SET @V_ID_CUESTIONARIO = SCOPE_IDENTITY();

				UPDATE IDP.K_BATERIA_PRUEBA SET ID_CUESTIONARIO_BAREMOS = @V_ID_CUESTIONARIO WHERE ID_BATERIA = @V_ID_BATERIA


				--LOOP DE LA TABLA TEMPORAL DE LAS PRUEBAS
				WHILE ((SELECT count(*) FROM   @TPRUEBAS) >= @j)
				begin 
					 Select Top 1 @V_ID_PRUEBA_PLANTILLA = ID_PRUEBA_PLANTILLA,
								 @V_CL_ESTADO = CL_ESTADO
					 From @TPRUEBAS WHERE ID_KEY=@j;
					 --INSERTAR EL CUESTIONARIO AL CANDIDATO
					 SELECT @V_NO_TIEMPO_PRUEBA = NO_TIEMPO_PRUEBA 
					 FROM IDP.C_PRUEBA CPR WHERE ID_PRUEBA=@V_ID_PRUEBA_PLANTILLA;

					 INSERT INTO ADM.K_CUESTIONARIO (ID_EVALUADO,FE_CREACION,CL_USUARIO_APP_CREA,NB_PROGRAMA_CREA)
					 VALUES(@V_ID_CANDIDATO,GETDATE(),@PIN_CL_USUARIO,@PIN_NB_PROGRAMA_CREA);
					 

					 SET @V_ID_CUESTIONARIO = SCOPE_IDENTITY();

					 --INSERTAR LA PRUEBA AL CANDIDATO
						INSERT INTO IDP.K_PRUEBA
								   (
									  [ID_CANDIDATO]
									, [ID_PRUEBA_PLANTILLA]
									, [ID_CUESTIONARIO]
									, [CL_TOKEN_EXTERNO]
									, [CL_ESTADO]									
									, [NO_TIEMPO]
									, [ID_BATERIA]
									, [FE_CREACION]
									, [CL_USUARIO_APP_CREA]
									, [NB_PROGRAMA_CREA]
								)
							VALUES( @V_ID_CANDIDATO								    
									, @V_ID_PRUEBA_PLANTILLA
									, @V_ID_CUESTIONARIO
									,NEWID()
									, @V_CL_ESTADO
									, @V_NO_TIEMPO_PRUEBA
									, @V_ID_BATERIA
									,  GETDATE()
									, @PIN_CL_USUARIO
									, @PIN_NB_PROGRAMA_CREA);
					--INSERTAR K_PRUEBAS_SECCION
					--SELECT * FROM IDP.K_PRUEBA_SECCION;
					--SELECT * FROM IDP.C_PRUEBA_SECCION;

					SET @V_ID_PRUEBA = SCOPE_IDENTITY();

					INSERT INTO IDP.K_PRUEBA_SECCION (ID_PRUEBA,CL_PRUEBA_SECCION,NB_PRUEBA_SECCION,NO_TIEMPO,CL_ESTADO,FE_CREACION,CL_USUARIO_APP_CREA,NB_PROGRAMA_CREA)
					 SELECT @V_ID_PRUEBA,CL_PRUEBA_SECCION,NB_PRUEBA_SECCION,NO_TIEMPO,'CREADA',GETDATE(),@PIN_CL_USUARIO,@PIN_NB_PROGRAMA_CREA
					  FROM IDP.C_PRUEBA_SECCION CPS					 
					 WHERE ID_PRUEBA =@V_ID_PRUEBA_PLANTILLA;

						
					 --INSERTAR LAS PREGUNTAS DE LAS PRUEBAS
					 INSERT INTO ADM.K_CUESTIONARIO_PREGUNTA (ID_CUESTIONARIO,ID_PREGUNTA,NB_PREGUNTA,FE_CREACION,CL_USUARIO_APP_CREA,NB_PROGRAMA_CREA)
					 SELECT @V_ID_CUESTIONARIO,ID_PREGUNTA,NB_PREGUNTA,GETDATE(),@PIN_CL_USUARIO,@PIN_NB_PROGRAMA_CREA
					  FROM IDP.C_PRUEBA CPR
					 JOIN ADM.C_PREGUNTA CPG ON CPG.ID_CUESTIONARIO = CPR.ID_CUESTIONARIO
					 WHERE ID_PRUEBA =@V_ID_PRUEBA_PLANTILLA;

					set @j= @j+1;
				end;
				set @i= @i+1;
			END;
			--SE DEVUELVE LA VARIABLE DE RETORNO INDICANDO QUE TODO SE REALIZO CORRECTAMENTE		


			




			SET @XML_RESULTADO = DBO.F_ERROR_CREAR_ENCABEZADO( @@ROWCOUNT, 1, 'SUCCESSFUL')
			SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_MENSAJES(@XML_RESULTADO, 'FOLIOS CREADOS', 'ES')
			SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_MENSAJES(@XML_RESULTADO, 'FOLIOS CREADOS', 'EN')
			--SI SE GENERO UNA TRANSACCION EN ESTE BLOQUE LA TERMINARA
			IF (@@TRANCOUNT > 0 AND @V_EXIST_TRAN = 1)
				COMMIT	
	END TRY
	BEGIN CATCH		
		--SI OCURRIO UN ERROR Y SE INICIO UNA TRANSACCION ENE ESTE BLOQUE SE CANCELARA LA TRANSACCION
		IF (@@TRANCOUNT > 0 AND @V_EXIST_TRAN = 1)
			ROLLBACK
		--SE INDICA EN LA VARIABLE DE RETORNO QUE OCURRIO UN ERROR
		
		--SE INSERTA EL ERROR EN LA TABLA		
		DECLARE @ERROR_CLAVE INT  = 	ERROR_NUMBER()
		DECLARE @ERROR_MENSAJE NVARCHAR(250)  = 	 ERROR_MESSAGE()

		SET @XML_RESULTADO = DBO.F_ERROR_CREAR_ENCABEZADO( @@ROWCOUNT, @ERROR_CLAVE, 'ERROR')
		SET @XML_RESULTADO = DBO.F_ERROR_MENSAJES(@ERROR_CLAVE,@ERROR_MENSAJE)
		SET @XML_RESULTADO = DBO.F_ERROR_MENSAJES(@ERROR_CLAVE,@ERROR_MENSAJE)
			
	END CATCH
END