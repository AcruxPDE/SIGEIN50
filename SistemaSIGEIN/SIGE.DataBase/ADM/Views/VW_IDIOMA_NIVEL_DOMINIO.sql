

-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Julio Díaz
-- CREATE date: 19/01/2016
-- Description: Vista del porcentaje del nivel de dominio del idioma
-- =============================================

CREATE VIEW [ADM].[VW_IDIOMA_NIVEL_DOMINIO]
AS
SELECT CAST(25 AS DECIMAL(5,2)) AS PR_DOMINIO, 'Mal 0% - 25%' AS NB_NIVEL UNION 
SELECT CAST(60 AS DECIMAL(5,2)) AS PR_DOMINIO, 'Regular 25% - 60%' AS NB_NIVEL UNION 
SELECT CAST(90 AS DECIMAL(5,2)) AS PR_DOMINIO, 'Bien 60% - 90%' AS NB_NIVEL UNION 
SELECT CAST(100 AS DECIMAL(5,2)) AS PR_DOMINIO, 'Excelente 91% - 100%' AS NB_NIVEL 



