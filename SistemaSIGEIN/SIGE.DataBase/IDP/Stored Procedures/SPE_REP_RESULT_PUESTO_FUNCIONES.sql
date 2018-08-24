﻿-- =============================================
-- Proyecto: Sigein 5.0
-- Copyright (c) - Acrux - 2016
-- Author: GABRIEL VAZQUEZ
-- CREATE date: 21/01/2016
-- Description: Obtiene los datos de las funciones genericas del puesto
-- =============================================
CREATE PROCEDURE IDP.SPE_REP_RESULT_PUESTO_FUNCIONES
	@PIN_ID_PUESTO AS INT
AS
BEGIN

	SELECT
		pf.ID_PUESTO_FUNCION,
		pf.NB_PUESTO_FUNCION,
		ISNULL(CAST(pf.XML_DETALLE AS NVARCHAR(max)),'') AS XML_DETALLE,
		ISNULL(CAST(pf.XML_NOTAS AS NVARCHAR(max)),'') AS XML_NOTAS,
		c.NB_COMPETENCIA,
		cn.NO_VALOR_NIVEL,
		id.NB_INDICADOR
	FROM ADM.C_PUESTO_FUNCION pf
		INNER JOIN ADM.C_PUESTO_COMPETENCIA pc ON pf.ID_PUESTO_FUNCION = pc.ID_PUESTO_FUNCION
		INNER JOIN ADM.C_COMPETENCIA_NIVEL cn ON PC.ID_NIVEL_DESEADO = cn.ID_NIVEL_COMPETENCIA
		INNER JOIN ADM.C_COMPETENCIA c ON pc.ID_COMPETENCIA = c.ID_COMPETENCIA
		INNER JOIN ADM.C_FUNCION_INDICADOR fi ON pc.ID_PUESTO_COMPETENCIA = fi.ID_PUESTO_COMPETENCIA
		INNER JOIN ADM.C_INDICADOR_DESEMPENO id ON fi.ID_INDICADOR = id.ID_INDICADOR
	WHERE pf.ID_PUESTO = @PIN_ID_PUESTO

END