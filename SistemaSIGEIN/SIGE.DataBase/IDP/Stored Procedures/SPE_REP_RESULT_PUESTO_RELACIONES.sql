-- =============================================
-- Proyecto: Sigein 5.0
-- Copyright (c) - Acrux - 2016
-- Author: GABRIEL VAZQUEZ
-- CREATE date: 21/01/2016
-- Description: Obtiene los datos de los puestos relacionados con los mismos puestos
-- =============================================
CREATE PROCEDURE IDP.SPE_REP_RESULT_PUESTO_RELACIONES
	@PIN_ID_PUESTO AS INT
AS
BEGIN

	SELECT
		pr.ID_PUESTO_RELACIONADO,
		pr.CL_TIPO_RELACION,
		p.NB_PUESTO
	FROM ADM.C_PUESTO_RELACIONADO pr
		INNER JOIN ADM.M_PUESTO p on pr.ID_PUESTO_RELACIONADO = p.ID_PUESTO
	WHERE pr.ID_PUESTO = @PIN_ID_PUESTO

END