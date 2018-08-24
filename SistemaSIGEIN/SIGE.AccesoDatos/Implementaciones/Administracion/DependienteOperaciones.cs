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

    public class DependienteOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTIENE DATOS  C_DEPENDIENTE_ECONOMICO
        public List<SPE_OBTIENE_C_DEPENDIENTE_ECONOMICO_Result> Obtener_C_DEPENDIENTE_ECONOMICO(int? ID_DEPENDIENTE_ECONOMICO = null, String NB_DEPENDIENTE_ECONOMICO = null, String CL_PARENTEZCO = null, String CL_GENERO = null, DateTime? FE_NACIMIENTO = null, int? ID_BITACORA = null, bool? CL_OCUPACION = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_C_DEPENDIENTE_ECONOMICO in context.SPE_OBTIENE_C_DEPENDIENTE_ECONOMICO(ID_DEPENDIENTE_ECONOMICO, NB_DEPENDIENTE_ECONOMICO, CL_PARENTEZCO, CL_GENERO, FE_NACIMIENTO, ID_BITACORA, CL_OCUPACION, FG_ACTIVO)
                        select V_C_DEPENDIENTE_ECONOMICO;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_DEPENDIENTE_ECONOMICO
        public int InsertaActualiza_C_DEPENDIENTE_ECONOMICO(string tipo_transaccion, SPE_OBTIENE_C_DEPENDIENTE_ECONOMICO_Result V_C_DEPENDIENTE_ECONOMICO, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_INSERTA_ACTUALIZA_C_DEPENDIENTE_ECONOMICO(pout_clave_retorno, V_C_DEPENDIENTE_ECONOMICO.ID_DEPENDIENTE_ECONOMICO, V_C_DEPENDIENTE_ECONOMICO.NB_DEPENDIENTE_ECONOMICO, V_C_DEPENDIENTE_ECONOMICO.CL_PARENTEZCO, V_C_DEPENDIENTE_ECONOMICO.CL_GENERO, V_C_DEPENDIENTE_ECONOMICO.FE_NACIMIENTO, V_C_DEPENDIENTE_ECONOMICO.ID_BITACORA, V_C_DEPENDIENTE_ECONOMICO.CL_OCUPACION, V_C_DEPENDIENTE_ECONOMICO.FG_ACTIVO, usuario, usuario, programa, programa, tipo_transaccion);
                //regresamos el valor de retorno de sql
                return Convert.ToInt32(pout_clave_retorno.Value); ;
            }
        }
        #endregion

        #region ELIMINA DATOS  C_DEPENDIENTE_ECONOMICO
        public int Elimina_C_DEPENDIENTE_ECONOMICO(int? ID_DEPENDIENTE_ECONOMICO = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_ELIMINA_C_DEPENDIENTE_ECONOMICO(pout_clave_retorno, ID_DEPENDIENTE_ECONOMICO, usuario, programa);
                //regresamos el valor de retorno de sql				
                return Convert.ToInt32(pout_clave_retorno.Value);
            }
        }
        #endregion

    }
}
