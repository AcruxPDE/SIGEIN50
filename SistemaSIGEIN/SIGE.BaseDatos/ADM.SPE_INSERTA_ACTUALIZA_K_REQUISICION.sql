-- =============================================
-- Proyecto: Sistema Nomina
-- Copyright (c) - Acrux - 2014
-- Author: Jesús Vázquez Pereyra
-- CRETAE date: 28/10/2015
-- Description: Inserta un nuevo registro en la tabla K_REQUISICION
-- =============================================
ALTER PROCEDURE [ADM].[SPE_INSERTA_ACTUALIZA_K_REQUISICION] 
	       @XML_RESULTADO XML = '' OUT      --APLICA PARA REGRESAR UN NÚMERO 0 PARA ERROR Y 1 PARA CORRECTO
    	, @PIN_ID_REQUISICION AS int
		, @PIN_NO_REQUISICION AS nvarchar(20)
		, @PIN_FE_SOLICITUD AS datetime
		, @PIN_ID_PUESTO AS int
		, @PIN_CL_ESTADO AS nvarchar(20)
		, @PIN_CL_CAUSA AS nvarchar(20)
		, @PIN_DS_CAUSA AS nvarchar(100)
		, @PIN_ID_NOTIFICACION AS int
		, @PIN_ID_SOLICITANTE AS int
		, @PIN_ID_AUTORIZA AS int
		, @PIN_ID_VISTO_BUENO AS int
		, @PIN_ID_EMPRESA AS int
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
			--SE INSERTA EL REGISTRO EN LA TABLA  ADM.K_REQUISICION
			INSERT INTO ADM.K_REQUISICION
					   ([NO_REQUISICION]
						, [FE_SOLICITUD]
						, [ID_PUESTO]
						, [CL_ESTADO]
						, [CL_CAUSA]
						, [DS_CAUSA]
						, [ID_NOTIFICACION]
						, [ID_SOLICITANTE]
						, [ID_AUTORIZA]
						, [ID_VISTO_BUENO]
						, [ID_EMPRESA]
						, [FE_CREACION]
						, [CL_USUARIO_APP_CREA]
						, [NB_PROGRAMA_CREA]
					)
			VALUES
					   (@PIN_NO_REQUISICION
						, @PIN_FE_SOLICITUD
						, @PIN_ID_PUESTO
						, @PIN_CL_ESTADO
						, @PIN_CL_CAUSA
						, @PIN_DS_CAUSA
						, @PIN_ID_NOTIFICACION
						, @PIN_ID_SOLICITANTE
						, @PIN_ID_AUTORIZA
						, @PIN_ID_VISTO_BUENO
						, @PIN_ID_EMPRESA
						,  GETDATE()
						, @PIN_CL_USUARIO_APP_CREA
						, @PIN_NB_PROGRAMA_CREA
					)			
		END ELSE BEGIN
			--SE ACTUALIZA EL REGISTRO EN LA TABLA  ADM.K_REQUISICION
			UPDATE ADM.K_REQUISICION SET
				[NO_REQUISICION] = @PIN_NO_REQUISICION
				, [FE_SOLICITUD] = @PIN_FE_SOLICITUD
				, [ID_PUESTO] = @PIN_ID_PUESTO
				, [CL_ESTADO] = @PIN_CL_ESTADO
				, [CL_CAUSA] = @PIN_CL_CAUSA
				, [DS_CAUSA] = @PIN_DS_CAUSA
				, [ID_NOTIFICACION] = @PIN_ID_NOTIFICACION
				, [ID_SOLICITANTE] = @PIN_ID_SOLICITANTE
				, [ID_AUTORIZA] = @PIN_ID_AUTORIZA
				, [ID_VISTO_BUENO] = @PIN_ID_VISTO_BUENO
				, [ID_EMPRESA] = @PIN_ID_EMPRESA
				, [FE_MODIFICACION] = GETDATE()
				, [CL_USUARIO_APP_MODIFICA] = @PIN_CL_USUARIO_APP_MODIFICA
				, [NB_PROGRAMA_MODIFICA] = @PIN_NB_PROGRAMA_MODIFICA
			       
			WHERE [ID_REQUISICION] = @PIN_ID_REQUISICION
									
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
