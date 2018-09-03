﻿-- =============================================
-- Proyecto: Sistema Nomina
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 14/11/2015
-- Description: Elimina un registro de la tabla C_PLANTILLA_FORMULARIO
-- =============================================
CREATE PROCEDURE [ADM].[SPE_ELIMINA_C_PLANTILLA_FORMULARIO]
	@XML_RESULTADO XML OUT,     --APLICA PARA REGRESAR UN NÚMERO 0 PARA ERROR Y 1 PARA CORRECTO
	@PIN_ID_PLANTILLA_SOLICITUD INT, 
	@PIN_CL_USUARIO NVARCHAR(50), --USUARIO QUE MANDA A ELIMINAR EL REGISTRO
	@PIN_NB_PROGRAMA NVARCHAR(50) -- PROGRAMA DONDE EL USUARIO MANDA ELIMINAR EL REGISTRO
AS   
BEGIN
	--SE DECLARA E INICIALIZA LA VARIABLE QUE NOS INDICARA SI GENERAMOS LA TRANSACCION EN ESTE SP
	DECLARE @V_EXIST_TRAN BIT = 0
	BEGIN TRY		   			
		--SE VERIFICA SI EXISTE UNA TRANSACCION EN EJECUCION
		IF (@@TRANCOUNT = 0) BEGIN
			--EN CASO DE QUE NO SE INICIALIZA LA TRANSACCION
			BEGIN TRANSACTION
			--SE EDITA LA VARIABLE QUE INDICA QUE SE INICIO LA TRANSACCION EN ESTE BLOQUE PARA CANCELARLA SI ES NECESARIO
			SET @V_EXIST_TRAN = 1
		END	
		--SE BORRA EL REGISTRO EN LA TABLA  IDP.C_PLANTILLA_FORMULARIO
		DELETE FROM ADM.C_PLANTILLA_FORMULARIO WHERE ID_PLANTILLA_SOLICITUD = @PIN_ID_PLANTILLA_SOLICITUD
				

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
		DECLARE @ERROR_CLAVE INT  = 	ERROR_NUMBER()
		DECLARE @ERROR_MENSAJE NVARCHAR(250)

		IF @ERROR_CLAVE = 547
		BEGIN
			SET @ERROR_MENSAJE = 'Registro asociado'
		END
		ELSE
		BEGIN
			SET @ERROR_MENSAJE = 'Ocurrió un error al procesar el registro'
		END


		SET @XML_RESULTADO = DBO.F_ERROR_CREAR_ENCABEZADO( @@ROWCOUNT, @ERROR_CLAVE, 'ERROR')
		SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_MENSAJES(@XML_RESULTADO, @ERROR_MENSAJE, 'ES')
		SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_MENSAJES(@XML_RESULTADO, @ERROR_MENSAJE, 'EN')

	END CATCH	
END 
