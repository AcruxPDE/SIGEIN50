--=============================================================================================================================
--=============================================================================================================================
SET ANSI_NULLS ON 
GO
SET QUOTED_IDENTIFIER ON 
GO
-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Jesús Vázquez Pereyra
-- CREATE date: 28/10/2015
-- Description: Obtiene LA FECHA DE LA BASE DE DATOS
-- =============================================
ALTER PROCEDURE ADM.SPE_OBTIENE_FECHA
AS
	SELECT GETDATE() AS FE_BD