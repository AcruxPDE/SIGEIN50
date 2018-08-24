-- =============================================
-- Proyecto: Sigein 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 12/11/2015
-- Description: Vista que obtiene porcentajes a calificar
-- =============================================


CREATE VIEW IDP.VW_OBTIENE_PORCENTAJES_IDIOMAS
AS


SELECT 'Mal 0% - 25%' AS NB_PORCENTAJE, 0.00 AS CL_PORCENTAJE
UNION
SELECT 'Mal 26% - 60%' AS NB_PORCENTAJE, 26.00 AS CL_PORCENTAJE
UNION
SELECT 'Mal 61% - 90%' AS NB_PORCENTAJE, 61.00 AS CL_PORCENTAJE
UNION
SELECT 'Mal 91% - 100%' AS NB_PORCENTAJE, 91.00 AS CL_PORCENTAJE