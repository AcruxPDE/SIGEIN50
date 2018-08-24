using System;
using System.Collections.Generic;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;


namespace SIGE.Negocio.Administracion
{
    public class PaisNegocio
    {		
		public List<SPE_OBTIENE_VW_PAIS_Result> Obtener_VW_PAIS(String CL_PAIS = null,String NB_PAIS = null)
		{
			PaisOperaciones operaciones = new PaisOperaciones();
			return operaciones.Obtener_VW_PAIS(CL_PAIS,NB_PAIS);
		}
	
	}
}