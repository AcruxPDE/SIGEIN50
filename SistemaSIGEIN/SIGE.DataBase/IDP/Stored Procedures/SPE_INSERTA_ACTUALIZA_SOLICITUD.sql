﻿-- =============================================
-- Proyecto: Sistema Nomina
-- Copyright (c) - Acrux - 2014
-- Author: Julio Díaz
-- CREATE date: 22/01/2016
-- Description: Inserta o actualiza la solicitud
-- =============================================
CREATE PROCEDURE [IDP].[SPE_INSERTA_ACTUALIZA_SOLICITUD]
	@XML_RESULTADO XML OUT
	, @PIN_XML_SOLICITUD_PLANTILLA XML
	, @PIN_ID_SOLICITUD INT
	, @PIN_CL_USUARIO NVARCHAR(50)
	, @PIN_NB_PROGRAMA NVARCHAR(50)
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
		--SE BORRA EL REGISTRO EN LA TABLA  ADM.C_PUESTO_FUNCION

		SET DATEFORMAT DMY

		DECLARE @T_VALORES TABLE (
			ID_CAMPO NVARCHAR(MAX)
			, NB_VALOR NVARCHAR(MAX)
			, CL_VALOR NVARCHAR(MAX)
			, XML_VALOR XML
		)

		INSERT INTO @T_VALORES (ID_CAMPO, NB_VALOR, CL_VALOR, XML_VALOR)
		SELECT c.value('@ID_CAMPO', 'NVARCHAR(MAX)') AS ID_CAMPO
			, c.value('@NB_VALOR', 'NVARCHAR(MAX)') AS NB_VALOR
			, c.value('@CL_VALOR', 'NVARCHAR(MAX)') AS CL_VALOR
			, c.query('GRID/DATOS') AS XML_VALOR
		FROM @PIN_XML_SOLICITUD_PLANTILLA.nodes('/PLANTILLA/CONTENEDORES/CONTENEDOR/CAMPO') x(c)

		DECLARE @ID_CANDIDATO INT							= (SELECT TOP 1 ID_CANDIDATO FROM ADM.K_SOLICITUD WHERE ID_SOLICITUD = @PIN_ID_SOLICITUD)
			, @NB_CANDIDATO NVARCHAR(100)					= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'NB_CANDIDATO')
			, @NB_APELLIDO_PATERNO NVARCHAR(100)			= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'NB_APELLIDO_PATERNO')
			, @NB_APELLIDO_MATERNO NVARCHAR(100)			= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'NB_APELLIDO_MATERNO')
			, @CL_RFC NVARCHAR(20)							= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'CL_RFC')
			, @CL_CURP NVARCHAR(20)							= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'CL_CURP')
			, @CL_NSS NVARCHAR(20)							= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'CL_NSS')
			, @DS_LUGAR_NACIMIENTO NVARCHAR(200)			= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'DS_LUGAR_NACIMIENTO')
			, @NB_NACIONALIDAD NVARCHAR(30)					= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'NB_NACIONALIDAD')
			, @FE_NACIMIENTO DATE							= (SELECT TOP 1 CONVERT(DATE, NB_VALOR) FROM @T_VALORES WHERE ID_CAMPO = 'FE_NACIMIENTO')
			, @CL_GENERO NVARCHAR(1)						= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'CL_GENERO')
			, @CL_ESTADO_CIVIL NVARCHAR(20)					= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'CL_ESTADO_CIVIL')
			, @NB_CONYUGUE NVARCHAR(100)					= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'NB_CONYUGUE')
			, @CL_CODIGO_POSTAL NVARCHAR(10)				= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'CL_CODIGO_POSTAL')
			, @CL_ESTADO NVARCHAR(10)						= (SELECT TOP 1 CL_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'NB_ESTADO')
			, @NB_ESTADO NVARCHAR(100)						= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'NB_ESTADO')
			, @CL_MUNICIPIO NVARCHAR(10)					= (SELECT TOP 1 CL_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'NB_MUNICIPIO')
			, @NB_MUNICIPIO NVARCHAR(100)					= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'NB_MUNICIPIO')
			, @CL_COLONIA NVARCHAR(30)						= (SELECT TOP 1 CL_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'NB_COLONIA')
			, @NB_COLONIA NVARCHAR(100)						= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'NB_COLONIA')
			, @NB_CALLE NVARCHAR(100)						= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'NB_CALLE')
			, @NO_EXTERIOR NVARCHAR(20)						= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'NO_EXTERIOR')
			, @NO_INTERIOR NVARCHAR(20)						= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'NO_INTERIOR')
			, @XML_TELEFONOS XML							= (SELECT TOP 1 XML_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'LS_TELEFONOS')
			, @CL_CORREO_ELECTRONICO NVARCHAR(200)			= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'CL_CORREO_ELECTRONICO')
			, @NB_LICENCIA NVARCHAR(30)						= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'NB_LICENCIA')
			, @DS_VEHICULO NVARCHAR(100)					= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'DS_VEHICULO')
			, @CL_CARTILLA_MILITAR NVARCHAR(30)				= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'CL_CARTILLA_MILITAR')
			, @CL_CEDULA_PROFESIONAL NVARCHAR(30)			= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'CL_CEDULA_PROFESIONAL')
			, @XML_DATOS_FAMILIARES XML						= (SELECT TOP 1 XML_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'LS_PARIENTES')
			, @XML_FORMACION_ACADEMICA XML					= (SELECT TOP 1 XML_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'LS_FORMACION_ACADEMICA')
			, @XML_IDIOMAS XML								= (SELECT TOP 1 XML_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'LS_IDIOMAS')
			, @XML_EXPERIENCIA_PROFESIONAL XML				= (SELECT TOP 1 XML_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'LS_EXPERIENCIA_LABORAL')
			, @XML_AREA_INTERES XML							= (SELECT TOP 1 XML_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'LS_AREAS_INTERES')
			, @XML_COMPETENCIAS XML							= (SELECT TOP 1 XML_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'LS_COMPETENCIAS')
			, @DS_COMPETENCIAS_ADICIONALES NVARCHAR(1000)	= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'DS_COMPETENCIAS_ADICIONALES')
			, @DS_DISPONIBILIDAD NVARCHAR(100)				= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'DS_DISPONIBILIDAD')
			, @CL_DISPONIBILIDAD_VIAJE NVARCHAR(20)			= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'CL_DISPONIBILIDAD_VIAJE')
			, @MN_SUELDO DECIMAL(13,2)						= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'MN_SUELDO')
			, @XML_PERFILES_REDES_SOCIALES XML				= (SELECT TOP 1 XML_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'LS_PERFILES')
			, @DS_COMENTARIO NVARCHAR(1000)					= (SELECT TOP 1 NB_VALOR FROM @T_VALORES WHERE ID_CAMPO = 'DS_COMENTARIO')
			, @FE_SISTEMA DATETIME							= GETDATE()

			SET @XML_TELEFONOS = (
				SELECT T.value('@NO_TELEFONO', 'NVARCHAR(MAX)') AS '@NO_TELEFONO'
					, T.value('@CL_TELEFONO_TIPO', 'NVARCHAR(MAX)') AS '@CL_TIPO'
				FROM @XML_TELEFONOS.nodes('/DATOS/ITEM') X(T)
				FOR XML PATH('TELEFONO'), ROOT('TELEFONOS')
			)

			SET @XML_PERFILES_REDES_SOCIALES = (
				SELECT T.value('@NB_PERFIL', 'NVARCHAR(200)') AS '@NB_PERFIL'
					, T.value('@CL_RED_SOCIAL', 'NVARCHAR(20)') AS '@CL_RED_SOCIAL'
				FROM @XML_PERFILES_REDES_SOCIALES.nodes('/DATOS/ITEM') X(T)
				FOR XML PATH('RED_SOCIAL'), ROOT('REDES_SOCIALES')
			)

		--00000000000000000000000000000000000000	CANDIDATOS

		DECLARE @T_CANDIDATO TABLE (
			ID_CANDIDATO INT
		)

		MERGE ADM.C_CANDIDATO AS T
		USING (
			SELECT @ID_CANDIDATO
		) AS S (
			ID_CANDIDATO
		) ON (T.ID_CANDIDATO = S.ID_CANDIDATO)
		WHEN MATCHED THEN
			UPDATE SET NB_CANDIDATO = @NB_CANDIDATO
				, NB_APELLIDO_PATERNO = @NB_APELLIDO_PATERNO
				, NB_APELLIDO_MATERNO = @NB_APELLIDO_MATERNO
				, CL_RFC = @CL_RFC
				, CL_CURP = @CL_CURP
				, CL_NSS = @CL_NSS
				, DS_LUGAR_NACIMIENTO = @DS_LUGAR_NACIMIENTO
				, CL_NACIONALIDAD = @NB_NACIONALIDAD
				, FE_NACIMIENTO = @FE_NACIMIENTO
				, CL_GENERO = @CL_GENERO
				, CL_ESTADO_CIVIL = @CL_ESTADO_CIVIL
				, NB_CONYUGUE = @NB_CONYUGUE
				, CL_CODIGO_POSTAL = @CL_CODIGO_POSTAL
				, CL_ESTADO = @CL_ESTADO
				, NB_ESTADO = @NB_ESTADO
				, CL_MUNICIPIO = @CL_MUNICIPIO
				, NB_MUNICIPIO = @NB_MUNICIPIO
				, CL_COLONIA = @CL_COLONIA
				, NB_COLONIA = @NB_COLONIA
				, NB_CALLE = @NB_CALLE
				, NO_EXTERIOR = @NO_EXTERIOR
				, NO_INTERIOR = @NO_INTERIOR
				, XML_TELEFONOS = @XML_TELEFONOS
				, CL_CORREO_ELECTRONICO = @CL_CORREO_ELECTRONICO
				, NB_LICENCIA = @NB_LICENCIA
				, DS_VEHICULO = @DS_VEHICULO
				, CL_CARTILLA_MILITAR = @CL_CARTILLA_MILITAR
				, CL_CEDULA_PROFESIONAL = @CL_CEDULA_PROFESIONAL
				, DS_DISPONIBILIDAD = @DS_DISPONIBILIDAD
				, CL_DISPONIBILIDAD_VIAJE = @CL_DISPONIBILIDAD_VIAJE
				, MN_SUELDO = @MN_SUELDO
				, XML_PERFIL_RED_SOCIAL = @XML_PERFILES_REDES_SOCIALES
				, DS_COMENTARIO = @DS_COMENTARIO
				, DS_COMPETENCIAS_ADICIONALES = @DS_COMPETENCIAS_ADICIONALES
				, FE_MODIFICACION = @FE_SISTEMA
				, CL_USUARIO_APP_MODIFICA = @PIN_CL_USUARIO
				, NB_PROGRAMA_MODIFICA = @PIN_NB_PROGRAMA
		WHEN NOT MATCHED THEN
			INSERT (
				NB_CANDIDATO
				, NB_APELLIDO_PATERNO
				, NB_APELLIDO_MATERNO
				, CL_RFC
				, CL_CURP
				, CL_NSS
				, DS_LUGAR_NACIMIENTO
				, CL_NACIONALIDAD
				, FE_NACIMIENTO
				, CL_GENERO
				, CL_ESTADO_CIVIL
				, NB_CONYUGUE
				, CL_CODIGO_POSTAL
				, CL_ESTADO
				, NB_ESTADO
				, CL_MUNICIPIO
				, NB_MUNICIPIO
				, CL_COLONIA
				, NB_COLONIA
				, NB_CALLE
				, NO_EXTERIOR
				, NO_INTERIOR
				, XML_TELEFONOS
				, CL_CORREO_ELECTRONICO
				, NB_LICENCIA
				, DS_VEHICULO
				, CL_CARTILLA_MILITAR
				, CL_CEDULA_PROFESIONAL
				, DS_DISPONIBILIDAD
				, CL_DISPONIBILIDAD_VIAJE
				, MN_SUELDO
				, XML_PERFIL_RED_SOCIAL
				, DS_COMENTARIO
				, DS_COMPETENCIAS_ADICIONALES
				, FG_ACTIVO
				, FE_CREACION
				, CL_USUARIO_APP_CREA
				, NB_PROGRAMA_CREA
			) VALUES (
				@NB_CANDIDATO
				, @NB_APELLIDO_PATERNO
				, @NB_APELLIDO_MATERNO
				, @CL_RFC
				, @CL_CURP
				, @CL_NSS
				, @DS_LUGAR_NACIMIENTO
				, @NB_NACIONALIDAD
				, @FE_NACIMIENTO
				, @CL_GENERO
				, @CL_ESTADO_CIVIL
				, @NB_CONYUGUE
				, @CL_CODIGO_POSTAL
				, @CL_ESTADO
				, @NB_ESTADO
				, @CL_MUNICIPIO
				, @NB_MUNICIPIO
				, @CL_COLONIA
				, @NB_COLONIA
				, @NB_CALLE
				, @NO_EXTERIOR
				, @NO_INTERIOR
				, @XML_TELEFONOS
				, @CL_CORREO_ELECTRONICO
				, @NB_LICENCIA
				, @DS_VEHICULO
				, @CL_CARTILLA_MILITAR
				, @CL_CEDULA_PROFESIONAL
				, @DS_DISPONIBILIDAD
				, @CL_DISPONIBILIDAD_VIAJE
				, @MN_SUELDO
				, @XML_PERFILES_REDES_SOCIALES
				, @DS_COMENTARIO
				, @DS_COMPETENCIAS_ADICIONALES
				, 1
				, @FE_SISTEMA
				, @PIN_CL_USUARIO
				, @PIN_NB_PROGRAMA
			)
		OUTPUT ISNULL(INSERTED.ID_CANDIDATO, @ID_CANDIDATO) INTO @T_CANDIDATO (ID_CANDIDATO);

		SET @ID_CANDIDATO = (SELECT TOP 1 ID_CANDIDATO FROM @T_CANDIDATO)

		--00000000000000000000000000000000000000	SOLICITUD

		DECLARE @T_SOLICITUD TABLE (
			ID_SOLICITUD INT
		)

		MERGE ADM.K_SOLICITUD AS T
		USING (
			SELECT @PIN_ID_SOLICITUD
				, @ID_CANDIDATO
				, @PIN_XML_SOLICITUD_PLANTILLA
		) AS S (
			ID_SOLICITUD
			, ID_CANDIDATO
			, XML_SOLICITUD_PLANTILLA
		) ON (T.ID_SOLICITUD = S.ID_SOLICITUD)
		WHEN MATCHED THEN
			UPDATE SET XML_PLANTILLA_SOLICITUD = @PIN_XML_SOLICITUD_PLANTILLA
		WHEN NOT MATCHED THEN
			INSERT (
				ID_CANDIDATO
				, CL_ACCESO_EVALUACION
				, CL_SOLICITUD_ESTATUS
				, XML_PLANTILLA_SOLICITUD
				, FE_CREACION
				, CL_USUARIO_APP_CREA
				, NB_PROGRAMA_CREA
			) VALUES (
				@ID_CANDIDATO
				, NEWID()
				, 'CREADA'
				, @PIN_XML_SOLICITUD_PLANTILLA
				, @FE_SISTEMA
				, @PIN_CL_USUARIO
				, @PIN_NB_PROGRAMA
			)
		OUTPUT INSERTED.ID_SOLICITUD INTO @T_SOLICITUD;

		DECLARE @CL_SEQ_SOLICITUD NVARCHAR(20)
			, @CL_SEQ_RETORNO NVARCHAR(40)		--APLICA PARA REGRESAR UN NÚMERO DE (ERROR, ERROR CONTROLADO, O IDENTIFICADOR)
			, @DS_SEQ_MENSAJE NVARCHAR(200)	--APLICA PARA REGRESAR EL TEXTO A MOSTRAR EN LA APLICACIÓN (ERROR, MENSAJE CONTROLADO)
			
		IF EXISTS(SELECT TOP 1 1 FROM @T_SOLICITUD) BEGIN
			EXEC ADM.SP_OBTIENE_SECUENCIA 
				@P_NO_SECUENCIA = @CL_SEQ_SOLICITUD OUT
				, @POUT_CLAVE_RETORNO = @CL_SEQ_RETORNO OUT
				, @POUT_MENSAJE_RETORNO = @DS_SEQ_MENSAJE OUT
				, @P_CL_SECUENCIA = N'K_SOLICITUD'
				, @P_ID_EMPRESA = NULL

			UPDATE KS SET CL_SOLICITUD = @CL_SEQ_SOLICITUD
			FROM ADM.K_SOLICITUD KS
				INNER JOIN @T_SOLICITUD TS
					ON KS.ID_SOLICITUD = TS.ID_SOLICITUD
		END

		--00000000000000000000000000000000000000	DATOS FAMILIARES

		MERGE ADM.C_PARIENTE AS T
		USING (
			SELECT T.value('@ID_PARIENTE', 'INT')
				, T.value('@NB_PARIENTE', 'NVARCHAR(200)')
				, T.value('@CL_PARENTESCO', 'NVARCHAR(20)')
				, T.value('@FE_NACIMIENTO', 'DATE')
				, T.value('@CL_OCUPACION', 'NVARCHAR(50)')
				, T.value('@FG_DEPENDIENTE', 'BIT')
			FROM @XML_DATOS_FAMILIARES.nodes('/DATOS/ITEM') X(T)
		) AS S (
			ID_PARIENTE
			, NB_PARIENTE
			, CL_PARENTESCO
			, FE_NACIMIENTO
			, CL_OCUPACION
			, FG_DEPENDIENTE
		) ON T.ID_PARIENTE = S.ID_PARIENTE
		WHEN MATCHED THEN
			UPDATE SET NB_PARIENTE = S.NB_PARIENTE
				, CL_PARENTESCO = S.CL_PARENTESCO
				, FE_NACIMIENTO = S.FE_NACIMIENTO
				, CL_OCUPACION = S.CL_OCUPACION
				, FG_DEPENDIENTE = S.FG_DEPENDIENTE
				, FE_MODIFICACION = @FE_SISTEMA
				, CL_USUARIO_APP_MODIFICA = @PIN_CL_USUARIO
				, NB_PROGRAMA_MODIFICA = @PIN_NB_PROGRAMA
		WHEN NOT MATCHED THEN
			INSERT (
				NB_PARIENTE
				, CL_PARENTESCO
				, FE_NACIMIENTO
				, ID_CANDIDATO
				, CL_OCUPACION
				, FG_DEPENDIENTE
				, FG_ACTIVO
				, FE_CREACION
				, CL_USUARIO_APP_CREA
				, NB_PROGRAMA_CREA
			) VALUES (
				S.NB_PARIENTE
				, S.CL_PARENTESCO
				, S.FE_NACIMIENTO
				, @ID_CANDIDATO
				, S.CL_OCUPACION
				, S.FG_DEPENDIENTE
				, 1
				, @FE_SISTEMA
				, @PIN_CL_USUARIO
				, @PIN_NB_PROGRAMA
			)
		WHEN NOT MATCHED BY SOURCE THEN
			DELETE
		;

		--00000000000000000000000000000000000000	FORMACIÓN ACADÉMICA

		MERGE IDP.C_EMPLEADO_ESCOLARIDAD AS T
		USING (
			SELECT T.value('@ID_EMPLEADO_ESCOLARIDAD', 'INT')
				, T.value('@CL_ESCOLARIDAD', 'INT')
				, T.value('@NB_INSTITUCION', 'NVARCHAR(50)')
				, T.value('@FE_PERIODO_INICIO', 'DATE')
				, T.value('@FE_PERIODO_FIN', 'DATE')
				, T.value('@CL_ESTADO_ESCOLARIDAD', 'NVARCHAR(10)')
			FROM @XML_FORMACION_ACADEMICA.nodes('/DATOS/ITEM') X(T)
		) AS S (
			ID_EMPLEADO_ESCOLARIDAD
			, ID_ESCOLARIDAD
			, NB_INSTITUCION
			, FE_PERIODO_INICIO
			, FE_PERIODO_FIN
			, CL_ESTADO_ESCOLARIDAD
		) ON T.ID_EMPLEADO_ESCOLARIDAD = S.ID_EMPLEADO_ESCOLARIDAD
		WHEN MATCHED THEN
			UPDATE SET ID_ESCOLARIDAD = S.ID_ESCOLARIDAD
				, NB_INSTITUCION = S.NB_INSTITUCION
				, FE_PERIODO_INICIO = S.FE_PERIODO_INICIO
				, FE_PERIODO_FIN = S.FE_PERIODO_FIN
				, CL_ESTADO_ESCOLARIDAD = S.CL_ESTADO_ESCOLARIDAD
				, FE_MODIFICACION = @FE_SISTEMA
				, CL_USUARIO_APP_MODIFICA = @PIN_CL_USUARIO
				, NB_PROGRAMA_MODIFICA = @PIN_NB_PROGRAMA
		WHEN NOT MATCHED THEN
			INSERT (
				ID_CANDIDATO
				, ID_ESCOLARIDAD
				, NB_INSTITUCION
				, FE_PERIODO_INICIO
				, FE_PERIODO_FIN
				, CL_ESTADO_ESCOLARIDAD
				, FE_CREACION
				, CL_USUARIO_APP_CREA
				, NB_PROGRAMA_CREA
			) VALUES (
				@ID_CANDIDATO
				, S.ID_ESCOLARIDAD
				, S.NB_INSTITUCION
				, S.FE_PERIODO_INICIO
				, S.FE_PERIODO_FIN
				, S.CL_ESTADO_ESCOLARIDAD
				, @FE_SISTEMA
				, @PIN_CL_USUARIO
				, @PIN_NB_PROGRAMA
			)
		WHEN NOT MATCHED BY SOURCE THEN 
			DELETE
		;

		--00000000000000000000000000000000000000	IDIOMAS

		MERGE ADM.C_EMPLEADO_IDIOMA AS T 
		USING (
			SELECT T.value('@ID_EMPLEADO_IDIOMA', 'INT')
				, T.value('@ID_IDIOMA', 'INT')
				, T.value('@PR_CONVERSACIONAL', 'DECIMAL(5,2)')
				, T.value('@PR_TRADUCCION', 'DECIMAL(5,2)')
				, T.value('@CL_INSTITUCION', 'INT')
				, T.value('@NO_PUNTAJE', 'DECIMAL(8,3)')
			FROM @XML_IDIOMAS.nodes('/DATOS/ITEM') X(T)
		) AS S (
			ID_EMPLEADO_IDIOMA
			, ID_IDIOMA
			, PR_CONVERSACIONAL
			, PR_LECTURA
			, CL_INSTITUCION
			, NO_PUNTAJE
		) ON T.ID_EMPLEADO_IDIOMA = S.ID_EMPLEADO_IDIOMA
		WHEN MATCHED THEN
			UPDATE SET ID_IDIOMA = S.ID_IDIOMA
				, PR_CONVERSACIONAL = S.PR_CONVERSACIONAL
				, PR_LECTURA = S.PR_LECTURA
				, CL_INSTITUCION = S.CL_INSTITUCION
				, NO_PUNTAJE = S.NO_PUNTAJE
				, FE_MODIFICACION = @FE_SISTEMA
				, CL_USUARIO_APP_MODIFICA = @PIN_CL_USUARIO
				, NB_PROGRAMA_MODIFICA = @PIN_NB_PROGRAMA
		WHEN NOT MATCHED THEN
			INSERT (
				ID_CANDIDATO
				, ID_IDIOMA
				, PR_CONVERSACIONAL
				, PR_LECTURA
				, CL_INSTITUCION
				, NO_PUNTAJE
				, FE_CREACION
				, CL_USUARIO_APP_CREA
				, NB_PROGRAMA_CREA
			) VALUES (
				@ID_CANDIDATO
				, S.ID_IDIOMA
				, S.PR_CONVERSACIONAL
				, S.PR_LECTURA
				, S.CL_INSTITUCION
				, S.NO_PUNTAJE
				, @FE_SISTEMA
				, @PIN_CL_USUARIO
				, @PIN_NB_PROGRAMA
			)
		WHEN NOT MATCHED BY SOURCE THEN
			DELETE
		;

		--00000000000000000000000000000000000000	EXPERIENCIA LABORAL

		MERGE ADM.K_EXPERIENCIA_LABORAL AS T
		USING (
			SELECT T.value('@ID_EXPERIENCIA_LABORAL', 'INT')
				, T.value('@NB_EMPRESA', 'NVARCHAR(100)')
				, T.value('@NB_GIRO', 'NVARCHAR(50)')
				, T.value('@NB_WEBSITE', 'NVARCHAR(500)')
				, T.value('@FE_INICIO', 'DATE')
				, T.value('@FE_FIN', 'DATE')
				, T.value('@NB_PUESTO', 'NVARCHAR(100)')
				, T.value('@DS_FUNCIONES', 'NVARCHAR(1000)')
				, T.value('@MN_ULTIMO_SUELDO', 'DECIMAL(13,2)')
				, T.value('@NB_CONTACTO', 'NVARCHAR(100)')
				, T.value('@NB_PUESTO_CONTACTO', 'NVARCHAR(100)')
				, T.value('@NO_TELEFONO_CONTACTO', 'NVARCHAR(20)')
				, T.value('@CL_CORREO_ELECTRONICO', 'NVARCHAR(200)')
			FROM @XML_EXPERIENCIA_PROFESIONAL.nodes('/DATOS/ITEM') X(T)
		) AS S (
			ID_EXPERIENCIA_LABORAL
			, NB_EMPRESA
			, NB_GIRO
			, NB_WEBSITE
			, FE_INICIO
			, FE_FIN
			, NB_PUESTO
			, DS_FUNCIONES
			, MN_ULTIMO_SUELDO
			, NB_CONTACTO
			, NB_PUESTO_CONTACTO
			, NO_TELEFONO_CONTACTO
			, CL_CORREO_ELECTRONICO
		) ON T.ID_EXPERIENCIA_LABORAL = S.ID_EXPERIENCIA_LABORAL
		WHEN MATCHED THEN
			UPDATE SET NB_EMPRESA = S.NB_EMPRESA
				, NB_GIRO = S.NB_GIRO
				, NB_WEBSITE = S.NB_WEBSITE
				, FE_INICIO = S.FE_INICIO
				, FE_FIN = S.FE_FIN
				, NB_PUESTO = S.NB_PUESTO
				, DS_FUNCIONES = S.DS_FUNCIONES
				, MN_ULTIMO_SUELDO = S.MN_ULTIMO_SUELDO
				, NB_CONTACTO = S.NB_CONTACTO
				, NB_PUESTO_CONTACTO = S.NB_PUESTO_CONTACTO
				, NO_TELEFONO_CONTACTO = S.NO_TELEFONO_CONTACTO
				, CL_CORREO_ELECTRONICO = S.CL_CORREO_ELECTRONICO
				, FE_MODIFICACION = @FE_SISTEMA
				, CL_USUARIO_APP_MODIFICA = @PIN_CL_USUARIO
				, NB_PROGRAMA_MODIFICA = @PIN_NB_PROGRAMA
		WHEN NOT MATCHED THEN
			INSERT (
				ID_CANDIDATO
				, NB_EMPRESA
				, NB_GIRO
				, NB_WEBSITE
				, FE_INICIO
				, FE_FIN
				, NB_PUESTO
				, DS_FUNCIONES
				, MN_ULTIMO_SUELDO
				, NB_CONTACTO
				, NB_PUESTO_CONTACTO
				, NO_TELEFONO_CONTACTO
				, CL_CORREO_ELECTRONICO
				, FE_CREACION
				, CL_USUARIO_APP_CREA
				, NB_PROGRAMA_CREA
			) VALUES (
				@ID_CANDIDATO
				, S.NB_EMPRESA
				, S.NB_GIRO
				, S.NB_WEBSITE
				, S.FE_INICIO
				, S.FE_FIN
				, S.NB_PUESTO
				, S.DS_FUNCIONES
				, S.MN_ULTIMO_SUELDO
				, S.NB_CONTACTO
				, S.NB_PUESTO_CONTACTO
				, S.NO_TELEFONO_CONTACTO
				, S.CL_CORREO_ELECTRONICO
				, @FE_SISTEMA
				, @PIN_CL_USUARIO
				, @PIN_NB_PROGRAMA
			)
		WHEN NOT MATCHED BY SOURCE THEN
			DELETE
		;

		--00000000000000000000000000000000000000	ÁREAS DE INTERÉS

		MERGE ADM.K_AREA_INTERES AS T
		USING (
			SELECT T.value('@ID_CANDIDATO_AREA_INTERES', 'INT')
				, T.value('@ID_AREA_INTERES', 'INT')
			FROM @XML_AREA_INTERES.nodes('/DATOS/ITEM') X(T)
		) AS S (
			ID_CANDIDATO_AREA_INTERES
			, ID_AREA_INTERES
		) ON T.ID_CANDIDATO_AREA_INTERES = S.ID_CANDIDATO_AREA_INTERES
		WHEN MATCHED THEN
			UPDATE SET ID_AREA_INTERES = S.ID_AREA_INTERES
				, FE_MODIFICACION = @FE_SISTEMA
				, CL_USUARIO_APP_MODIFICA = @PIN_CL_USUARIO
				, NB_PROGRAMA_MODIFICA = @PIN_NB_PROGRAMA
		WHEN NOT MATCHED THEN
			INSERT (
				ID_CANDIDATO
				, ID_AREA_INTERES
				, FE_CREACION
				, CL_USUARIO_APP_CREA
				, NB_PROGRAMA_CREA
			) VALUES (
				@ID_CANDIDATO
				, S.ID_AREA_INTERES
				, @FE_SISTEMA
				, @PIN_CL_USUARIO
				, @PIN_NB_PROGRAMA
			)
		WHEN NOT MATCHED BY SOURCE THEN
			DELETE
		;

		--00000000000000000000000000000000000000	COMPETENCIAS

		MERGE IDP.K_CANDIDATO_COMPETENCIA AS T
		USING (
			SELECT T.value('@ID_CANDIDATO_COMPETENCIA', 'INT')
				, T.value('@ID_COMPETENCIA', 'INT')
			FROM @XML_COMPETENCIAS.nodes('/DATOS/ITEM') X(T)
		) AS S (
			ID_CANDIDATO_COMPETENCIA
			, ID_COMPETENCIA
		) ON T.ID_CANDIDATO_COMPETENCIA = S.ID_CANDIDATO_COMPETENCIA
		WHEN MATCHED THEN
			UPDATE SET ID_COMPETENCIA = S.ID_COMPETENCIA
				, FE_MODIFICACION = @FE_SISTEMA
				, CL_USUARIO_APP_MODIFICA = @PIN_CL_USUARIO
				, NB_PROGRAMA_MODIFICA = @PIN_NB_PROGRAMA
		WHEN NOT MATCHED THEN
			INSERT (
				ID_CANDIDATO
				, ID_COMPETENCIA
				, FE_CREACION
				, CL_USUARIO_APP_CREA
				, NB_PROGRAMA_CREA
			) VALUES (
				@ID_CANDIDATO
				, S.ID_COMPETENCIA
				, @FE_SISTEMA
				, @PIN_CL_USUARIO
				, @PIN_NB_PROGRAMA
			)
		WHEN NOT MATCHED BY SOURCE THEN
			DELETE
		;

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
	--SE INSERTA EL ERROR EN LA TABLA		
		DECLARE @ERROR_CLAVE INT  = 	ERROR_NUMBER()
		DECLARE @ERROR_MENSAJE NVARCHAR(250)

		IF @ERROR_CLAVE = 547
			SET @ERROR_MENSAJE = 'Registro asociado'
		ELSE
			SET @ERROR_MENSAJE = 'Ocurrió un error al procesar el registro'

		SET @XML_RESULTADO = DBO.F_ERROR_CREAR_ENCABEZADO( @@ROWCOUNT, @ERROR_CLAVE, 'ERROR')
		SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_MENSAJES(@XML_RESULTADO, @ERROR_MENSAJE, 'ES')
		SET @XML_RESULTADO = DBO.F_ERROR_INSERTAR_MENSAJES(@XML_RESULTADO, @ERROR_MENSAJE, 'EN')
	END CATCH	
END 