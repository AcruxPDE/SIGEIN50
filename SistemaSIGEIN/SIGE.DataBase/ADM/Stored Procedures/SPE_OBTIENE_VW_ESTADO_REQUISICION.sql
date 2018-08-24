-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Jesús Vázquez Pereyra
-- CREATE date: 28/10/2015
-- Description: Obtiene los datos de la VISTA VW_ESTADO_REQUISICION 
-- =============================================
CREATE PROCEDURE ADM.SPE_OBTIENE_VW_ESTADO_REQUISICION
	 @PIN_CL_ESTADO AS nvarchar(20) = NULL
	
AS
	SELECT  CL_ESTADO , '' as DS_FILTRO
	FROM ADM.VW_ESTADO_REQUISICION
	WHERE (CL_ESTADO = @PIN_CL_ESTADO AND @PIN_CL_ESTADO IS NOT NULL) OR ( @PIN_CL_ESTADO IS NULL)
	ORDER BY CL_ESTADO
