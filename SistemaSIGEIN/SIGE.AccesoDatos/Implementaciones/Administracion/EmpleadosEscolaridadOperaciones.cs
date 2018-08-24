using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using System.Data.Objects;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{

    public class EmpleadosEscolaridadOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTIENE DATOS  C_EMPLEADO_ESCOLARIDAD
        public List<SPE_OBTIENE_C_EMPLEADO_ESCOLARIDAD_Result> Obtener_C_EMPLEADO_ESCOLARIDAD(int? ID_EMPLEADO_ESCOLARIDAD = null, int? ID_EMPLEADO = null, int? ID_CANDIDATO = null, int? ID_ESCOLARIDAD = null, int? CL_INSTITUCION = null, String NB_INSTITUCION = null, DateTime? FE_PERIODO_INICIO = null, DateTime? FE_PERIODO_FIN = null, String CL_ESTADO_ESCOLARIDAD = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_C_EMPLEADO_ESCOLARIDAD in context.SPE_OBTIENE_C_EMPLEADO_ESCOLARIDAD(ID_EMPLEADO_ESCOLARIDAD, ID_EMPLEADO, ID_CANDIDATO, ID_ESCOLARIDAD, CL_INSTITUCION, NB_INSTITUCION, FE_PERIODO_INICIO, FE_PERIODO_FIN, CL_ESTADO_ESCOLARIDAD)
                        select V_C_EMPLEADO_ESCOLARIDAD;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_EMPLEADO_ESCOLARIDAD
        public int InsertaActualiza_C_EMPLEADO_ESCOLARIDAD(string usuario, string programa, XElement archivo)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_INSERTA_ACTUALIZA_C_EMPLEADO_ESCOLARIDAD(pout_clave_retorno, usuario, usuario, programa, programa, archivo.ToString());
                return Convert.ToInt32(pout_clave_retorno.Value); ;
            }
        }
        #endregion

        #region ELIMINA DATOS  C_EMPLEADO_ESCOLARIDAD
        public int Elimina_C_EMPLEADO_ESCOLARIDAD(int? ID_EMPLEADO_ESCOLARIDAD = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_ELIMINA_C_EMPLEADO_ESCOLARIDAD(pout_clave_retorno, ID_EMPLEADO_ESCOLARIDAD, usuario, programa);
                //regresamos el valor de retorno de sql				
                return Convert.ToInt32(pout_clave_retorno.Value);
            }
        }
        #endregion

    }
}