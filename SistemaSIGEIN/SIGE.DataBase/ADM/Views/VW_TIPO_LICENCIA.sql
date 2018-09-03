-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 08/11/2015
-- Description: Vista de Tipos de Licencia
-- =============================================

CREATE VIEW ADM.VW_TIPO_LICENCIA
AS

SELECT 'MENORES' AS CL_LICENCIA, 'Menores de 18 años' AS NB_LICENCIA
UNION 
SELECT 'TIPO A' AS CL_LICENCIA, 'TIPO A' AS NB_LICENCIA
UNION 
SELECT 'TIPO B' AS CL_LICENCIA, 'TIPO B' AS NB_LICENCIA
UNION
SELECT 'TIPO C' AS CL_LICENCIA, 'TIPO C' AS NB_LICENCIA
UNION
SELECT 'TIPO D' AS CL_LICENCIA, 'TIPO D' AS NB_LICENCIA