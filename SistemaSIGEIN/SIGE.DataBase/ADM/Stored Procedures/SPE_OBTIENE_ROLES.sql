-- =============================================
-- Proyecto: Sigein 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Julio Díaz
-- CREATE date: 20/11/2015
-- Description: Obtiene los roles del sistema
-- =============================================

CREATE PROCEDURE [ADM].[SPE_OBTIENE_ROLES]
	@PIN_ID_ROL AS INT = NULL
AS   
	SELECT CR.ID_ROL
		, CR.CL_ROL
		, CR.NB_ROL
		, CASE WHEN CR.FG_ACTIVO = 1 THEN 'Sí' ELSE 'No' END AS FG_ACTIVO
	FROM ADM.C_ROL CR
	WHERE (@PIN_ID_ROL IS NULL OR (@PIN_ID_ROL IS NOT NULL AND CR.ID_ROL = @PIN_ID_ROL))
