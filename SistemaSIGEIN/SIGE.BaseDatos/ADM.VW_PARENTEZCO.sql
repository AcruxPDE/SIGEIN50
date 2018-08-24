-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 02/11/2015
-- Description: Vista que muestra los parentezcos que se tiene con familiares directos
-- =============================================

ALTER VIEW ADM.VW_PARENTEZCO
AS

	SELECT 'Cónyugue' AS NB_PARENTEZCO
	UNION
	SELECT 'Hijo' AS NB_PARENTEZCO
	UNION
	SELECT 'Hija' AS NB_PARENTEZCO
	UNION 
	SELECT 'Padre' as NB_PARENTEZCO
	UNION
	SELECT 'Madre' as NB_PARENTEZCO
	UNION
	SELECT 'Hermano' as NB_PARENTEZCO
	UNION 
	SELECT 'Hermana' as NB_PARENTEZCO