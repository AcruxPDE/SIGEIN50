﻿-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [IDP].[F_OBTIENE_VALORES_ESTILO_PENSAMIENTO]
(
	-- Add the parameters for the function here
	@PIN_ID_CUESTIONARIO AS INT,
	@PIN_CL_GRUPO AS NVARCHAR(5)
)
RETURNS DECIMAL(13,3)
AS
BEGIN
	
	-- VARIABLES PARA CALCULAR EL COEFICIENTE INTELECTUAL
	DECLARE @V_NO_CI AS INT
	DECLARE @V_NO_TOTAL AS decimal(13,3)
	
	/*SELECT @V_NO_TOTAL = CAST((SUM(NO_VALOR)) AS DECIMAL(13,3)) * CAST(@PIN_NO_FACTOR AS DECIMAL(13,3))
	FROM IDP.K_RESULTADO r
	WHERE ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO AND ID_VARIABLE IN (@PIN_NO_V1, @PIN_NO_V2, @PIN_NO_V3, @PIN_NO_V4, @PIN_NO_V5)
	GROUP BY ID_CUESTIONARIO*/

	;WITH T_GRUPOS AS (
		SELECT 'PENSAMIENTO_RES_A1' AS CL_VARIABLE,'V2' AS CL_GRUPO, 2 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_A2' AS CL_VARIABLE,'I2' AS CL_GRUPO, 2 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_A3' AS CL_VARIABLE,'A2' AS CL_GRUPO, 2 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_A4' AS CL_VARIABLE,'L2' AS CL_GRUPO, 2 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_A5' AS CL_VARIABLE,'A2' AS CL_GRUPO, 2 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_A6' AS CL_VARIABLE,'I2' AS CL_GRUPO, 2 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_A7' AS CL_VARIABLE,'V2' AS CL_GRUPO, 2 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_A8' AS CL_VARIABLE,'L2' AS CL_GRUPO, 2 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_A9' AS CL_VARIABLE,'I2' AS CL_GRUPO, 2 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_A10' AS CL_VARIABLE,'L2' AS CL_GRUPO, 2 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_A11' AS CL_VARIABLE,'V2' AS CL_GRUPO, 2 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_A12' AS CL_VARIABLE,'A2' AS CL_GRUPO, 2 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_A13' AS CL_VARIABLE,'A2' AS CL_GRUPO, 2 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_A14' AS CL_VARIABLE,'L2' AS CL_GRUPO, 2 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_A15' AS CL_VARIABLE,'V2' AS CL_GRUPO, 2 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_A16' AS CL_VARIABLE,'I2' AS CL_GRUPO, 2 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_A17' AS CL_VARIABLE,'V2' AS CL_GRUPO, 2 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_A18' AS CL_VARIABLE,'I2' AS CL_GRUPO, 2 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_A19' AS CL_VARIABLE,'A2' AS CL_GRUPO, 2 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_A20' AS CL_VARIABLE,'L2' AS CL_GRUPO, 2 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_B1' AS CL_VARIABLE,'V3' AS CL_GRUPO, 3 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_B2' AS CL_VARIABLE,'I3' AS CL_GRUPO, 3 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_B3' AS CL_VARIABLE,'L3' AS CL_GRUPO, 3 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_B4' AS CL_VARIABLE,'I3' AS CL_GRUPO, 3 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_B5' AS CL_VARIABLE,'L3' AS CL_GRUPO, 3 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_B6' AS CL_VARIABLE,'L3' AS CL_GRUPO, 3 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_B7' AS CL_VARIABLE,'L3' AS CL_GRUPO, 3 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_B8' AS CL_VARIABLE,'A3' AS CL_GRUPO, 3 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_B9' AS CL_VARIABLE,'A3' AS CL_GRUPO, 3 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_B10' AS CL_VARIABLE,'V3' AS CL_GRUPO, 3 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_B11' AS CL_VARIABLE,'A3' AS CL_GRUPO, 3 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_B12' AS CL_VARIABLE,'V3' AS CL_GRUPO, 3 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_B13' AS CL_VARIABLE,'I3' AS CL_GRUPO, 3 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_B14' AS CL_VARIABLE,'L3' AS CL_GRUPO, 3 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_B15' AS CL_VARIABLE,'A3' AS CL_GRUPO, 3 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_B16' AS CL_VARIABLE,'I3' AS CL_GRUPO, 3 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_B17' AS CL_VARIABLE,'A3' AS CL_GRUPO, 3 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_B18' AS CL_VARIABLE,'V3' AS CL_GRUPO, 3 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_B19' AS CL_VARIABLE,'V3' AS CL_GRUPO, 3 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_B20' AS CL_VARIABLE,'I3' AS CL_GRUPO, 3 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_C1' AS CL_VARIABLE,'A5' AS CL_GRUPO, 5 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_C2' AS CL_VARIABLE,'V5' AS CL_GRUPO, 5 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_C3' AS CL_VARIABLE,'L5' AS CL_GRUPO, 5 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_C4' AS CL_VARIABLE,'L5' AS CL_GRUPO, 5 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_C5' AS CL_VARIABLE,'V5' AS CL_GRUPO, 5 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_C6' AS CL_VARIABLE,'I5' AS CL_GRUPO, 5 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_C7' AS CL_VARIABLE,'A5' AS CL_GRUPO, 5 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_C8' AS CL_VARIABLE,'L5' AS CL_GRUPO, 5 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_C9' AS CL_VARIABLE,'I5' AS CL_GRUPO, 5 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_C10' AS CL_VARIABLE,'A5' AS CL_GRUPO, 5 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_C11' AS CL_VARIABLE,'I5' AS CL_GRUPO, 5 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_C12' AS CL_VARIABLE,'V5' AS CL_GRUPO, 5 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_C13' AS CL_VARIABLE,'L5' AS CL_GRUPO, 5 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_C14' AS CL_VARIABLE,'I5' AS CL_GRUPO, 5 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_C15' AS CL_VARIABLE,'V5' AS CL_GRUPO, 5 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_C16' AS CL_VARIABLE,'A5' AS CL_GRUPO, 5 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_C17' AS CL_VARIABLE,'A5' AS CL_GRUPO, 5 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_C18' AS CL_VARIABLE,'I5' AS CL_GRUPO, 5 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_C19' AS CL_VARIABLE,'V5' AS CL_GRUPO, 5 AS NO_FACTOR UNION ALL
		SELECT 'PENSAMIENTO_RES_C20' AS CL_VARIABLE,'L5' AS CL_GRUPO, 5 AS NO_FACTOR)
	
	
		SELECT @V_NO_TOTAL = CAST((SUM(NO_VALOR)) AS DECIMAL(13,3)) * CAST(g.NO_FACTOR AS DECIMAL(13,3))
		FROM IDP.K_RESULTADO r
			INNER JOIN IDP.C_VARIABLE v ON r.ID_VARIABLE = v.ID_VARIABLE
			INNER JOIN T_GRUPOS g ON v.CL_VARIABLE = g.CL_VARIABLE
		WHERE r.ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO AND CL_GRUPO = @PIN_CL_GRUPO
		GROUP BY r.ID_CUESTIONARIO, g.NO_FACTOR


		-- Return the result of the function
		RETURN @V_NO_TOTAL

END