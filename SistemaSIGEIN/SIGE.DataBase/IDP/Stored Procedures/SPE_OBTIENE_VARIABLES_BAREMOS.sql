
-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Gabriel Vázquez Torres
-- CREATE date: 08/02/2016
-- Description: Obtiene las variables de baremos con respecto a la bateria
-- =============================================
CREATE PROCEDURE [IDP].[SPE_OBTIENE_VARIABLES_BAREMOS]
	@PIN_ID_BATERIA AS INT
AS

	SELECT
			cv.ID_VARIABLE,
			cv.CL_VARIABLE,
			ISNULL(RES.NO_VALOR, 0) AS NO_VALOR
			--KBP.ID_BATERIA
	FROM IDP.C_VARIABLE CV
		LEFT JOIN (
					SELECT KR.ID_VARIABLE, KR.NO_VALOR
					FROM IDP.K_RESULTADO KR
						INNER JOIN IDP.K_BATERIA_PRUEBA KBP ON KR.ID_CUESTIONARIO = KBP.ID_CUESTIONARIO_BAREMOS
					WHERE KBP.ID_BATERIA = @PIN_ID_BATERIA
				) AS RES ON CV.ID_VARIABLE = RES.ID_VARIABLE
	WHERE CV.CL_TIPO_VARIABLE = 4
	
