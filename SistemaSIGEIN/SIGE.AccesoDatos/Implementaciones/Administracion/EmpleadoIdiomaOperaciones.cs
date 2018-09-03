using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using System.Data.Objects;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{

    public class EmpleadoIdiomaOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTIENE DATOS  C_EMPLEADO_IDIOMA
        public List<SPE_OBTIENE_C_EMPLEADO_IDIOMA_Result> Obtener_C_EMPLEADO_IDIOMA(int? ID_EMPLEADO_IDIOMA = null, int? ID_EMPLEADO = null, int? ID_CANDIDATO = null, int? ID_IDIOMA = null, Decimal? PR_LECTURA = null, Decimal? PR_ESCRITURA = null, Decimal? PR_CONVERSACIONAL = null, int? CL_INSTITUCION = null, Decimal? NO_PUNTAJE = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_C_EMPLEADO_IDIOMA in context.SPE_OBTIENE_C_EMPLEADO_IDIOMA(ID_EMPLEADO_IDIOMA, ID_EMPLEADO, ID_CANDIDATO, ID_IDIOMA, PR_LECTURA, PR_ESCRITURA, PR_CONVERSACIONAL, CL_INSTITUCION, NO_PUNTAJE)
                        select V_C_EMPLEADO_IDIOMA;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_EMPLEADO_IDIOMA
        public int InsertaActualiza_C_EMPLEADO_IDIOMA(string tipo_transaccion, SPE_OBTIENE_C_EMPLEADO_IDIOMA_Result V_C_EMPLEADO_IDIOMA, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_INSERTA_ACTUALIZA_C_EMPLEADO_IDIOMA(pout_clave_retorno, V_C_EMPLEADO_IDIOMA.ID_EMPLEADO_IDIOMA, V_C_EMPLEADO_IDIOMA.ID_EMPLEADO, V_C_EMPLEADO_IDIOMA.ID_CANDIDATO, V_C_EMPLEADO_IDIOMA.ID_IDIOMA, V_C_EMPLEADO_IDIOMA.PR_LECTURA, V_C_EMPLEADO_IDIOMA.PR_ESCRITURA, V_C_EMPLEADO_IDIOMA.PR_CONVERSACIONAL, V_C_EMPLEADO_IDIOMA.CL_INSTITUCION, V_C_EMPLEADO_IDIOMA.NO_PUNTAJE, usuario, usuario, programa, programa, tipo_transaccion);
                //regresamos el valor de retorno de sql
                return Convert.ToInt32(pout_clave_retorno.Value); ;
            }
        }
        #endregion

        #region ELIMINA DATOS  C_EMPLEADO_IDIOMA
        public int Elimina_C_EMPLEADO_IDIOMA(int? ID_EMPLEADO_IDIOMA = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_ELIMINA_C_EMPLEADO_IDIOMA(pout_clave_retorno, ID_EMPLEADO_IDIOMA, usuario, programa);
                //regresamos el valor de retorno de sql				
                return Convert.ToInt32(pout_clave_retorno.Value);
            }
        }
        #endregion

    }
}