﻿-- =============================================
-- Proyecto: Sigein 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CRETAE date: 14/09/2015
-- Description: Inserta un nuevo C_ESTADO
-- =============================================
CREATE PROCEDURE [ADM].[SPE_INSERTA_ACTUALIZA_C_ESTADO] 
    	  @XML_RESULTADO XML = '' OUT      --APLICA PARA REGRESAR UN NÚMERO 0 PARA ERROR Y 1 PARA CORRECTO
    	, @PIN_ID_ESTADO AS int
		, @PIN_CL_PAIS AS nvarchar(10)
		, @PIN_CL_ESTADO AS nvarchar(10)
		, @PIN_NB_ESTADO AS nvarchar(100)
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
		IF @PIN_TIPO_TRANSACCION='I'
	    	BEGIN
			--SE INSERTA EL REGISTRO EN LA TABLA  ADM.C_ESTADO
			INSERT INTO ADM.C_ESTADO
					   ([CL_PAIS]
						, [CL_ESTADO]
						, [NB_ESTADO]
						, [FE_CREACION]
						, [CL_USUARIO_APP_CREA]
						, [NB_PROGRAMA_CREA]
					)
			VALUES
					   (@PIN_CL_PAIS
						, @PIN_CL_ESTADO
						, @PIN_NB_ESTADO
						, @CFE_SISTEMA
						, @PIN_CL_USUARIO_APP_CREA
						, @PIN_NB_PROGRAMA_CREA
					)			
		END ELSE BEGIN
			--SE ACTUALIZA EL REGISTRO EN LA TABLA  ADM.C_ESTADO
			UPDATE ADM.C_ESTADO SET
				[CL_PAIS] = @PIN_CL_PAIS
				, [CL_ESTADO] = @PIN_CL_ESTADO
				, [NB_ESTADO] = @PIN_NB_ESTADO
				, [FE_MODIFICACION] = @CFE_SISTEMA
				, [CL_USUARIO_APP_MODIFICA] = @PIN_CL_USUARIO_APP_MODIFICA
				, [NB_PROGRAMA_MODIFICA] = @PIN_NB_PROGRAMA_MODIFICA
			       
			WHERE [ID_ESTADO] = @PIN_ID_ESTADO
									
		END
		--SE DEVUELVE LA VARIABLE DE RETORNO INDICANDO QUE TODO SE REALIZO CORRECTAMENTE
		SET @XML_RESULTADO = DBO.F_ERROR_CREAR_ENCABEZADO( @@ROWCOUNT, ERROR_NUMBER(), 'SUCCESSFUL')
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
		DECLARE @ERROR_CLAVE INT  = 	ERROR_NUMBER()
		DECLARE @ERROR_MENSAJE NVARCHAR(250)  = 	 ERROR_MESSAGE()
		
		SET @XML_RESULTADO = DBO.F_ERROR_CREAR_ENCABEZADO( @@ROWCOUNT, @ERROR_CLAVE, 'ERROR')
		SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_MENSAJES(@XML_RESULTADO, 'Ocurrió un error al procesar el registro', 'ES')
		SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_MENSAJES(@XML_RESULTADO, 'Ocurrió un error al procesar el registro', 'EN')
			
	END CATCH
END