using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using System.Data.Objects;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{

    public class ParienteOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTIENE DATOS  C_PARIENTE
        public List<SPE_OBTIENE_C_PARIENTE_Result> Obtener_C_PARIENTE(int? ID_PARIENTE = null, String NB_PARIENTE = null, String CL_PARENTEZCO = null, String CL_GENERO = null, DateTime? FE_NACIMIENTO = null, int? ID_EMPLEADO = null, int? ID_CANDIDATO = null, int? ID_BITACORA = null, String CL_OCUPACION = null, bool? FG_DEPENDIENTE = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_C_PARIENTE in context.SPE_OBTIENE_C_PARIENTE(ID_PARIENTE, NB_PARIENTE, CL_PARENTEZCO, CL_GENERO, FE_NACIMIENTO, ID_EMPLEADO, ID_CANDIDATO, ID_BITACORA, CL_OCUPACION, FG_DEPENDIENTE, FG_ACTIVO)
                        select V_C_PARIENTE;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_PARIENTE
        public int InsertaActualiza_C_PARIENTE(string tipo_transaccion, SPE_OBTIENE_C_PARIENTE_Result V_C_PARIENTE, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_INSERTA_ACTUALIZA_C_PARIENTE(pout_clave_retorno, V_C_PARIENTE.ID_PARIENTE, V_C_PARIENTE.NB_PARIENTE, V_C_PARIENTE.CL_PARENTEZCO, V_C_PARIENTE.CL_GENERO, V_C_PARIENTE.FE_NACIMIENTO, V_C_PARIENTE.ID_EMPLEADO, V_C_PARIENTE.ID_CANDIDATO, V_C_PARIENTE.ID_BITACORA, V_C_PARIENTE.CL_OCUPACION, V_C_PARIENTE.FG_DEPENDIENTE, V_C_PARIENTE.FG_ACTIVO, usuario, usuario, programa, programa, tipo_transaccion);
                //regresamos el valor de retorno de sql
                return Convert.ToInt32(pout_clave_retorno.Value); ;
            }
        }
        #endregion

        #region ELIMINA DATOS  C_PARIENTE
        public int Elimina_C_PARIENTE(int? ID_PARIENTE = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_ELIMINA_C_PARIENTE(pout_clave_retorno, ID_PARIENTE, usuario, programa);
                //regresamos el valor de retorno de sql				
                return Convert.ToInt32(pout_clave_retorno.Value);
            }
        }
        #endregion

    }
}