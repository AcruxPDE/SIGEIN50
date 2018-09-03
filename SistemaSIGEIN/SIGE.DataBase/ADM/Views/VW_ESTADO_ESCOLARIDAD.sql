-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Julio Díaz
-- CREATE date: 19/01/2016
-- Description: Vista del porcentaje del nivel de dominio del idioma
-- =============================================

CREATE VIEW [ADM].[VW_ESTADO_ESCOLARIDAD]
AS
SELECT 'SI' AS CL_ESTADO_ESCOLARIDAD, 'Sí' AS NB_ESTADO_ESCOLARIDAD UNION 
SELECT 'NO' AS CL_ESTADO_ESCOLARIDAD, 'No' AS NB_ESTADO_ESCOLARIDAD UNION 
SELECT 'ENCURSO' AS CL_ESTADO_ESCOLARIDAD, 'En curso' AS NB_ESTADO_ESCOLARIDAD 


