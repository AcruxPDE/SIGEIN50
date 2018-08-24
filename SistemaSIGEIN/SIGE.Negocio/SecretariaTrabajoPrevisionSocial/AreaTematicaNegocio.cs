using SIGE.AccesoDatos.Implementaciones.SecretariaTrabajoPrevisionSocial;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.SecretariaTrabajoPrevisionSocial;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Negocio.SecretariaTrabajoPrevisionSocial
{
    public class AreaTematicaNegocio
    {

        #region OBTIENE DATOS  C_AREA_TEMATICA
        public List<SPE_OBTIENE_C_AREA_TEMATICA_Result> Obtener_C_AREA_TEMATICA(int? ID_AREA_TEMATICA = null, String CL_AREA_TEMATICA = null, String NB_AREA_TEMATICA = null)
        {
            AreasTematicasOperaciones operaciones = new AreasTematicasOperaciones();
            return operaciones.Obtener_SPE_OBTIENE_C_AREA_TEMATICA(ID_AREA_TEMATICA, CL_AREA_TEMATICA, NB_AREA_TEMATICA);
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_AREA_TEMATICA
        public E_RESULTADO InsertaActualiza_C_AREA_TEMATICA(string tipo_transaccion, E_AREA_TEMATICA V_C_AREAT, string usuario, string programa)
        {
            AreasTematicasOperaciones operaciones = new AreasTematicasOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualiza_C_AREA_TEMATICA(tipo_transaccion, V_C_AREAT, usuario, programa));
        }
        #endregion

        #region ELIMINA DATOS  C_AREA_TEMATICA
        public E_RESULTADO Elimina_C_AREA_TEMATICA(int? ID_AREA_TEMATICA = null, string usuario = null, string programa = null)
        {
            AreasTematicasOperaciones operaciones = new AreasTematicasOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Elimina_C_AREA_TEMATICA(ID_AREA_TEMATICA, usuario, programa));
        }
        #endregion

        #region OBTIENE DATOS AREA_TEMATICA_CURSO
        public List<SPE_OBTIENE_AREA_TEMATICA_CURSO_Result> Obtener_AREA_TEMATICA_CURSO(int? ID_AREA_TEMATICA_CURSO = null, int? ID_CURSO = null, int? ID_AREA_TEMATICA = null, String CL_AREA_TEMATICA = null, String NB_AREA_TEMATICA = null, String CL_CURSO = null, String NB_CURSO = null)
        {
            AreasTematicasOperaciones operaciones = new AreasTematicasOperaciones();
            return operaciones.Obtener_SPE_OBTIENE_C_AREA_TEMATICA_CURSO(ID_AREA_TEMATICA_CURSO,ID_CURSO,ID_AREA_TEMATICA, CL_AREA_TEMATICA, NB_AREA_TEMATICA,CL_CURSO,NB_CURSO);
        }
        #endregion

        #region ELIMINA DATOS K_AREA_TEMATICA_CURSO
        public E_RESULTADO Elimina_K_AREA_TEMATICA_CURSO(int? ID_AREA_TEMATICA = null, int? ID_CURSO = null, string usuario = null, string programa = null)
        {
            AreasTematicasOperaciones operaciones = new AreasTematicasOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Elimina_K_AREA_TEMATICA_CURSO(ID_AREA_TEMATICA,ID_CURSO, usuario, programa));
        }
        #endregion
    }
}
