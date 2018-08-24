-- =============================================
-- Proyecto: Sigein 5.0
-- Copyright (c) - Acrux - 2016
-- Author: GABRIEL VAZQUEZ
-- CREATE date: 21/01/2016
-- Description: Obtiene los datos de las competencias genericas del puesto
-- =============================================
CREATE PROCEDURE IDP.SPE_REP_RESULT_PUESTO_CAMPOS_ADICIONALES
	@PIN_ID_PUESTO AS INT
AS
BEGIN

	DECLARE @XML_CAMPOS_ADICIONALES AS XML
	
	SELECT @XML_CAMPOS_ADICIONALES = XML_CAMPOS_ADICIONALES
	FROM ADM.M_PUESTO
	WHERE ID_PUESTO = @PIN_ID_PUESTO

	/*SELECT
		d.value('@NB_CAMPO', 'NVARCHAR(50)'),
		d.value('@DS_VALOR', 'NVARCHAR(100)')
	FROM @XML_CAMPOS_ADICIONALES.nodes('CAMPOS/CAMPO') AS T(d)*/



	SELECT cca.NB_CAMPO, pca.NO_VALOR
	FROM ADM.C_CAMPO_ADICIONAL cca
		INNER JOIN (
			SELECT
				d.value('@ID_CAMPO', 'NVARCHAR(50)') AS ID_CAMPO,
				CASE WHEN d.value('@DS_VALOR', 'NVARCHAR(100)') = '' THEN d.value('@NO_VALOR', 'NVARCHAR(100)') ELSE d.value('@DS_VALOR', 'NVARCHAR(100)') END AS NO_VALOR
			FROM @XML_CAMPOS_ADICIONALES.nodes('CAMPOS/CAMPO') AS T(d)
		) AS pca ON cca.CL_CAMPO = pca.ID_CAMPO

END