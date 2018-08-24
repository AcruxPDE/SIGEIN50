-- =============================================
-- Author:		Juan Pérez
-- Create date: 07/12/2015
-- Description:	Función sacar la edad de un empleado,candidato etc.
-- =============================================
CREATE FUNCTION [dbo].[F_OBTENER_EDAD]
(
	-- Add the parameters for the function here
	 @PIN_FE_NACIMIENTO DATETIME

)
RETURNS INT
AS
BEGIN
	DECLARE @EDAD AS INT
	SET @EDAD =(
	SELECT
	-- DATEDIFF(hour,@PIN_FE_NACIMIENTO,GETDATE())/8766.0 AS EDAD_DECIMAL
    --,CONVERT(int,ROUND(DATEDIFF(hour,@PIN_FE_NACIMIENTO,GETDATE())/8766.0,0)) AS EDAD_REDONDEADA
    DATEDIFF(hour,@PIN_FE_NACIMIENTO,GETDATE())/8766 AS EDAD
	)
	RETURN @EDAD

END
