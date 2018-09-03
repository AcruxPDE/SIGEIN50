-- =============================================
-- Author:		Julio Díaz
-- Create date: 12/02/2016
-- Description:	Función sacar la fecha según la configuración del horario
-- =============================================
CREATE FUNCTION [dbo].[F_GETDATE] ()
RETURNS DATETIME
AS
BEGIN
	DECLARE @FE_SISTEMA AS DATETIME = GETDATE()
	RETURN @FE_SISTEMA
END
