﻿-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 04/11/2015
-- Description: Obtiene una lista de años desde el actual y 50 atrás
-- =============================================

CREATE PROCEDURE [ADM].[SP_OBTIENE_ANIOS]
AS
		DECLARE @TABLE TABLE (numero int IDENTITY(1,1), list_year INT)
 
		DECLARE @YEAR INT,
				@ULTIMO INT 
 
		SET @YEAR= YEAR(GETDATE())
		SET @ULTIMO = @YEAR - 70

		while @year >= @ULTIMO
		BEGIN
			INSERT INTO @TABLE VALUES (@YEAR)
			SET @YEAR = @YEAR-1       
		END

		SELECT numero, CAST(list_year AS NVARCHAR(20)) AS list_year 
		FROM @TABLE
 