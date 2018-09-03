-- =============================================
-- Proyecto: Sigein 5.0
-- Copyright (c) - Acrux - 2015
-- Author: Margarita Salcedo
-- CREATE date: 14/09/2015
-- Description: Obtiene los C_ROL_FUNCION 
-- =============================================
--JVP 30/10/2015 SE MODIFICA PARA QUE SE OBTENGA LOS DATOS ADECUADOS PARA EL GRID
-- =============================================
ALTER PROCEDURE ADM.SPE_OBTIENE_C_ROL_FUNCION 
	    @PIN_ID_ROL AS int = NULL,
        @PIN_ID_FUNCION AS int = NULL

AS   
	SELECT 
		 rf.ID_ROL,
		 rf.ID_FUNCION
		, R.NB_ROL
		,R.CL_ROL
		,R.XML_AUTORIZACION
		,F.CL_FUNCION
		,F.CL_TIPO_FUNCION
		,F.NB_FUNCION
		,F.NB_URL
		,F.XML_CONFIGURACION
		,F.ID_FUNCION_PADRE
		,'' as DS_FILTRO
	FROM ADM.C_ROL_FUNCION as rf
	join Adm.C_ROL as r ON r.id_rol = rf.ID_ROL
	join ADM.S_FUNCION AS F ON F.ID_FUNCION = RF.ID_FUNCION
	WHERE (@PIN_ID_ROL IS NULL OR (@PIN_ID_ROL IS NOT NULL AND  rf.ID_ROL = @PIN_ID_ROL)) AND 
(@PIN_ID_FUNCION IS NULL OR (@PIN_ID_FUNCION IS NOT NULL AND  rf.ID_FUNCION = @PIN_ID_FUNCION)) 

GO