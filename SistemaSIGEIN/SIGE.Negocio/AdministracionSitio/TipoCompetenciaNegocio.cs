using System;
using System.Collections.Generic;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Negocio.Utilerias;
using SIGE.Entidades.Externas; // reemplazar por la carpeta correspondiente
//using SIGE.Entidades.Administracion;

namespace SIGE.Negocio.Administracion  // reemplazar por la carpeta correspondiente
{
    public class TipoCompetenciaNegocio
    {
        public List<SPE_OBTIENE_S_TIPO_COMPETENCIA_Result> ObtieneTipoCompetencia(string pClTipoCompetencia = null, string pNbTipoCompetencia = null, string pDsTipoCompetencia = null, bool? pFgActivo = null)
        {
            TipoCompetenciasOperaciones oTipoCompetencia = new TipoCompetenciasOperaciones();
            return oTipoCompetencia.ObtenerTipoCompetencia(pClTipoCompetencia, pNbTipoCompetencia, pDsTipoCompetencia, pFgActivo);
        }

        //public List<SPE_OBTIENE_S_TIPO_COMPETENCIA_Result> Obtener_TIPO_COMPETENCIA(string CL_TIPO_COMPETENCIA = null, string NB_TIPO_COMPETENCIA = null, string DS_TIPO_COMPETENCIA = null)
        //{
        //    TipoCompetenciasOperaciones operaciones = new TipoCompetenciasOperaciones();
        //    return operaciones.ObtenerTipoCompetencia(CL_TIPO_COMPETENCIA, NB_TIPO_COMPETENCIA, DS_TIPO_COMPETENCIA);
        //}

        public E_RESULTADO InsertaActualizaTipoCompetencia(string pTipoTransaccion, SPE_OBTIENE_S_TIPO_COMPETENCIA_Result pTipoCompetencia, string pClUsuario, string pNbPrograma)
        {
            TipoCompetenciasOperaciones oTipoCompetencia = new TipoCompetenciasOperaciones();
            return UtilRespuesta.EnvioRespuesta(oTipoCompetencia.InsertarActualizarTipoCompetencia(pTipoTransaccion, pTipoCompetencia, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO EliminaTipoCompetencia(String pClTipoCompetencia = null, string pClUsuario = null, string pNbPrograma = null)
        {
            TipoCompetenciasOperaciones oTipoCompetencia = new TipoCompetenciasOperaciones();
            return UtilRespuesta.EnvioRespuesta(oTipoCompetencia.EliminarTipoCompetencia(pClTipoCompetencia, pClUsuario, pNbPrograma));
        }        
    }
}