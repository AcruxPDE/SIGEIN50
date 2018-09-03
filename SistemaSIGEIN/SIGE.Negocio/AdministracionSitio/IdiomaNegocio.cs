using System;
using System.Collections.Generic;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;

namespace SIGE.Negocio.Administracion
{
    public class IdiomaNegocio
    {
        public List<SPE_OBTIENE_C_IDIOMA_Result> Obtener_C_IDIOMA(int? ID_IDIOMA = null, String CL_IDIOMA = null, String NB_IDIOMA = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            IdiomaOperaciones operaciones = new IdiomaOperaciones();
            return operaciones.Obtener_C_IDIOMA(ID_IDIOMA, CL_IDIOMA, NB_IDIOMA, FG_ACTIVO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
        }

        public E_RESULTADO InsertaActualiza_C_IDIOMA(string tipo_transaccion, SPE_OBTIENE_C_IDIOMA_Result V_C_IDIOMA, string usuario, string programa)
        {
            IdiomaOperaciones operaciones = new IdiomaOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualiza_C_IDIOMA(tipo_transaccion, V_C_IDIOMA, usuario, programa));
        }

        public E_RESULTADO Elimina_C_IDIOMA(int? ID_IDIOMA = null, string usuario = null, string programa = null)
        {
            IdiomaOperaciones operaciones = new IdiomaOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Elimina_C_IDIOMA(ID_IDIOMA, usuario, programa));
        }        
    }
}