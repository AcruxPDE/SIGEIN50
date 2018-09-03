-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Juan De Dios Pérez Pedroza
-- CREATE date: 21/09/2015
-- Description: Obtiene el tiempo que le queda a una prueba
-- =============================================
CREATE PROCEDURE [IDP].[SPE_OBTIENE_TIEMPO_PRUEBA] 
      @PIN_ID_PRUEBA AS int = NULL
	 ,@PIN_NO_TIEMPO_PRUEBA AS INT =NULL
AS   
	BEGIN 
	    DECLARE
	    @start datetime
      , @end   datetime
	  , @DAYS INT
	  , @hours INT
	  , @MINUTES INT
	  , @SECONDS INT
	   , @SEGUNDOS INT
		SELECT @start = (SELECT FE_INICIO FROM IDP.K_PRUEBA WHERE ID_PRUEBA= @PIN_ID_PRUEBA)--'2016-01-04 11:34:50.040'--'2009-01-01'
		 , @end   = getdate()
		 , @PIN_NO_TIEMPO_PRUEBA = (SELECT NO_TIEMPO FROM IDP.K_PRUEBA WHERE ID_PRUEBA= @PIN_ID_PRUEBA)

		 SELECT @MINUTES = (DateDiff(mi, @start, @end) % 60)

		-- IF  @MINUTES > @PIN_NO_TIEMPO_PRUEBA 
		-- BEGIN 
		-- 	SELECT 
		-- @DAYS = 0
		--,@hours =0
		--,@MINUTES =0
		--,@SECONDS =0
		-- END 
		-- ELSE 
		 BEGIN
		 SELECT 
		   @DAYS =    (DateDiff(dd, @start, @end))
		 , @hours =   (DateDiff(hh, @start, @end) % 24) 
		 , @MINUTES = (DateDiff(mi, @start, @end) % 60)
		 , @SECONDS = ( DateDiff(ss, @start, @end) % 60 ) 
		 , @SEGUNDOS = ( DateDiff(ss, @start, @end) /*% 60*/ ) 
		 END 
		 ---------------------------------
		  SELECT @DAYS AS DIAS
		 , @hours AS HORAS 
		 , @MINUTES AS MINUTOS
		 , @SECONDS AS SEGUNDOS 
		  , @SEGUNDOS AS SECONDS
		 ---------------------------------
		END


		--EXECUTE [IDP].[SPE_OBTIENE_TIEMPO_PRUEBA]  4
