using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades.Administracion;
using SIGE.Entidades;
using System.Data.Objects;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{

    public class EvaluadorExternoOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTIENE DATOS  C_EVALUADOR_EXTERNO
        public List<SPE_OBTIENE_C_EVALUADOR_EXTERNO_Result> Obtener_C_EVALUADOR_EXTERNO(int? ID_EVALUADOR_EXTERNO = null, String CL_EVALUADOR_EXTERNO = null, String NB_EVALUADOR_EXTERNO = null, String DS_EVALUARDO_EXTERNO = null, bool? FG_ACTIVO = null, String XML_CAMPOS_ADICIONALES = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_C_EVALUADOR_EXTERNO in context.SPE_OBTIENE_C_EVALUADOR_EXTERNO(ID_EVALUADOR_EXTERNO, CL_EVALUADOR_EXTERNO, NB_EVALUADOR_EXTERNO, DS_EVALUARDO_EXTERNO, FG_ACTIVO, XML_CAMPOS_ADICIONALES)
                        select V_C_EVALUADOR_EXTERNO;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_EVALUADOR_EXTERNO
        public int InsertaActualiza_C_EVALUADOR_EXTERNO(string tipo_transaccion, SPE_OBTIENE_C_EVALUADOR_EXTERNO_Result V_C_EVALUADOR_EXTERNO, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_INSERTA_ACTUALIZA_C_EVALUADOR_EXTERNO(pout_clave_retorno, V_C_EVALUADOR_EXTERNO.ID_EVALUADOR_EXTERNO, V_C_EVALUADOR_EXTERNO.CL_EVALUADOR_EXTERNO, V_C_EVALUADOR_EXTERNO.NB_EVALUADOR_EXTERNO, V_C_EVALUADOR_EXTERNO.DS_EVALUARDO_EXTERNO, V_C_EVALUADOR_EXTERNO.FG_ACTIVO, V_C_EVALUADOR_EXTERNO.XML_CAMPOS_ADICIONALES, usuario, usuario, programa, programa, tipo_transaccion);
                //regresamos el valor de retorno de sql
                return Convert.ToInt32(pout_clave_retorno.Value); ;
            }
        }
        #endregion

        #region ELIMINA DATOS  C_EVALUADOR_EXTERNO
        public int Elimina_C_EVALUADOR_EXTERNO(int? ID_EVALUADOR_EXTERNO = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_ELIMINA_C_EVALUADOR_EXTERNO(pout_clave_retorno, ID_EVALUADOR_EXTERNO, usuario, programa);
                //regresamos el valor de retorno de sql				
                return Convert.ToInt32(pout_clave_retorno.Value);
            }
        }
        #endregion

    }
}
