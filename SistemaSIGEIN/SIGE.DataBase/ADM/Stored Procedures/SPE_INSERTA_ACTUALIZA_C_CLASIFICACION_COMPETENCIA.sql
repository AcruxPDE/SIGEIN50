﻿-- =============================================
-- Proyecto: Sistema Nomina
-- Copyright (c) - Acrux - 2014
-- Author: Jesús Vázquez Pereyra
-- CRETAE date: 07/10/2015
-- Description: Inserta un nuevo registro en la tabla C_CLASIFICACION_COMPETENCIA
-- =============================================
-- 20/11/2015 MS - Se cambia el valor de retorno
-- ============================================= 

CREATE PROCEDURE [ADM].[SPE_INSERTA_ACTUALIZA_C_CLASIFICACION_COMPETENCIA] 
			@XML_RESULTADO XML = '' OUT      --APLICA PARA REGRESAR UN NÚMERO 0 PARA ERROR Y 1 PARA CORRECTO
    	, @PIN_ID_CLASIFICACION_COMPETENCIA AS int
		, @PIN_CL_CLASIFICACION AS nvarchar(20)
		, @PIN_CL_TIPO_COMPETENCIA AS nvarchar(20)
		, @PIN_NB_CLASIFICACION_COMPETENCIA AS nvarchar(100)
		, @PIN_DS_CLASIFICACION_COMPETENCIA AS nvarchar(1000)
		, @PIN_DS_NOTAS_CLASIFICACION AS nvarchar(1000)
		, @PIN_FG_ACTIVO AS BIT
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

		DECLARE @V_CL_CLASIFICACION INT 
			   ,@V_NB_CLASIFICACION_COMPETENCIA INT
			   ,@V_DS_CLASIFICACION_COMPETENCIA INT

		--SE VERIFICA SI SE INSERTA EL REGISTRO O SE ACTUALIZARA SEGUN LA VARIABLE DE TIPO DE TRANSACCION  QUE RECIBE EL SP
		IF @PIN_TIPO_TRANSACCION='I'
	    	BEGIN




			--SE INSERTA EL REGISTRO EN LA TABLA  ADM.C_CLASIFICACION_COMPETENCIA
			INSERT INTO ADM.C_CLASIFICACION_COMPETENCIA
					   ([CL_CLASIFICACION]
						, [CL_TIPO_COMPETENCIA]
						, [NB_CLASIFICACION_COMPETENCIA]
						, [DS_CLASIFICACION_COMPETENCIA]
						, [DS_NOTAS_CLASIFICACION]
						, FG_ACTIVO
						, [FE_CREACION]
						, [CL_USUARIO_APP_CREA]
						, [NB_PROGRAMA_CREA]
					)
			VALUES
					   (@PIN_CL_CLASIFICACION
						, @PIN_CL_TIPO_COMPETENCIA
						, @PIN_NB_CLASIFICACION_COMPETENCIA
						, @PIN_DS_CLASIFICACION_COMPETENCIA
						, @PIN_DS_NOTAS_CLASIFICACION
						, @PIN_FG_ACTIVO
						,  @CFE_SISTEMA
						, @PIN_CL_USUARIO_APP_CREA
						, @PIN_NB_PROGRAMA_CREA
					)			
		END ELSE BEGIN
			--SE ACTUALIZA EL REGISTRO EN LA TABLA  ADM.C_CLASIFICACION_COMPETENCIA
			UPDATE ADM.C_CLASIFICACION_COMPETENCIA SET
				[CL_CLASIFICACION] = @PIN_CL_CLASIFICACION
				, [CL_TIPO_COMPETENCIA] = @PIN_CL_TIPO_COMPETENCIA
				, [NB_CLASIFICACION_COMPETENCIA] = @PIN_NB_CLASIFICACION_COMPETENCIA
				, [DS_CLASIFICACION_COMPETENCIA] = @PIN_DS_CLASIFICACION_COMPETENCIA
				, [DS_NOTAS_CLASIFICACION] = @PIN_DS_NOTAS_CLASIFICACION
				, FG_ACTIVO = @PIN_FG_ACTIVO
				, [FE_MODIFICACION] = @CFE_SISTEMA
				, [CL_USUARIO_APP_MODIFICA] = @PIN_CL_USUARIO_APP_MODIFICA
				, [NB_PROGRAMA_MODIFICA] = @PIN_NB_PROGRAMA_MODIFICA
			       
			WHERE [ID_CLASIFICACION_COMPETENCIA] = @PIN_ID_CLASIFICACION_COMPETENCIA
									
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
		
		--SE INSERTA EL ERROR EN LA TABLA		
		DECLARE @ERROR_CLAVE INT  = 	ERROR_NUMBER()
		DECLARE @ERROR_MENSAJE NVARCHAR(250)  = 	 ERROR_MESSAGE()
		
		SET @XML_RESULTADO = DBO.F_ERROR_CREAR_ENCABEZADO( @@ROWCOUNT, @ERROR_CLAVE, 'ERROR')
		SET @XML_RESULTADO = DBO.F_ERROR_MENSAJES(@ERROR_CLAVE,@ERROR_MENSAJE)
		SET @XML_RESULTADO = DBO.F_ERROR_MENSAJES(@ERROR_CLAVE,@ERROR_MENSAJE)
			
	END CATCH
END
