using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;

namespace SIGE.Negocio.Administracion
{
    public class AreaInteresNegocio
    {        
        public List<SPE_OBTIENE_C_AREA_INTERES_Result> Obtener_C_AREA_INTERES(int? id_area_interes = null, string cl_area_interes = null, string nb_area_interes = null, bool? fg_activo = null, DateTime? fe_creacion = null, DateTime? fe_modificacion = null, string cl_usuario_app_crea = null, string cl_usuario_app_modifica = null, string nb_programa_crea = null, string nb_programa_modifica = null)
        {
            AreaInteresOperaciones operaciones = new AreaInteresOperaciones();
            return operaciones.Obtener_C_AREA_INTERES(id_area_interes, cl_area_interes, nb_area_interes, fg_activo, fe_creacion, fe_modificacion, cl_usuario_app_crea, cl_usuario_app_modifica, nb_programa_crea, nb_programa_modifica);
        }
                
        public E_RESULTADO InsertaActualiza_C_AREA_INTERES(string tipo_transaccion, E_AREA_INTERES v_c_area_interes, string usuario, string programa)
        {
            AreaInteresOperaciones operaciones = new AreaInteresOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualiza_C_AREA_INTERES(tipo_transaccion, v_c_area_interes, usuario, programa));
        }
                
        public E_RESULTADO Elimina_C_AREA_INTERES(int? ID_AREA_INTERES = null, string CL_AREA_INTERES = null, string usuario = null, string programa = null)
        {
            AreaInteresOperaciones operaciones = new AreaInteresOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Elimina_C_AREA_INTERES(ID_AREA_INTERES, CL_AREA_INTERES, usuario, programa));
        }
        
    }
}
