SET ANSI_NULLS ON 
GO
SET QUOTED_IDENTIFIER ON 
GO
-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Jesús Vázquez Pereyra
-- CREATE date: 28/10/2015
-- Description: Obtiene los datos de la VISTA VW_CAUSAS_REQUISICION 
-- =============================================
ALTER PROCEDURE ADM.SPE_OBTIENE_VW_CAUSAS_REQUISICION
	-- @PIN_CL_ESTADO AS nvarchar(20) = NULL
	 @PIN_CL_CAUSA AS nvarchar(20) = NULL
AS
	SELECT  CL_CAUSA ,'' as DS_FILTRO
	FROM ADM.VW_CAUSAS_REQUISICION
	WHERE (@PIN_CL_CAUSA = @PIN_CL_CAUSA AND @PIN_CL_CAUSA IS NOT NULL) OR ( @PIN_CL_CAUSA IS NULL)
	ORDER BY CL_CAUSA
