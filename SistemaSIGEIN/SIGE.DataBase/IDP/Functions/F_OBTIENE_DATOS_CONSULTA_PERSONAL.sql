﻿-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [IDP].[F_OBTIENE_DATOS_CONSULTA_PERSONAL]
(
	@PIN_ID_CUESTIONARIO_BAREMOS AS INT
)
RETURNS TABLE 
AS
RETURN 
(

	SELECT 
		KR.NO_VALOR,
		CV.ID_VARIABLE,
		CV.CL_VARIABLE,
		CF.CL_FACTOR,
		CF.NB_FACTOR,
		CF.DS_FACTOR,
		CF.ID_FACTOR,
		CC.ID_COMPETENCIA,
		CC.NB_COMPETENCIA,
		CC.DS_COMPETENCIA,
		CC.CL_TIPO_COMPETENCIA,
		CC.CL_CLASIFICACION,
		CCC.CL_COLOR,
		CV.NB_PRUEBA,
		CF.NB_ABREVIATURA
	FROM IDP.K_RESULTADO KR
		INNER JOIN IDP.C_VARIABLE CV ON KR.ID_VARIABLE = CV.ID_VARIABLE
		INNER JOIN IDP.C_FACTOR CF ON CV.ID_VARIABLE = CF.ID_VARIABLE
		INNER JOIN IDP.K_COMPETENCIA_FACTOR KCF ON CF.ID_FACTOR = KCF.ID_FACTOR
		INNER JOIN ADM.C_COMPETENCIA CC ON KCF.ID_COMPETENCIA = CC.ID_COMPETENCIA
		INNER JOIN ADM.C_CLASIFICACION_COMPETENCIA CCC ON CC.CL_CLASIFICACION = CCC.CL_CLASIFICACION
	WHERE KR.ID_CUESTIONARIO = @PIN_ID_CUESTIONARIO_BAREMOS

)