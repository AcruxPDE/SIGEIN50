﻿-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 18/09/2015
-- Description: Obtiene los datos de la tabla S_CONFIGURACION 
-- =============================================
CREATE PROCEDURE [ADM].[SPE_OBTIENE_S_CONFIGURACION] 
	  @PIN_XML_CONFIGURACION AS xml = NULL
	, @PIN_CL_USUARIO_MODIFICA AS nvarchar(50) = NULL
	
AS   
	--SE DEVUELVE LOS REGISTROS EN BASE A LOS PARAMETROS INSERTADOS
	DECLARE @XML_CONFIGURACION XML = (
		SELECT TOP 1 XML_CONFIGURACION FROM ADM.S_CONFIGURACION
	)			

	DECLARE @FI_LOGOTIPO VARBINARY(MAX) = (
		SELECT TOP 1 FI_ARCHIVO FROM ADM.K_ARCHIVO WHERE ID_ARCHIVO = @XML_CONFIGURACION.value('/CONFIGURACIONES[1]/EMPRESA[1]/LOGOTIPO[1]/@ID_ARCHIVO[1]', 'UNIQUEIDENTIFIER')
	)

	SELECT @XML_CONFIGURACION AS XML_CONFIGURACION
		, @FI_LOGOTIPO AS FI_LOGOTIPO
			