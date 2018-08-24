﻿-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CRETAE date: 27/10/2015
-- Description: Inserta un nuevo registro en la tabla C_CATALOGO_LISTA
-- =============================================
CREATE PROCEDURE [ADM].[SPE_INSERTA_ACTUALIZA_C_CATALOGO_LISTA] 
		  @XML_RESULTADO XML = '' OUT     --APLICA PARA REGRESAR UN NÚMERO 0 PARA ERROR Y 1 PARA CORRECTO
    	, @PIN_ID_CATALOGO_LISTA AS int
		, @PIN_NB_CATALOGO_LISTA AS nvarchar(100)
		, @PIN_DS_CATALOGO_LISTA AS nvarchar(1000)
		, @PIN_ID_CATALOGO_TIPO AS int
		, @PIN_CL_USUARIO_APP_CREA AS nvarchar(50)
		, @PIN_CL_USUARIO_APP_MODIFICA AS nvarchar(50)
		, @PIN_NB_PROGRAMA_CREA AS nvarchar(50)
		, @PIN_NB_PROGRAMA_MODIFICA AS nvarchar(50)
		, @PIN_TIPO_TRANSACCION CHAR(1)             --I=INSERCIÓN   A=ACTUALIZACIÓN

AS 
BEGIN  
	--SE DECLARA E INICIALIZA LA VARIABLE QUE NOS INDICARA SI GENERAMOS LA TRANSACCION EN ESTE SP
	DECLARE @V_EXIST_TRAN BIT = 0
	,@CFE_SISTEMA DATETIME = dbo.F_GETDATE()

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

		----- VALIDAR QUE NO SE REPITA NOMBRE Y DESCRIPCION EM CATALOGO

		DECLARE @V_NB_CATALOGO_LISTA INT
			   ,@V_DS_CATALOGO_LISTA INT

		IF @PIN_TIPO_TRANSACCION='I'
	    	BEGIN
			--SE INSERTA EL REGISTRO EN LA TABLA  ADM.C_CATALOGO_LISTA

		/*	SET @V_NB_CATALOGO_LISTA = (SELECT COUNT(*) FROM ADM.C_CATALOGO_LISTA WHERE NB_CATALOGO_LISTA = @PIN_NB_CATALOGO_LISTA)
			SET @V_DS_CATALOGO_LISTA = (SELECT COUNT(*) FROM ADM.C_CATALOGO_LISTA WHERE DS_CATALOGO_LISTA = @PIN_DS_CATALOGO_LISTA)

			-- VALIDA LA EXISTENCIA DE ALGUN REGISTRO CON EL MISMO NOMBRE
			IF @V_NB_CATALOGO_LISTA > 0
				--SE INSERTA EL ERROR EN LA TABLA		
		DECLARE @ERROR_CLAVE INT  = 2627
		DECLARE @ERROR_MENSAJE NVARCHAR(250)  =ERROR_MESSAGE()
		
			BEGIN
				SET @XML_RESULTADO = DBO.F_ERROR_CREAR_ENCABEZADO( @@ROWCOUNT, @ERROR_CLAVE, 'ERROR')
				SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_MENSAJES(@XML_RESULTADO, 'Nombre repetido', 'ES')
				SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_MENSAJES(@XML_RESULTADO, 'Nombre repetido', 'EN')
				ROLLBACK TRANSACTION
				RETURN;
			END
			*/
			-- VALIDA LA EXISTENCIA DE ALGUN REGISTRO CON LA MISMA DESCRIPCIÓN
			/*IF @V_DS_CATALOGO_LISTA > 0
			BEGIN
				SET @XML_RESULTADO = DBO.F_ERROR_CREAR_ENCABEZADO( @@ROWCOUNT, 4, 'WARNING')
				SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_MENSAJES(@XML_RESULTADO, 'Descripción repetida', 'ES')
				SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_MENSAJES(@XML_RESULTADO, 'Descripción repetida', 'EN')
				ROLLBACK TRANSACTION
				RETURN;
			END
			*/

			INSERT INTO ADM.C_CATALOGO_LISTA
					   ([NB_CATALOGO_LISTA]
						, [DS_CATALOGO_LISTA]
						, [ID_CATALOGO_TIPO]
						, [FE_CREACION]
						, [CL_USUARIO_APP_CREA]
						, [NB_PROGRAMA_CREA]
					)
			VALUES
					   ( @PIN_NB_CATALOGO_LISTA
						, @PIN_DS_CATALOGO_LISTA
						, @PIN_ID_CATALOGO_TIPO
						,  @CFE_SISTEMA
						, @PIN_CL_USUARIO_APP_CREA
						, @PIN_NB_PROGRAMA_CREA
					)			
		END 
		ELSE 
			BEGIN
			--SE ACTUALIZA EL REGISTRO EN LA TABLA  ADM.C_CATALOGO_LISTA
			UPDATE ADM.C_CATALOGO_LISTA SET
				[NB_CATALOGO_LISTA] = @PIN_NB_CATALOGO_LISTA
				, [DS_CATALOGO_LISTA] = @PIN_DS_CATALOGO_LISTA
				, [ID_CATALOGO_TIPO] = @PIN_ID_CATALOGO_TIPO
				, [FE_MODIFICACION] =@CFE_SISTEMA
				, [CL_USUARIO_APP_MODIFICA] = @PIN_CL_USUARIO_APP_MODIFICA
				, [NB_PROGRAMA_MODIFICA] = @PIN_NB_PROGRAMA_MODIFICA
			       
			WHERE [ID_CATALOGO_LISTA] = @PIN_ID_CATALOGO_LISTA
									
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

		--SE INSERTA EL ERROR EN LA TABLA		
		DECLARE @ERROR_CLAVE2 INT  = 	ERROR_NUMBER()
		DECLARE @ERROR_MENSAJE2 NVARCHAR(250)  = 	 ERROR_MESSAGE()
		
		-- EL XML DEVUELVE EL ERROR INDICADO POR SQL Y UN MSJ DE ERROR GENÉRICO
		SET @XML_RESULTADO = DBO.F_ERROR_CREAR_ENCABEZADO( @@ROWCOUNT, @ERROR_CLAVE2, 'ERROR')
		SET @XML_RESULTADO = DBO.F_ERROR_MENSAJES(@ERROR_CLAVE2,@ERROR_MENSAJE2)
		SET @XML_RESULTADO = DBO.F_ERROR_MENSAJES(@ERROR_CLAVE2,@ERROR_MENSAJE2)
			
	END CATCH
END