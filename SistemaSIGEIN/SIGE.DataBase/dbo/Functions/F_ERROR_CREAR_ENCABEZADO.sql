-- =============================================
-- Author:		Julio Díaz
-- Create date: 20/11/2015
-- Description:	Función para crear el encabezado del mensaje de error
-- =============================================
CREATE FUNCTION F_ERROR_CREAR_ENCABEZADO
(
	-- Add the parameters for the function here
	@PIN_NO_REGISTROS_AFECTADOS INT
	,@PIN_NO_ERROR INT
	,@PIN_CL_TIPO_ERROR NVARCHAR(10)
)
RETURNS XML
AS
BEGIN

	DECLARE @XML_RESULTADO XML
	SET @XML_RESULTADO = (
		SELECT @PIN_NO_REGISTROS_AFECTADOS AS "@NO_AFECTADOS" 
			, @PIN_NO_ERROR AS "@NO_ERROR"
			, @PIN_CL_TIPO_ERROR AS "@CL_TIPO_ERROR"
		FOR XML PATH ('RESULT')
	)

	RETURN @XML_RESULTADO

END
