-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Julio Díaz
-- CREATE date: 19/01/2016
-- Description: Vista del porcentaje del nivel de dominio del idioma
-- =============================================

CREATE VIEW [ADM].[VW_RED_SOCIAL]
AS
SELECT 'LINKEDIN' AS CL_RED_SOCIAL, 'LinkedIn' AS NB_RED_SOCIAL UNION 
SELECT 'FACEBOOK' AS CL_RED_SOCIAL, 'Facebook' AS NB_RED_SOCIAL UNION 
SELECT 'TWITTER' AS CL_RED_SOCIAL, 'Twitter' AS NB_RED_SOCIAL UNION 
SELECT 'INSTAGRAM' AS CL_RED_SOCIAL, 'Instagram' AS NB_RED_SOCIAL UNION 
SELECT 'GOOGLE_PLUS' AS CL_RED_SOCIAL, 'Google+' AS NB_RED_SOCIAL 

