-- =============================================
-- Author:		Julio Díaz
-- Create date: 20/11/2015
-- Description:	Función para crear el encabezado del mensaje de error
-- =============================================
ALTER FUNCTION F_ERROR_INSERTAR_MENSAJES
(
	-- Add the parameters for the function here
	@PIN_XML_ENCABEZADO XML
	,@PIN_DS_MENSAJE NVARCHAR(500)
	,@PIN_CL_IDIOMA NVARCHAR(20)
)
RETURNS XML
AS
BEGIN

	DECLARE @XML_RESULTADO XML
	SET @XML_RESULTADO = (
		SELECT @PIN_CL_IDIOMA AS "@CL_IDIOMA" 
			, @PIN_DS_MENSAJE AS "@DS_MENSAJE"
		FOR XML PATH ('MENSAJE')
	)

	IF @PIN_XML_ENCABEZADO.exist('/RESULT/MENSAJES') <> 1
		SET @PIN_XML_ENCABEZADO.modify('insert <MENSAJES /> into (/RESULT)[1]');

	SET @PIN_XML_ENCABEZADO.modify('insert sql:variable("@XML_RESULTADO") into (/RESULT/MENSAJES)[1]') ;

	SET @XML_RESULTADO = @PIN_XML_ENCABEZADO

	RETURN @XML_RESULTADO

END
GO

