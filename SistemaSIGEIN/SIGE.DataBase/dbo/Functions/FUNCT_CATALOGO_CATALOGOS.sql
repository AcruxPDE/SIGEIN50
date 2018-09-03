-- =============================================
-- Proyecto: Sigein 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Felipe Hernández Palafox
-- CREATE date: 07/12/2015
-- Description: obtiene catalogo de catalogos
-- =============================================
CREATE FUNCTION [dbo].[FUNCT_CATALOGO_CATALOGOS](@IdCatalogo int) 
RETURNS TABLE 
AS
RETURN 
(
	SELECT 
			  [ID_CATALOGO_VALOR] 
			, [CL_CATALOGO_VALOR] 
			, [NB_CATALOGO_VALOR]
			, [DS_CATALOGO_VALOR]
			, CC.ID_CATALOGO_LISTA
			, CL.NB_CATALOGO_LISTA 
			FROM ADM.C_CATALOGO_VALOR CC
			JOIN ADM.C_CATALOGO_LISTA CL ON CC.ID_CATALOGO_LISTA = CL.ID_CATALOGO_LISTA 
			WHERE  CC.ID_CATALOGO_LISTA = @IdCatalogo			
)
