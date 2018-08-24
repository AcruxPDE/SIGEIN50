-- =============================================
-- Author:		Julio Díaz
-- Create date: 9/12/2015
-- Description:	Función para insertar datos de regreso de la base de datos
-- =============================================
CREATE FUNCTION [dbo].[F_ERROR_INSERTAR_DATOS]
(
	-- Add the parameters for the function here
	@PIN_XML_ENCABEZADO XML
	,@PIN_XML_DATOS XML
)
RETURNS XML
AS
BEGIN

	DECLARE @XML_RESULTADO XML = @PIN_XML_DATOS

	IF @PIN_XML_ENCABEZADO.exist('/RESULT/DATOS') <> 1
		SET @PIN_XML_ENCABEZADO.modify('insert <DATOS /> into (/RESULT)[1]');

	SET @PIN_XML_ENCABEZADO.modify('insert sql:variable("@XML_RESULTADO") into (/RESULT/DATOS)[1]') ;

	SET @XML_RESULTADO = @PIN_XML_ENCABEZADO

	RETURN @XML_RESULTADO

END