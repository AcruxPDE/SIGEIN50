﻿-- =============================================
-- Proyecto: Sigein 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 15/09/2015
-- Description: Elimina un C_EMPLEADO_ESCOLARIDAD
-- =============================================
CREATE PROCEDURE [ADM].[SPE_ELIMINA_C_EMPLEADO_ESCOLARIDAD]
	@XML_RESULTADO XML OUT,      --APLICA PARA REGRESAR UN NÚMERO 0 PARA ERROR Y 1 PARA CORRECTO
	@PIN_ID_EMPLEADO_ESCOLARIDAD AS int, 
	@PIN_CL_USUARIO_APP_CREA AS nvarchar(50), --USUARIO QUE MANDA A ELIMINAR EL REGISTRO
	@PIN_NB_PROGRAMA_CREA AS nvarchar(50) -- PROGRAMA DONDE EL USUARIO MANDA ELIMINAR EL REGISTRO
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
		--SE BORRA EL REGISTRO EN LA TABLA  ADM.C_EMPLEADO_ESCOLARIDAD
		DELETE FROM ADM.C_EMPLEADO_ESCOLARIDAD
		WHERE [ID_EMPLEADO_ESCOLARIDAD] = @PIN_ID_EMPLEADO_ESCOLARIDAD
				

		SET @XML_RESULTADO = DBO.F_ERROR_CREAR_ENCABEZADO( @@ROWCOUNT, ERROR_NUMBER(), 'SUCCESSFUL')
	    SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_MENSAJES(@XML_RESULTADO, 'Se eliminó las escolaridades del empleado satisfactoriamente', 'ES')
		SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_MENSAJES(@XML_RESULTADO, 'Se eliminó las escolardiades del empleado satisfactoriamente', 'EN')
		
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
