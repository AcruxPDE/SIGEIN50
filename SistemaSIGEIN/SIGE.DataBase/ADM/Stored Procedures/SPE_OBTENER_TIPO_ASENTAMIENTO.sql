-- =============================================
-- Proyecto: Sistema Nomina
-- Copyright (c) - Acrux - 2014
-- Author: Jesús Vázquez Pereyra
-- CREATE date: 27/10/2015
-- Description: se obtiene el tipo de asentamiento que se tiene en la tabla de colonias
-- =============================================

CREATE PROCEDURE [ADM].[SPE_OBTENER_TIPO_ASENTAMIENTO]
AS BEGIN
	SELECT CL_TIPO_ASENTAMIENTO, NB_TIPO_ASENTAMIENTO FROM VW_TIPO_ASENTAMIENTO
END
