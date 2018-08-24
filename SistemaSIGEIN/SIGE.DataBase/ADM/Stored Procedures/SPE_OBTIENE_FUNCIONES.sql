-- =============================================
-- Proyecto: Sigein 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Julio Díaz
-- CREATE date: 19/11/2015
-- Description: Obtiene las funciones del sistema por tipo
-- =============================================
-- 7/1/2015 JDR Se agrega ordenamiento por el campo NO_ORDEN de manera ascendente
-- =============================================

CREATE PROCEDURE [ADM].[SPE_OBTIENE_FUNCIONES] 
        @PIN_CL_TIPO_FUNCION AS nvarchar(100) = NULL

AS   
	SELECT ID_FUNCION
		, CL_FUNCION
		, CL_TIPO_FUNCION
		, NB_FUNCION
		, ID_FUNCION_PADRE
		, NB_URL
		, XML_CONFIGURACION
	FROM ADM.S_FUNCION
	WHERE (@PIN_CL_TIPO_FUNCION IS NULL OR (@PIN_CL_TIPO_FUNCION IS NOT NULL AND CL_TIPO_FUNCION = @PIN_CL_TIPO_FUNCION))
	ORDER BY NO_ORDEN