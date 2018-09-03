-- =============================================
-- Proyecto: Sigein 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 09/11/2015
-- Description: Obtiene los datos de la colonia
-- =============================================

ALTER PROCEDURE ADM.SPE_OBTIENE_DATOS_CP
	@PIN_CL_CODIGO_POSTAL AS nvarchar(10) = NULL
AS

	SELECT CO.ID_COLONIA
		  , CO.CL_PAIS
		  , CO.CL_ESTADO
		  , CO.CL_MUNICIPIO
		  , CO.CL_COLONIA
		  , CO.NB_COLONIA
		  , CO.CL_TIPO_ASENTAMIENTO
		  , CO.CL_CODIGO_POSTAL
		  , MU.NB_MUNICIPIO 
		  , ES.NB_ESTADO
	FROM ADM.C_COLONIA CO
	JOIN ADM.C_MUNICIPIO MU ON CO.CL_MUNICIPIO = MU.CL_MUNICIPIO
	JOIN ADM.C_ESTADO ES ON CO.CL_ESTADO = ES.CL_ESTADO 
	WHERE (@PIN_CL_CODIGO_POSTAL IS NULL OR (@PIN_CL_CODIGO_POSTAL IS NOT NULL AND CL_CODIGO_POSTAL = @PIN_CL_CODIGO_POSTAL))
