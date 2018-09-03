-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 06/11/2015
-- Description: Vista que muestra los diferentes tipos de licencia
-- =============================================

ALTER VIEW VW_TIPOS_LICENCIA
AS
	SELECT 1 AS CL_LICENCIA, 'MENORES DE EDAD' AS NB_LICENCIA
	UNION
	SELECT 2 AS CL_LICENCIA, 'TIPO A' AS NB_LICENCIA
	UNION 
	SELECT 3 AS CL_LICENCIA, 'TIPO B' AS NB_LICENCIA
	UNION 
	SELECT 4 AS CL_LICENCIA, 'TIPO C' AS NB_LICENCIA
	UNION
	SELECT 5 AS CL_LICENCIA, 'TIPO D' AS NB_LICENCIA