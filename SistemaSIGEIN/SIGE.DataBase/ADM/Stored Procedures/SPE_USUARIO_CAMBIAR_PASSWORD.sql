﻿-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Julio Díaz
-- CRETAE date: 9/12/2015
-- Description: Establece el mecanismo para cambiar la contraseña de un usuario
-- =============================================
CREATE PROCEDURE [ADM].[SPE_USUARIO_CAMBIAR_PASSWORD]
		  @XML_RESULTADO XML OUT     --APLICA PARA REGRESAR UN NÚMERO 0 PARA ERROR Y 1 PARA CORRECTO
    	, @PIN_CL_USER AS NVARCHAR(50) = NULL
		, @PIN_NB_CORREO_ELECTRONICO AS NVARCHAR(500)
		, @PIN_NB_PASSWORD AS NVARCHAR(100)
		, @PIN_CL_TOKEN AS NVARCHAR(100)
		, @PIN_CL_USUARIO AS NVARCHAR(50)
		, @PIN_NB_PROGRAMA AS NVARCHAR(50)
AS 
BEGIN  
	--SE DECLARA E INICIALIZA LA VARIABLE QUE NOS INDICARA SI GENERAMOS LA TRANSACCION EN ESTE SP
	DECLARE @FE_SISTEMA DATETIME = GETDATE()
		, @XML_DATOS XML
		, @CL_USER AS NVARCHAR(50)

    BEGIN TRY
		BEGIN TRANSACTION
			
			IF (NULLIF(RTRIM(LTRIM(@PIN_NB_CORREO_ELECTRONICO)), '') IS NOT NULL OR NULLIF(RTRIM(LTRIM(@PIN_CL_USER)), '') IS NOT NULL) BEGIN
				UPDATE ADM.C_USUARIO
				SET CL_CAMBIAR_PASSWORD = @PIN_CL_TOKEN
					, FG_CAMBIAR_PASSWORD = 1
					, FE_CAMBIAR_PASSWORD = @FE_SISTEMA
					, FE_MODIFICACION = @FE_SISTEMA
					, NB_PROGRAMA_MODIFICA = @PIN_NB_PASSWORD
					, CL_USUARIO_APP_MODIFICA = @PIN_CL_USUARIO
				WHERE CL_USUARIO = @PIN_CL_USER OR NB_CORREO_ELECTRONICO = @PIN_NB_CORREO_ELECTRONICO

				IF (@@ROWCOUNT > 0) BEGIN

					SET @XML_DATOS = (
						SELECT TOP 1 CU.CL_USUARIO AS '@CL_USUARIO'
							, CU.NB_USUARIO AS '@NB_USUARIO'
							, CU.NB_CORREO_ELECTRONICO AS '@NB_CORREO_ELECTRONICO'
							, 'CHANGING' AS '@CL_ESTADO_RECUPERACION'
						FROM ADM.C_USUARIO CU
						WHERE CL_USUARIO = @PIN_CL_USER OR NB_CORREO_ELECTRONICO = @PIN_NB_CORREO_ELECTRONICO
						FOR XML PATH('USUARIO')
					)

					SET @XML_RESULTADO = DBO.F_ERROR_CREAR_ENCABEZADO (@@ROWCOUNT, 1, 'SUCCESSFUL')
					SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_MENSAJES (@XML_RESULTADO, 'Se ha enviado un código de recuperación de tu contraseña a tu correo electrónico.', 'ES')
					SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_MENSAJES (@XML_RESULTADO, 'Se ha enviado un código de recuperación de tu contraseña a tu correo electrónico.', 'EN')
					SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_DATOS (@XML_RESULTADO, @XML_DATOS)
				END ELSE BEGIN
					IF (NULLIF(RTRIM(LTRIM(@PIN_NB_CORREO_ELECTRONICO)), '') IS NOT NULL) BEGIN
						SET @XML_RESULTADO = DBO.F_ERROR_CREAR_ENCABEZADO (@@ROWCOUNT, 1, 'WARNING')
						SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_MENSAJES (@XML_RESULTADO, 'El correo electrónico ' + @PIN_NB_CORREO_ELECTRONICO + '  no está asociado a ninguna cuenta.', 'ES')
						SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_MENSAJES (@XML_RESULTADO, 'El correo electrónico ' + @PIN_NB_CORREO_ELECTRONICO + '  no está asociado a ninguna cuenta.', 'EN')
					END ELSE BEGIN
						SET @XML_RESULTADO = DBO.F_ERROR_CREAR_ENCABEZADO (@@ROWCOUNT, 1, 'WARNING')
						SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_MENSAJES (@XML_RESULTADO, 'La cuenta ' + @PIN_CL_USER + ' existe.', 'ES')
						SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_MENSAJES (@XML_RESULTADO, 'La cuenta ' + @PIN_CL_USER + ' existe.', 'EN')
					END
				END
			END ELSE BEGIN
				
				SET @CL_USER = (SELECT TOP 1 CL_USUARIO FROM ADM.C_USUARIO WHERE CL_CAMBIAR_PASSWORD = @PIN_CL_TOKEN)

				UPDATE ADM.C_USUARIO
				SET CL_CAMBIAR_PASSWORD = NULL
					, FG_CAMBIAR_PASSWORD = 0
					, FE_CAMBIAR_PASSWORD = @FE_SISTEMA
					, NB_PASSWORD = @PIN_NB_PASSWORD
					, FE_MODIFICACION = @FE_SISTEMA
					, NB_PROGRAMA_MODIFICA = @PIN_NB_PROGRAMA
					, CL_USUARIO_APP_MODIFICA = @PIN_CL_USUARIO
				WHERE CL_CAMBIAR_PASSWORD = @PIN_CL_TOKEN
					AND @FE_SISTEMA < DATEADD(HOUR, 1, FE_CAMBIAR_PASSWORD)

				IF (@@ROWCOUNT > 0) BEGIN

					SET @XML_DATOS = (
						SELECT TOP 1 CU.CL_USUARIO AS '@CL_USUARIO'
							, CU.NB_USUARIO AS '@NB_USUARIO'
							, CU.NB_CORREO_ELECTRONICO AS '@NB_CORREO_ELECTRONICO'
							, 'CHANGED' AS '@CL_ESTADO_RECUPERACION'
						FROM ADM.C_USUARIO CU
						WHERE CL_USUARIO = @CL_USER
						FOR XML PATH('USUARIO')
					)

					SET @XML_RESULTADO = DBO.F_ERROR_CREAR_ENCABEZADO(@@ROWCOUNT, 1, 'SUCCESSFUL')
					SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_MENSAJES(@XML_RESULTADO, 'La contraseña se cambió satisfactoriamente', 'ES')
					SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_MENSAJES(@XML_RESULTADO, 'La contraseña se cambió satisfactoriamente', 'EN')
					SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_DATOS (@XML_RESULTADO, @XML_DATOS)
				END ELSE BEGIN
					SET @XML_RESULTADO = DBO.F_ERROR_CREAR_ENCABEZADO(@@ROWCOUNT, 1, 'WARNING')
					SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_MENSAJES(@XML_RESULTADO, 'El código ha caducado.', 'ES')
					SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_MENSAJES(@XML_RESULTADO, 'El código ha caducado.', 'EN')
				END
			END

		COMMIT	
	END TRY
	BEGIN CATCH
		ROLLBACK
		
		-- EL XML DEVUELVE EL ERROR INDICADO POR SQL Y UN MSJ DE ERROR GENÉRICO
		SET @XML_RESULTADO = DBO.F_ERROR_MENSAJES(ERROR_NUMBER(), ERROR_MESSAGE())
			
	END CATCH
END