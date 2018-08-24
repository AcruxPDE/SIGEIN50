﻿-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Julio Díaz
-- CREATE date: 24/12/2015
-- Description: Obtiene los datos de la solicitud con base en la plantilla para el formulario dinámico
-- =============================================
CREATE PROCEDURE [IDP].[SPE_OBTIENE_SOLICITUD_PLANTILLA]
	@PIN_ID_PLANTILLA INT = NULL
	, @PIN_ID_SOLICITUD INT = NULL
AS
BEGIN

	DECLARE @XML_SOLICITUD_PLANTILLA XML
		, @ID_PLANTILLA INT = @PIN_ID_PLANTILLA

	IF @ID_PLANTILLA IS NULL BEGIN
		SET @ID_PLANTILLA = (
			SELECT TOP 1 ID_PLANTILLA_SOLICITUD FROM ADM.C_PLANTILLA_FORMULARIO WHERE FG_GENERAL = 1
		)
	END
	
	SELECT @XML_SOLICITUD_PLANTILLA = XML_PLANTILLA_SOLICITUD FROM ADM.C_PLANTILLA_FORMULARIO WHERE ID_PLANTILLA_SOLICITUD = @ID_PLANTILLA

	DECLARE @ID_CANDIDATO INT
		, @NB_CANDIDATO NVARCHAR(100)
		, @NB_APELLIDO_PATERNO NVARCHAR(100)
		, @NB_APELLIDO_MATERNO NVARCHAR(100)
		, @CL_RFC NVARCHAR(20)
		, @CL_CURP NVARCHAR(20)
		, @CL_NSS NVARCHAR(20)
		, @DS_LUGAR_NACIMIENTO NVARCHAR(200)
		, @NB_NACIONALIDAD NVARCHAR(30)
		, @FE_NACIMIENTO DATE

		, @CL_GENERO NVARCHAR(1)
		, @XML_CL_GENERO XML

		, @CL_ESTADO_CIVIL NVARCHAR(20)
		, @XML_CL_ESTADO_CIVIL XML

		, @NB_CONYUGUE NVARCHAR(100)
		, @CL_CODIGO_POSTAL NVARCHAR(10)

		, @CL_ESTADO NVARCHAR(10)
		, @NB_ESTADO NVARCHAR(100)
		, @XML_NB_ESTADO XML

		, @CL_MUNICIPIO NVARCHAR(10)
		, @NB_MUNICIPIO NVARCHAR(100)
		, @XML_NB_MUNICIPIO XML

		, @CL_COLONIA NVARCHAR(30)
		, @NB_COLONIA NVARCHAR(100)
		, @XML_NB_COLONIA XML

		, @NB_CALLE NVARCHAR(100)
		, @NO_EXTERIOR NVARCHAR(20)
		, @NO_INTERIOR NVARCHAR(20)

		, @XML_TELEFONOS XML
		, @XML_NO_TELEFONOS XML
		, @XML_NO_TELEFONO_TIPOS XML

		, @CL_CORREO_ELECTRONICO NVARCHAR(200)
		, @NB_LICENCIA NVARCHAR(30)
		, @DS_VEHICULO NVARCHAR(100)
		, @CL_CARTILLA_MILITAR NVARCHAR(30)
		, @CL_CEDULA_PROFESIONAL NVARCHAR(30)

		, @XML_CL_PARENTESCO XML
		, @XML_CL_OCUPACION XML
		, @XML_DATOS_FAMILIARES XML

		, @XML_CL_NIVEL_ESCOLARIDAD XML
		, @XML_ID_ESCOLARIDAD XML
		, @XML_CL_ESTADO_ESCOLARIDAD XML
		, @XML_FORMACION_ACADEMICA XML

		, @XML_ID_IDIOMA XML
		, @XML_PR_DOMINIO_IDIOMA XML
		, @XML_CL_CERTIFICADO_IDIOMA XML
		, @XML_IDIOMAS XML

		, @XML_EXPERIENCIA_PROFESIONAL XML

		, @XML_ID_AREA_INTERES XML
		, @XML_AREA_INTERES XML

		, @XML_ID_COMPETENCIAS XML
		, @XML_COMPETENCIAS XML

		, @DS_COMPETENCIAS_ADICIONALES NVARCHAR(1000)
		, @DS_DISPONIBILIDAD NVARCHAR(100)
		, @CL_DISPONIBILIDAD_VIAJE NVARCHAR(20)

		, @XML_CL_RED_SOCIAL XML
		, @XML_RED_SOCIAL XML
		, @XML_PERFILES_REDES_SOCIALES XML


		, @DS_COMENTARIO NVARCHAR(1000)

	SELECT @ID_CANDIDATO = KS.ID_CANDIDATO
		, @NB_CANDIDATO = CC.NB_CANDIDATO
		, @NB_APELLIDO_PATERNO = CC.NB_APELLIDO_PATERNO
		, @NB_APELLIDO_MATERNO = CC.NB_APELLIDO_MATERNO
		, @CL_RFC = CC.CL_RFC
		, @CL_CURP = CC.CL_CURP
		, @CL_NSS = CC.CL_NSS
		, @DS_LUGAR_NACIMIENTO = CC.DS_LUGAR_NACIMIENTO
		, @NB_NACIONALIDAD = CC.CL_NACIONALIDAD
		, @FE_NACIMIENTO = CC.FE_NACIMIENTO
		, @XML_CL_GENERO = CC.CL_GENERO
		, @XML_CL_ESTADO_CIVIL = CC.CL_ESTADO_CIVIL
		, @NB_CONYUGUE = CC.NB_CONYUGUE
		, @CL_CODIGO_POSTAL = CC.CL_CODIGO_POSTAL
		, @CL_ESTADO = CC.CL_ESTADO
		, @NB_ESTADO = CC.NB_ESTADO
		, @CL_MUNICIPIO = CC.CL_MUNICIPIO
		, @NB_MUNICIPIO = CC.NB_MUNICIPIO
		, @CL_COLONIA = CC.CL_COLONIA
		, @NB_COLONIA = CC.NB_COLONIA
		, @NB_CALLE = CC.NB_CALLE
		, @NO_EXTERIOR = CC.NO_EXTERIOR
		, @NO_INTERIOR = CC.NO_INTERIOR
		, @XML_TELEFONOS = CC.XML_TELEFONOS
		, @CL_CORREO_ELECTRONICO = CC.CL_CORREO_ELECTRONICO
		, @NB_LICENCIA = CC.NB_LICENCIA
		, @DS_VEHICULO = CC.DS_VEHICULO
		, @CL_CARTILLA_MILITAR = CC.CL_CARTILLA_MILITAR
		, @CL_CEDULA_PROFESIONAL = CC.CL_CEDULA_PROFESIONAL
		, @DS_COMPETENCIAS_ADICIONALES = CC.DS_COMPETENCIAS_ADICIONALES
		, @DS_DISPONIBILIDAD = CC.DS_DISPONIBILIDAD
		, @CL_DISPONIBILIDAD_VIAJE = CC.CL_DISPONIBILIDAD_VIAJE
		, @XML_RED_SOCIAL = CC.XML_PERFIL_RED_SOCIAL
		, @DS_COMENTARIO = CC.DS_COMENTARIO
	FROM ADM.K_SOLICITUD KS
		INNER JOIN ADM.C_CANDIDATO CC
			ON KS.ID_CANDIDATO = CC.ID_CANDIDATO
			AND KS.ID_SOLICITUD = @PIN_ID_SOLICITUD

	SET @XML_NO_TELEFONOS = (
		SELECT ROW_NUMBER() OVER(ORDER BY T.value('@NO_NUMERO', 'NVARCHAR(20)')) AS '@ID_ITEM'
			, T.value('@NO_TELEFONO', 'NVARCHAR(20)') AS '@NO_TELEFONO'
			, T.value('@CL_TIPO', 'NVARCHAR(20)') AS '@CL_TELEFONO_TIPO'
			, ISNULL(TT.NB_CATALOGO_VALOR, T.value('@CL_TIPO', 'NVARCHAR(20)')) AS '@NB_TELEFONO_TIPO'
		FROM @XML_TELEFONOS.nodes('/TELEFONOS/TELEFONO') X(T)
			LEFT JOIN ADM.F_OBTENER_CATALOGO('TELEFONO_TIPOS') AS TT
				ON T.value('@CL_TIPO', 'NVARCHAR(20)') = TT.CL_CATALOGO_VALOR
		FOR XML PATH('ITEM'), ROOT('DATOS')
	)

	SET @XML_NO_TELEFONO_TIPOS = (
		SELECT TT.CL_CATALOGO_VALOR AS '@NB_VALOR'
			, TT.NB_CATALOGO_VALOR AS '@NB_TEXTO'
		FROM ADM.F_OBTENER_CATALOGO('TELEFONO_TIPOS') AS TT
		FOR XML PATH('ITEM'), ROOT('ITEMS')
	)

	SET @XML_NB_ESTADO = (
		SELECT ISNULL(@CL_ESTADO, 'NA') AS '@NB_VALOR'
			, ISNULL(@NB_ESTADO, 'No seleccionado') AS '@NB_TEXTO'
		FOR XML PATH('NB_ESTADO')
	)

	SET @XML_NB_MUNICIPIO = (
		SELECT ISNULL(@CL_MUNICIPIO, 'NA') AS '@NB_VALOR'
			, ISNULL(@NB_MUNICIPIO, 'No seleccionado') AS '@NB_TEXTO'
		FOR XML PATH('NB_MUNICIPIO')
	)

	SET @XML_NB_COLONIA = (
		SELECT ISNULL(@CL_COLONIA, 'NA') AS '@NB_VALOR'
			, ISNULL(@NB_COLONIA, 'No seleccionado') AS '@NB_TEXTO'
		FOR XML PATH('NB_COLONIA')
	)

	SET @XML_CL_GENERO = (
		SELECT CL_CATALOGO_VALOR AS '@NB_VALOR'
			, NB_CATALOGO_VALOR AS '@NB_TEXTO'
			, CASE WHEN @CL_GENERO = CL_CATALOGO_VALOR THEN 1 ELSE 0 END AS '@FG_SELECCIONADO'
		FROM ADM.F_OBTENER_CATALOGO('GENERO')
		FOR XML PATH('ITEM'), ROOT('ITEMS')
	)

	SET @XML_CL_ESTADO_CIVIL = (
		SELECT CL_CATALOGO_VALOR AS '@NB_VALOR'
			, NB_CATALOGO_VALOR AS '@NB_TEXTO'
			, CASE WHEN @CL_ESTADO_CIVIL = CL_CATALOGO_VALOR THEN 1 ELSE 0 END AS '@FG_SELECCIONADO'
		FROM ADM.F_OBTENER_CATALOGO('EDOCIVIL')
		FOR XML PATH('ITEM'), ROOT('ITEMS')
	)

	SET @XML_CL_PARENTESCO = (
		SELECT CL_CATALOGO_VALOR AS '@NB_VALOR'
			, NB_CATALOGO_VALOR AS '@NB_TEXTO'
			--, CASE WHEN @CL_PARENTESCO = CL_CATALOGO_VALOR THEN 1 ELSE 0 END AS '@FG_SELECCIONADO'
		FROM ADM.F_OBTENER_CATALOGO('PARENTESCO')
		FOR XML PATH('ITEM'), ROOT('ITEMS')
	)

	SET @XML_CL_OCUPACION = (
		SELECT CL_CATALOGO_VALOR AS '@NB_VALOR'
			, NB_CATALOGO_VALOR AS '@NB_TEXTO'
			--, CASE WHEN @CL_OCUPACION = CL_CATALOGO_VALOR THEN 1 ELSE 0 END AS '@FG_SELECCIONADO'
		FROM ADM.F_OBTENER_CATALOGO('OCUPACION')
		FOR XML PATH('ITEM'), ROOT('ITEMS')
	)

	SET @XML_DATOS_FAMILIARES = (
		SELECT NEWID() AS '@ID_ITEM'
			, CP.ID_PARIENTE AS '@ID_PARIENTE'
			, CP.NB_PARIENTE AS '@NB_PARIENTE'
			, CP.CL_PARENTESCO AS '@CL_PARENTESCO'
			, ISNULL(P.NB_CATALOGO_VALOR, CP.CL_PARENTESCO) AS '@NB_PARENTESCO'
			, CONVERT(NVARCHAR(10), CP.FE_NACIMIENTO, 103) AS '@FE_NACIMIENTO'
			, CP.CL_OCUPACION AS '@CL_OCUPACION'
			, ISNULL(O.NB_CATALOGO_VALOR, CP.CL_OCUPACION) AS '@NB_OCUPACION'
			, CP.FG_DEPENDIENTE AS '@FG_DEPENDIENTE'
			, CASE WHEN CP.FG_DEPENDIENTE = 1 THEN 'Sí' ELSE 'No' END AS '@NB_DEPENDIENTE'
		FROM ADM.C_PARIENTE CP
			LEFT JOIN ADM.F_OBTENER_CATALOGO('PARENTESCO') AS P
				ON CP.CL_PARENTESCO = P.CL_CATALOGO_VALOR
			LEFT JOIN ADM.F_OBTENER_CATALOGO('OCUPACION') AS O
				ON CP.CL_OCUPACION = O.CL_CATALOGO_VALOR
		WHERE CP.ID_CANDIDATO = @ID_CANDIDATO
		FOR XML PATH('ITEM'), ROOT('DATOS')
	)

	SET @XML_CL_NIVEL_ESCOLARIDAD = (
		SELECT CNE.CL_NIVEL_ESCOLARIDAD AS '@NB_VALOR'
			, CNE.DS_NIVEL_ESCOLARIDAD AS '@NB_TEXTO'
		FROM ADM.C_NIVEL_ESCOLARIDAD CNE
		FOR XML PATH('ITEM'), ROOT('ITEMS')
	)

	SET @XML_ID_ESCOLARIDAD = (
		SELECT CE.ID_ESCOLARIDAD AS '@NB_VALOR'
			, CE.NB_ESCOLARIDAD AS '@NB_TEXTO'
			, CE.CL_NIVEL_ESCOLARIDAD AS '@NB_VALOR_PADRE'
		FROM ADM.C_ESCOLARIDAD CE
		FOR XML PATH('ITEM'), ROOT('ITEMS')
	)

	SET @XML_CL_ESTADO_ESCOLARIDAD = (
		SELECT VEE.CL_ESTADO_ESCOLARIDAD AS '@NB_VALOR'
			, VEE.NB_ESTADO_ESCOLARIDAD AS '@NB_TEXTO'
		FROM ADM.VW_ESTADO_ESCOLARIDAD VEE
		FOR XML PATH('ITEM'), ROOT('ITEMS')
	)

	SET @XML_FORMACION_ACADEMICA = (
		SELECT NEWID() AS '@ID_ITEM'
			, CEE.ID_EMPLEADO_ESCOLARIDAD AS '@ID_EMPLEADO_ESCOLARIDAD'
			, CE.ID_ESCOLARIDAD AS '@CL_NIVEL_ESCOLARIDAD'
			, ISNULL(CNE.DS_NIVEL_ESCOLARIDAD, CE.CL_NIVEL_ESCOLARIDAD) AS '@NB_NIVEL_ESCOLARIDAD'
			, CEE.NB_INSTITUCION AS '@NB_INSTITUCION'
			, CE.CL_ESCOLARIDAD AS '@CL_ESCOLARIDAD'
			, CE.NB_ESCOLARIDAD AS '@NB_ESCOLARIDAD'
			, CONVERT(NVARCHAR(10), CEE.FE_PERIODO_INICIO, 103) AS '@FE_PERIODO_INICIO'
			, CONVERT(NVARCHAR(10), CEE.FE_PERIODO_FIN, 103) AS '@FE_PERIODO_FIN'
			, CEE.CL_ESTADO_ESCOLARIDAD AS '@CL_ESTADO_ESCOLARIDAD'
			, ISNULL(VEE.NB_ESTADO_ESCOLARIDAD, CEE.CL_ESTADO_ESCOLARIDAD) AS '@NB_ESTADO_ESCOLARIDAD'
		FROM IDP.C_EMPLEADO_ESCOLARIDAD CEE
			LEFT JOIN ADM.C_ESCOLARIDAD CE
				ON CEE.ID_ESCOLARIDAD = CE.ID_ESCOLARIDAD
			LEFT JOIN ADM.C_NIVEL_ESCOLARIDAD CNE
				ON CE.CL_NIVEL_ESCOLARIDAD = CNE.CL_NIVEL_ESCOLARIDAD
			LEFT JOIN ADM.VW_ESTADO_ESCOLARIDAD VEE
				ON CEE.CL_ESTADO_ESCOLARIDAD = VEE.CL_ESTADO_ESCOLARIDAD
		WHERE CEE.ID_CANDIDATO = @ID_CANDIDATO
		FOR XML PATH('ITEM'), ROOT('DATOS')
	)

	SET @XML_ID_IDIOMA = (
		SELECT CI.ID_IDIOMA AS '@NB_VALOR'
			, CI.NB_IDIOMA AS '@NB_TEXTO'
		FROM ADM.C_IDIOMA CI
		FOR XML PATH('ITEM'), ROOT('ITEMS')
	)

	SET @XML_PR_DOMINIO_IDIOMA = (
		SELECT VNDI.PR_DOMINIO AS '@NB_VALOR'
			, VNDI.NB_NIVEL AS '@NB_TEXTO'
		FROM ADM.VW_IDIOMA_NIVEL_DOMINIO VNDI
		FOR XML PATH('ITEM'), ROOT('ITEMS')
	)

	SET @XML_CL_CERTIFICADO_IDIOMA = (
		SELECT VIC.CL_IDIOMA_CERTIFICADO AS '@NB_VALOR'
			, VIC.NB_IDIOMA_CERTIFICADO AS '@NB_TEXTO'
		FROM ADM.VW_IDIOMA_CERTIFICADO VIC
		FOR XML PATH('ITEM'), ROOT('ITEMS')
	)

	SET @XML_IDIOMAS = (
		SELECT NEWID() AS '@ID_ITEM'
			, CEI.ID_EMPLEADO_IDIOMA AS '@ID_EMPLEADO_IDIOMA'
			, CEI.ID_IDIOMA AS '@ID_IDIOMA'
			, CI.NB_IDIOMA AS '@NB_IDIOMA'
			, CEI.PR_CONVERSACIONAL AS '@PR_CONVERSACIONAL'
			, ISNULL(VINDC.NB_NIVEL, CEI.PR_CONVERSACIONAL) AS '@NB_CONVERSACIONAL'
			, CEI.PR_LECTURA AS '@PR_TRADUCCION'
			, ISNULL(VINDT.NB_NIVEL, CEI.PR_LECTURA) AS '@NB_TRADUCCION'
			, CEI.CL_INSTITUCION AS '@CL_INSTITUCION'
			, ISNULL(VIC.NB_IDIOMA_CERTIFICADO, CEI.CL_INSTITUCION) AS '@NB_INSTITUCION'
			, CEI.NO_PUNTAJE AS '@NO_PUNTAJE'
		FROM ADM.C_EMPLEADO_IDIOMA CEI
			LEFT JOIN ADM.C_IDIOMA CI
				ON CEI.ID_IDIOMA = CI.ID_IDIOMA
			LEFT JOIN ADM.VW_IDIOMA_NIVEL_DOMINIO VINDC
				ON CEI.PR_CONVERSACIONAL = VINDC.PR_DOMINIO
			LEFT JOIN ADM.VW_IDIOMA_NIVEL_DOMINIO VINDT
				ON CEI.PR_LECTURA = VINDT.PR_DOMINIO
			LEFT JOIN ADM.VW_IDIOMA_CERTIFICADO VIC
				ON CEI.CL_INSTITUCION = VIC.CL_IDIOMA_CERTIFICADO
		WHERE CEI.ID_CANDIDATO = @ID_CANDIDATO
		FOR XML PATH('ITEM'), ROOT('DATOS')
	)

	SET @XML_EXPERIENCIA_PROFESIONAL = (
		SELECT NEWID() AS '@ID_ITEM'
			, KEL.ID_EXPERIENCIA_LABORAL AS '@ID_EXPERIENCIA_LABORAL'
			, KEL.NB_EMPRESA AS '@NB_EMPRESA'
			, KEL.NB_GIRO AS '@NB_GIRO'
			, KEL.NB_WEBSITE AS '@NB_WEBSITE'
			, CONVERT(NVARCHAR(10), KEL.FE_INICIO, 103) AS '@FE_INICIO'
			, CONVERT(NVARCHAR(10), FE_FIN, 103) AS '@FE_FIN'
			, KEL.NB_PUESTO AS '@NB_PUESTO'
			, KEL.DS_FUNCIONES AS '@DS_FUNCIONES'
			, KEL.MN_ULTIMO_SUELDO AS '@MN_ULTIMO_SUELDO'
			, KEL.NB_CONTACTO AS '@NB_CONTACTO'
			, KEL.NB_PUESTO_CONTACTO AS '@NB_PUESTO_CONTACTO'
			, KEL.NO_TELEFONO_CONTACTO AS '@NO_TELEFONO_CONTACTO'
			, KEL.CL_CORREO_ELECTRONICO AS '@CL_CORREO_ELECTRONICO'
		FROM ADM.K_EXPERIENCIA_LABORAL KEL
		WHERE KEL.ID_CANDIDATO = @ID_CANDIDATO
		FOR XML PATH('ITEM'), ROOT('DATOS')
	)

	SET @XML_ID_AREA_INTERES = (
		SELECT ID_AREA_INTERES AS '@NB_VALOR'
			, NB_AREA_INTERES AS '@NB_TEXTO'
		FROM ADM.C_AREA_INTERES CAI
		FOR XML PATH('ITEM'), ROOT('ITEMS')
	)

	SET @XML_AREA_INTERES = (
		SELECT NEWID() AS '@ID_ITEM'
			, KAI.ID_CANDIDATO_AREA_INTERES AS '@ID_CANDIDATO_AREA_INTERES'
			, KAI.ID_AREA_INTERES AS '@ID_AREA_INTERES'
			, CAI.NB_AREA_INTERES AS '@NB_AREA_INTERES'
		FROM ADM.K_AREA_INTERES KAI
			LEFT JOIN ADM.C_AREA_INTERES CAI
				ON KAI.ID_AREA_INTERES = CAI.ID_AREA_INTERES
		WHERE KAI.ID_CANDIDATO = @ID_CANDIDATO
		FOR XML PATH('ITEM'), ROOT('DATOS')
	)

	SET @XML_ID_COMPETENCIAS = (
		SELECT CC.ID_COMPETENCIA AS '@NB_VALOR'
			, CC.NB_COMPETENCIA AS '@NB_TEXTO'
		FROM ADM.C_COMPETENCIA CC
		FOR XML PATH('ITEM'), ROOT('ITEMS')
	)

	SET @XML_COMPETENCIAS = (
		SELECT NEWID() AS '@ID_ITEM'
			, KCC.ID_CANDIDATO_COMPETENCIA AS '@ID_CANDIDATO_COMPETENCIA'
			, KCC.ID_COMPETENCIA AS '@ID_COMPETENCIA'
			, CC.NB_COMPETENCIA AS '@NB_COMPETENCIA'
		FROM IDP.K_CANDIDATO_COMPETENCIA KCC
			LEFT JOIN ADM.C_COMPETENCIA CC
				ON KCC.ID_COMPETENCIA = CC.ID_COMPETENCIA
		WHERE KCC.ID_CANDIDATO = @ID_CANDIDATO
		FOR XML PATH('ITEM'), ROOT('DATOS')
	)

	SET @XML_CL_RED_SOCIAL = (
		SELECT VRS.CL_RED_SOCIAL AS '@NB_VALOR'
			, VRS.NB_RED_SOCIAL AS '@NB_TEXTO'
		FROM ADM.VW_RED_SOCIAL VRS
		FOR XML PATH('ITEM'), ROOT('ITEMS')
	)

	SET @XML_PERFILES_REDES_SOCIALES = (
		SELECT ROW_NUMBER() OVER(ORDER BY T.value('@CL_RED_SOCIAL', 'NVARCHAR(20)')) AS '@ID_ITEM'
			, T.value('@NB_PERFIL', 'NVARCHAR(200)') AS '@NB_PERFIL'
			, T.value('@CL_RED_SOCIAL', 'NVARCHAR(20)') AS '@CL_RED_SOCIAL'
			, ISNULL(VRS.NB_RED_SOCIAL, T.value('@CL_RED_SOCIAL', 'NVARCHAR(20)')) AS '@NB_RED_SOCIAL'
		FROM @XML_RED_SOCIAL.nodes('/REDES_SOCIALES/RED_SOCIAL') X(T)
			LEFT JOIN ADM.VW_RED_SOCIAL AS VRS
				ON T.value('@CL_RED_SOCIAL', 'NVARCHAR(20)') = VRS.CL_RED_SOCIAL
		FOR XML PATH('ITEM'), ROOT('DATOS')
	)

	DECLARE @XML_VALORES XML 


	SET @XML_VALORES = (
		SELECT @NB_CANDIDATO AS NB_CANDIDATO
			, @NB_APELLIDO_PATERNO AS NB_APELLIDO_PATERNO
			, @NB_APELLIDO_MATERNO AS NB_APELLIDO_MATERNO
			, @CL_RFC AS CL_RFC
			, @CL_CURP AS CL_CURP
			, @CL_NSS AS CL_NSS
			, @DS_LUGAR_NACIMIENTO AS DS_LUGAR_NACIMIENTO
			, @NB_NACIONALIDAD AS NB_NACIONALIDAD
			, CONVERT(NVARCHAR(10), @FE_NACIMIENTO, 103) AS FE_NACIMIENTO
			, @XML_CL_GENERO AS CL_GENERO
			, @XML_CL_ESTADO_CIVIL AS CL_ESTADO_CIVIL
			, @NB_CONYUGUE AS NB_CONYUGUE
			, @CL_CODIGO_POSTAL AS CL_CODIGO_POSTAL
			, @XML_NB_ESTADO
			, @XML_NB_MUNICIPIO
			, @XML_NB_COLONIA
			, @NB_CALLE AS NB_CALLE
			, @NO_EXTERIOR AS NO_EXTERIOR
			, @NO_INTERIOR AS NO_INTERIOR
			, @XML_NO_TELEFONOS AS LS_TELEFONOS
			, @XML_NO_TELEFONO_TIPOS AS LS_TELEFONOS_TIPO
			, @CL_CORREO_ELECTRONICO AS CL_CORREO_ELECTRONICO
			, @NB_LICENCIA AS NB_LICENCIA
			, @DS_VEHICULO AS DS_VEHICULO
			, @CL_CARTILLA_MILITAR AS CL_CARTILLA_MILITAR
			, @CL_CEDULA_PROFESIONAL AS CL_CEDULA_PROFESIONAL
			, @XML_CL_PARENTESCO AS LS_PARIENTES_CL_PARENTESCO
			, @XML_CL_OCUPACION AS LS_PARIENTES_CL_OCUPACION
			, @XML_DATOS_FAMILIARES AS LS_PARIENTES
			, @XML_CL_NIVEL_ESCOLARIDAD AS LS_FORMACION_ACADEMICA_CL_NIVEL_ESCOLARIDAD
			, @XML_ID_ESCOLARIDAD AS LS_FORMACION_ACADEMICA_CL_ESCOLARIDAD
			, @XML_CL_ESTADO_ESCOLARIDAD AS LS_FORMACION_ACADEMICA_CL_ESTADO_ESCOLARIDAD
			, @XML_FORMACION_ACADEMICA AS LS_FORMACION_ACADEMICA
			, @XML_ID_IDIOMA AS LS_IDIOMAS_ID_IDIOMA
			, @XML_PR_DOMINIO_IDIOMA AS LS_IDIOMAS_PR_CONVERSACIONAL
			, @XML_PR_DOMINIO_IDIOMA AS LS_IDIOMAS_PR_TRADUCCION
			, @XML_CL_CERTIFICADO_IDIOMA AS LS_IDIOMAS_CL_INSTITUCION
			, @XML_IDIOMAS AS LS_IDIOMAS
			, @XML_EXPERIENCIA_PROFESIONAL AS LS_EXPERIENCIA_LABORAL
			, @XML_ID_AREA_INTERES AS LS_AREAS_INTERES_ID_AREA_INTERES
			, @XML_AREA_INTERES AS LS_AREAS_INTERES
			, @XML_ID_COMPETENCIAS AS LS_AREAS_INTERES_ID_COMPETENCIAS
			, @XML_COMPETENCIAS AS LS_COMPETENCIAS
			, @DS_COMPETENCIAS_ADICIONALES AS DS_COMPETENCIAS_ADICIONALES
			, @DS_DISPONIBILIDAD AS DS_DISPONIBILIDAD
			, @CL_DISPONIBILIDAD_VIAJE AS CL_DISPONIBILIDAD_VIAJE
			, @XML_CL_RED_SOCIAL AS LS_PERFILES_CL_RED_SOCIAL
			, @XML_PERFILES_REDES_SOCIALES AS LS_PERFILES
			, @DS_COMENTARIO AS DS_COMENTARIO

		FOR XML PATH('VALORES')
	)

	SELECT 'F-0001' AS CL_SOLICITUD
		, GETDATE() AS FE_SOLICITUD
		, @XML_SOLICITUD_PLANTILLA AS XML_SOLICITUD_PLANTILLA
		, @XML_VALORES AS XML_VALORES


	
END