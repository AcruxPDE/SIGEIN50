-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2014
-- Author: Julio Díaz
-- CREATE date: 18/11/2015
-- Description: Obtiene la estructura de configuración de un parámetro específico
-- =============================================

CREATE PROCEDURE [ADM].[SPE_OBTIENE_CONFIGURACION]
	@CL_CONFIGURACION NVARCHAR(255)
   ,@XML_CONFIGURACION XML OUT

AS
BEGIN

	SET @XML_CONFIGURACION = (SELECT TOP 1 XML_CONFIGURACION FROM ADM.S_CONFIGURACION)

	SET @XML_CONFIGURACION = (
		SELECT TOP 1 C.query('.') 
		FROM @XML_CONFIGURACION.nodes('/CONFIGURACIONES/CONFIGURACION') AS T(C) 
		WHERE C.query('.').value('/CONFIGURACION[1]/@PARAMETRO', 'NVARCHAR(255)') = @CL_CONFIGURACION
	)

END