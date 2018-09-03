-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Jesús Vázquez Pereyra
-- CREATE date: 28/10/2015
-- Description: SE CREA LA VISTA DE LAS CLAVES DE ESTADO DE REQUICISION
-- =============================================
create view ADM.VW_ESTADO_REQUISICION
AS
	SELECT   'VIGENTE' AS CL_ESTADO
	UNION
	SELECT   'CANCELADA' AS CL_ESTADO
	UNION
	SELECT   'CUBIERTA' AS CL_ESTADO
	