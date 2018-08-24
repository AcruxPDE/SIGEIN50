using System;
using System.Collections.Generic;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;


namespace SIGE.Negocio.Administracion
{
    public class NivelEscolaridadNegocio
    {
        public List<SPE_OBTIENE_C_NIVEL_ESCOLARIDAD_Result> Obtener_C_NIVEL_ESCOLARIDAD(int? ID_NIVEL_ESCOLARIDAD = null, String CL_NIVEL_ESCOLARIDAD = null, String DS_NIVEL_ESCOLARIDAD = null, String CL_TIPO_ESCOLARIDAD = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            NivelEscolaridadOperaciones operaciones = new NivelEscolaridadOperaciones();
            return operaciones.Obtener_C_NIVEL_ESCOLARIDAD(ID_NIVEL_ESCOLARIDAD, CL_NIVEL_ESCOLARIDAD, DS_NIVEL_ESCOLARIDAD, CL_TIPO_ESCOLARIDAD, FG_ACTIVO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
        }

        public E_RESULTADO InsertaActualiza_C_NIVEL_ESCOLARIDAD(string tipo_transaccion, E_NIVEL_ESCOLARIDAD V_C_NIVEL_ESCOLARIDAD, string usuario, string programa)
        {
            NivelEscolaridadOperaciones operaciones = new NivelEscolaridadOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualiza_C_NIVEL_ESCOLARIDAD(tipo_transaccion, V_C_NIVEL_ESCOLARIDAD, usuario, programa));
        }

        public E_RESULTADO Elimina_C_NIVEL_ESCOLARIDAD(int? ID_NIVEL_ESCOLARIDAD = null, string usuario = null, string programa = null)
        {
            NivelEscolaridadOperaciones operaciones = new NivelEscolaridadOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Elimina_C_NIVEL_ESCOLARIDAD(ID_NIVEL_ESCOLARIDAD, usuario, programa));
        }

        public List<E_TIPO_ESCOLARIDAD> Obtener_VW_TIPO_ESCOLARIDAD(int? ID_NIVEL_ESCOLARIDAD = null, String DS_NIVEL_ESCOLARIDAD = null, String CL_TIPO_ESCOLARIDAD = null)
        {
            NivelEscolaridadOperaciones operaciones = new NivelEscolaridadOperaciones();

            List<SPE_OBTIENE_VW_TIPO_ESCOLARIDAD_Result> Tipos = operaciones.Obtener_VW_TIPO_ESCOLARIDAD(ID_NIVEL_ESCOLARIDAD, DS_NIVEL_ESCOLARIDAD, CL_TIPO_ESCOLARIDAD);

            List<E_TIPO_ESCOLARIDAD> lista_result = new List<E_TIPO_ESCOLARIDAD>();

            foreach (SPE_OBTIENE_VW_TIPO_ESCOLARIDAD_Result vtipoEsco in Tipos)
            {
                lista_result.Add( new E_TIPO_ESCOLARIDAD {CL_TIPO_ESCOLARIDAD = vtipoEsco.CL_TIPO_ESCOLARIDAD, DS_NIVEL_ESCOLARIDAD = vtipoEsco.DS_NIVEL_ESCOLARIDAD, ID_NIVEL_ESCOLARIDAD = vtipoEsco.ID_NIVEL_ESCOLARIDAD});
            }

            return lista_result;
        }        
    }
}
