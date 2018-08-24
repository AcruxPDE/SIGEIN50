-- =============================================
-- Proyecto: Sigein 5.0
-- Copyright (c) - Acrux - 2016
-- Author: GABRIEL VAZQUEZ
-- CREATE date: 21/01/2016
-- Description: Obtiene los datos de las competencias del perfil de ingreso del puesto
-- =============================================
CREATE PROCEDURE IDP.SPE_REP_RESULT_PUESTO_COMPETENCIAS_PERFIL
	@PIN_ID_PUESTO AS INT

AS
BEGIN

	SELECT
		cc.ID_COMPETENCIA,
		cc.CL_COMPETENCIA AS Clave,
		cc.NB_COMPETENCIA AS Competencia,
		cc.DS_COMPETENCIA
	FROM ADM.C_COMPETENCIA cc
		INNER JOIN ADM.C_PUESTO_COMPETENCIA pc ON cc.ID_COMPETENCIA = pc.ID_COMPETENCIA
	WHERE cc.FG_ACTIVO = 1 AND pc.CL_TIPO_COMPETENCIA = 'PERFIL' AND pc.ID_PUESTO = @PIN_ID_PUESTO

END