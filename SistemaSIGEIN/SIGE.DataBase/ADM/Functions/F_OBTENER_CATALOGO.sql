-- =============================================
-- Proyecto: Sigein 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Felipe Hernández Palafox
-- CREATE date: 07/12/2015
-- Description: obtiene catalogo de catalogos
-- =============================================
CREATE FUNCTION [ADM].[F_OBTENER_CATALOGO](@CL_CATALOGO NVARCHAR(30)) 
RETURNS TABLE 
AS
	RETURN 
	(
		SELECT *
		FROM ADM.C_CATALOGO_VALOR
		WHERE ID_CATALOGO_LISTA = (SELECT TOP 1 XML_CONFIGURACION.value('(/CONFIGURACIONES/CATALOGOS/*[local-name()=sql:variable("@CL_CATALOGO")]/@ID_CATALOGO)[1]', 'INT') FROM ADM.S_CONFIGURACION)
	)
