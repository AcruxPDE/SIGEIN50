-- =============================================
-- Proyecto: Sistema SIGEIN 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- Create date: 05/11/2015
-- Description: Obtiene el folio de secuencia
-- =============================================

CREATE PROCEDURE [ADM].[SPE_OBTIENE_FOLIO_SECUENCIA]
		@P_CL_SECUENCIA VARCHAR(30)
AS     
 BEGIN  		 
		
				DECLARE @P_NO_SECUENCIA NVARCHAR(20)

				  SET @P_NO_SECUENCIA=(SELECT TOP 1 
											  (CASE WHEN CS.CL_PREFIJO IS NULL THEN '' ELSE CS.CL_PREFIJO  END)
											  +RIGHT(REPLICATE('0', NO_DIGITOS)+CAST(NO_ULTIMO_VALOR AS VARCHAR(7)), NO_DIGITOS)
											  +(CASE WHEN CS.CL_SUFIJO IS NULL THEN '' ELSE CS.CL_SUFIJO  END)
										 FROM ADM.C_SECUENCIA CS WHERE CL_SECUENCIA=@P_CL_SECUENCIA)
          
				  UPDATE ADM.C_SECUENCIA SET NO_ULTIMO_VALOR=NO_ULTIMO_VALOR+1 WHERE CL_SECUENCIA=@P_CL_SECUENCIA;	


				  SELECT '' AS NB_SECUENCIA, @P_NO_SECUENCIA AS NO_SECUENCIA
				  							           
		
 END