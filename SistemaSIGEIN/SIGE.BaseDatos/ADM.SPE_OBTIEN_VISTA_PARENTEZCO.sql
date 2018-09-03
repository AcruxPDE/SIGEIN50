-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 02/11/2015
-- Description: Procedimiento que regresa vista de parentezco 
-- =============================================

ALTER PROCEDURE ADM.SPE_OBTIEN_VISTA_PARENTEZCO
AS
SELECT NB_PARENTEZCO 
FROM ADM.VW_PARENTEZCO