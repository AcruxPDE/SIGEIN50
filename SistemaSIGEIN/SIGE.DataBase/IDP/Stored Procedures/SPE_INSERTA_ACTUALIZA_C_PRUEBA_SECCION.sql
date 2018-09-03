﻿-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Juan De Dios Pérez Pedroza
-- CRETAE date: 21/09/2015
-- Description: Inserta un nuevo registro en la tabla C_PRUEBA_TIEMPO
-- =============================================
CREATE PROCEDURE [IDP].[SPE_INSERTA_ACTUALIZA_C_PRUEBA_SECCION] 
		  @XML_RESULTADO XML = '' OUT,      --APLICA PARA REGRESAR UN NÚMERO 0 PARA ERROR Y 1 PARA CORRECTO
	      @PIN_ID_PRUEBA_TIEMPO int 
	    , @PIN_ID_PRUEBA int 
		, @PIN_CL_PRUEBA_TIEMPO AS nvarchar(20) 
		, @PIN_NB_PRUEBA_TIEMPO AS nvarchar(200)
		, @PIN_NO_TIEMPO AS smallint 
		, @PIN_CL_USUARIO_APP_CREA AS nvarchar(50)
		, @PIN_CL_USUARIO_APP_MODIFICA AS nvarchar(50)
		, @PIN_NB_PROGRAMA_CREA AS nvarchar(50)
		, @PIN_NB_PROGRAMA_MODIFICA AS nvarchar(50)
		, @PIN_TIPO_TRANSACCION CHAR(1)             --I=INSERCIÓN   A=ACTUALIZACIÓN

AS 
BEGIN  
	--SE DECLARA E INICIALIZA LA VARIABLE QUE NOS INDICARA SI GENERAMOS LA TRANSACCION EN ESTE SP
	DECLARE @V_EXIST_TRAN BIT = 0
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
			--SE INSERTA EL REGISTRO EN LA TABLA  ADM.C_PRUEBA
			INSERT INTO IDP.C_PRUEBA_SECCION
					   (
						 [ID_PRUEBA]
						,[CL_PRUEBA_SECCION]
						,[NB_PRUEBA_SECCION]
						,[NO_TIEMPO]
						, [FE_CREACION]
						, [CL_USUARIO_APP_CREA]
						, [NB_PROGRAMA_CREA]
					)
			VALUES
					   (  @PIN_ID_PRUEBA
					    , @PIN_CL_PRUEBA_TIEMPO
						, @PIN_NB_PRUEBA_TIEMPO
						, @PIN_NO_TIEMPO
						,  GETDATE()
						, @PIN_CL_USUARIO_APP_CREA
						, @PIN_NB_PROGRAMA_CREA
					)			
		END ELSE BEGIN
			--SE ACTUALIZA EL REGISTRO EN LA TABLA  ADM.C_PRUEBA
			UPDATE IDP.C_PRUEBA_SECCION SET
			      [ID_PRUEBA] = @PIN_ID_PRUEBA
				, [CL_PRUEBA_SECCION] = @PIN_CL_PRUEBA_TIEMPO
				, [NB_PRUEBA_SECCION] = @PIN_NB_PRUEBA_TIEMPO
				, [NO_TIEMPO] = @PIN_NO_TIEMPO
				, [FE_MODIFICACION] = GETDATE()
				, [CL_USUARIO_APP_MODIFICA] = @PIN_CL_USUARIO_APP_MODIFICA
				, [NB_PROGRAMA_MODIFICA] = @PIN_NB_PROGRAMA_MODIFICA
			       
			WHERE [ID_PRUEBA_SECCION] = @PIN_ID_PRUEBA_TIEMPO
		--AND [CL_AREA_INTERES] = @PIN_CL_AREA_INTERES	
							
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
			
		DECLARE @ERROR_CLAVE INT  = 	ERROR_NUMBER()
		DECLARE @ERROR_MENSAJE NVARCHAR(250)  = 	 ERROR_MESSAGE()
	
	    SET @XML_RESULTADO = DBO.F_ERROR_CREAR_ENCABEZADO( @@ROWCOUNT, @ERROR_CLAVE, 'ERROR')
		SET @XML_RESULTADO = DBO.F_ERROR_MENSAJES( @ERROR_CLAVE,'Ocurrió un error al procesar el registro')
		SET @XML_RESULTADO = DBO.F_ERROR_MENSAJES( @ERROR_CLAVE, 'Ocurrió un error al procesar el registro')
		
	END CATCH
END