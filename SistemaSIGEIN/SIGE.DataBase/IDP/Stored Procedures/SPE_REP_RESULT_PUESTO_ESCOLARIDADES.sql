﻿-- =============================================
-- Proyecto: Sigein 5.0
-- Copyright (c) - Acrux - 2016
-- Author: GABRIEL VAZQUEZ
-- CREATE date: 21/01/2016
-- Description: Obtiene los datos para el valor del
-- =============================================
CREATE PROCEDURE [IDP].[SPE_REP_RESULT_PUESTO_ESCOLARIDADES]
	 @PIN_ID_PUESTO AS INT

AS
BEGIN

	SELECT
		pe.ID_PUESTO_ESCOLARIDAD,
		pe.ID_PUESTO,
		pe.ID_ESCOLARIDAD,
		e.NB_ESCOLARIDAD,
		e.CL_NIVEL_ESCOLARIDAD,
		ne.CL_TIPO_ESCOLARIDAD
	FROM ADM.C_PUESTO_ESCOLARIDAD pe
		INNER JOIN ADM.C_ESCOLARIDAD e ON pe.ID_ESCOLARIDAD = e.ID_ESCOLARIDAD
		INNER JOIN ADM.C_NIVEL_ESCOLARIDAD ne ON e.CL_NIVEL_ESCOLARIDAD  = ne.CL_NIVEL_ESCOLARIDAD
	WHERE pe.ID_PUESTO = @PIN_ID_PUESTO

END