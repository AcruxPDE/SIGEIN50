-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Jesús Vázquez Pereyra
-- CREATE date: 28/10/2015
-- Description: SE CREA LA VISTA DE LAS CLAVES DE CAUSA DE REQUICISION
-- =============================================
create view ADM.VW_CAUSAS_REQUISICION
AS
	SELECT   'RENUNCIA' AS CL_CAUSA
	UNION
	SELECT   'ABANDONO' AS CL_CAUSA
	UNION
	SELECT   'DESPIDO' AS CL_CAUSA
	UNION
	SELECT   'INCAPACIDAD' AS CL_CAUSA
	UNION
	SELECT   'JUBILACION' AS CL_CAUSA
	UNION
	SELECT   'TRASLADO' AS CL_CAUSA
	UNION
	SELECT   'ASCENSO' AS CL_CAUSA
	UNION
	SELECT   'OTRA' AS CL_CAUSA
	