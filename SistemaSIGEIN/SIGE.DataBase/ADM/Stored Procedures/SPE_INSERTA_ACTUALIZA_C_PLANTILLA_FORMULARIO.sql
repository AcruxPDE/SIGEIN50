﻿-- =============================================
-- Proyecto: Sistema Nomina
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CRETAE date: 14/11/2015
-- Description: Inserta un nuevo registro en la tabla C_PLANTILLA_FORMULARIO
-- =============================================
-- 25/02/2016 JDR Se eliminan parámetros de bitácora
-- =============================================

CREATE PROCEDURE [ADM].[SPE_INSERTA_ACTUALIZA_C_PLANTILLA_FORMULARIO] 
    	  @XML_RESULTADO XML = '' OUT      --APLICA PARA REGRESAR UN NÚMERO 0 PARA ERROR Y 1 PARA CORRECTO
    	, @PIN_ID_PLANTILLA_SOLICITUD INT
		, @PIN_NB_PLANTILLA_SOLICITUD NVARCHAR(100)
		, @PIN_DS_PLANTILLA_SOLICITUD NVARCHAR(500)
		, @PIN_CL_FORMULARIO NVARCHAR(20)
		, @PIN_XML_PLANTILLA_SOLICITUD XML
		, @PIN_CL_USUARIO NVARCHAR(50)
		, @PIN_NB_PROGRAMA NVARCHAR(50)
		, @PIN_TIPO_TRANSACCION CHAR(1)             --I=INSERCIÓN   A=ACTUALIZACIÓN

AS 
BEGIN  
	--SE DECLARA E INICIALIZA LA VARIABLE QUE NOS INDICARA SI GENERAMOS LA TRANSACCION EN ESTE SP
	DECLARE @V_EXIST_TRAN BIT = 0
		, @FE_SISTEMA DATETIME = DBO.F_GETDATE()
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
			--SE INSERTA EL REGISTRO EN LA TABLA  IDP.C_PLANTILLA_FORMULARIO
			INSERT INTO ADM.C_PLANTILLA_FORMULARIO (
				NB_PLANTILLA_SOLICITUD
				, DS_PLANTILLA_SOLICITUD
				, CL_FORMULARIO
				, XML_PLANTILLA_SOLICITUD
				, FG_GENERAL
				, FE_CREACION
				, CL_USUARIO_APP_CREA
				, NB_PROGRAMA_CREA
			)
			VALUES (
				@PIN_NB_PLANTILLA_SOLICITUD
				, @PIN_DS_PLANTILLA_SOLICITUD
				, @PIN_CL_FORMULARIO
				, @PIN_XML_PLANTILLA_SOLICITUD
				, 0
				, @FE_SISTEMA
				, @PIN_CL_USUARIO
				, @PIN_NB_PROGRAMA
			)			
		END ELSE BEGIN
			--SE ACTUALIZA EL REGISTRO EN LA TABLA  IDP.C_PLANTILLA_FORMULARIO
			UPDATE ADM.C_PLANTILLA_FORMULARIO SET
				NB_PLANTILLA_SOLICITUD = @PIN_NB_PLANTILLA_SOLICITUD
				, DS_PLANTILLA_SOLICITUD = @PIN_DS_PLANTILLA_SOLICITUD
				, CL_FORMULARIO = @PIN_CL_FORMULARIO
				, XML_PLANTILLA_SOLICITUD = @PIN_XML_PLANTILLA_SOLICITUD
				, FE_MODIFICACION = @FE_SISTEMA
				, CL_USUARIO_APP_MODIFICA = @PIN_CL_USUARIO
				, NB_PROGRAMA_MODIFICA = @PIN_NB_PROGRAMA
			WHERE ID_PLANTILLA_SOLICITUD = @PIN_ID_PLANTILLA_SOLICITUD
									
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

		SET @XML_RESULTADO = DBO.F_ERROR_MENSAJES(ERROR_NUMBER(), ERROR_MESSAGE())
			
	END CATCH
END
