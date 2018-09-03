-- =============================================
-- Proyecto: Sigein 5.0
-- Copyright (c) - Acrux - 2016
-- Author: GABRIEL VAZQUEZ
-- CREATE date: 21/01/2016
-- Description: Obtiene los datos de las competencias del perfil de ingreso del puesto
-- =============================================
CREATE PROCEDURE IDP.SPE_REP_RESULT_PUESTO_EXPERIENCIA
	@PIN_ID_PUESTO AS INT
AS
BEGIN

	SELECT
		pe.ID_PUESTO_EXPERIENCIA,
		pe.ID_AREA_INTERES,
		pe.NO_TIEMPO,
		pe.CL_NIVEL_REQUERIDO,
		ai.NB_AREA_INTERES
	FROM ADM.C_PUESTO_EXPERIENCIA pe
		INNER JOIN ADM.C_AREA_INTERES ai ON pe.ID_AREA_INTERES = ai.ID_AREA_INTERES
	WHERE ID_PUESTO = @PIN_ID_PUESTO

END