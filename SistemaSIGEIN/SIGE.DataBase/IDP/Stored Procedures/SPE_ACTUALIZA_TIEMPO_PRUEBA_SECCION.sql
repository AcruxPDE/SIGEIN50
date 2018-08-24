﻿-- =============================================
-- Proyecto: SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Daniela Sanchez
-- CRETAE date: 10/02/2016
-- Description: Actualiza tiempo de seccion de pruebas
-- =============================================
CREATE PROCEDURE [IDP].[SPE_ACTUALIZA_TIEMPO_PRUEBA_SECCION]
@XML_RESULTADO XML = '' OUT 
,@PIN_XML_TIEMPO AS XML
		, @PIN_CL_USUARIO AS nvarchar(50)
		, @PIN_NB_PROGRAMA AS nvarchar(50)

	AS 

BEGIN  
	--SE DECLARA E INICIALIZA LA VARIABLE QUE NOS INDICARA SI GENERAMOS LA TRANSACCION EN ESTE SP
	DECLARE @V_EXIST_TRAN BIT = 0,
			@CFE_SISTEMA DATETIME = dbo.F_GETDATE()
    	BEGIN TRY
		--SE VERIFICA SI EXISTE UNA TRANSACCION EN EJECUCION
		IF (@@TRANCOUNT = 0) 
		BEGIN
			--EN CASO DE QUE NO SE INICIALIZA LA TRANSACCION
			BEGIN TRANSACTION
			--SE EDITA LA VARIABLE QUE INDICA QUE SE INICIO LA TRANSACCION EN ESTE BLOQUE PARA CANCELARLA SI ES NECESARIO
			SET @V_EXIST_TRAN = 1
		END
		UPDATE CPS
			SET NO_TIEMPO =d.value('@NO_TIEMPO', 'INT')
				,FE_MODIFICACION = @CFE_SISTEMA
				,CL_USUARIO_APP_MODIFICA = @PIN_CL_USUARIO
				,NB_PROGRAMA_MODIFICA=@PIN_NB_PROGRAMA 
			FROM IDP.C_PRUEBA_SECCION CPS 
				INNER JOIN @PIN_XML_TIEMPO.nodes('TIEMPOS/TIEMPO')  T(d)
				ON d.value('@ID_PRUEBA_SECCION', 'int') = CPS.ID_PRUEBA_SECCION;

				

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