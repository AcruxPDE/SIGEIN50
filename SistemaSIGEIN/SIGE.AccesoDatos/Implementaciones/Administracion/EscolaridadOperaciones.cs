using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades.Administracion;
using SIGE.Entidades;
using System.Data.Objects;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{

    public class EscolaridadOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTIENE DATOS  C_ESCOLARIDAD
        public List<SPE_OBTIENE_C_ESCOLARIDAD_Result> Obtener_C_ESCOLARIDAD(int? ID_ESCOLARIDAD = null,String CL_ESCOLARIDAD = null, String NB_ESCOLARIDAD = null, String DS_ESCOLARIDAD = null, String CL_NIVEL_ESCOLARIDAD = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_C_ESCOLARIDAD in context.SPE_OBTIENE_C_ESCOLARIDAD(ID_ESCOLARIDAD,CL_ESCOLARIDAD, NB_ESCOLARIDAD, DS_ESCOLARIDAD, CL_NIVEL_ESCOLARIDAD, FG_ACTIVO)
                        select V_C_ESCOLARIDAD;
                return q.ToList();
            }
        }
        #endregion

        #region OBTIENE DATOS  ESCOLARIDADES
        public List<SPE_OBTIENE_ESCOLARIDADES_Result> Obtener_Escolaridades(int? ID_ESCOLARIDAD = null, String NB_ESCOLARIDAD = null, String DS_ESCOLARIDAD = null, String CL_NIVEL_ESCOLARIDAD = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null, int? ID_NIVEL_ESCOLARIDAD = null, String NB_NIVEL_ESCOLARIDAD =null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_C_ESCOLARIDADES in context.SPE_OBTIENE_ESCOLARIDADES(ID_ESCOLARIDAD, NB_ESCOLARIDAD, DS_ESCOLARIDAD, CL_NIVEL_ESCOLARIDAD, FG_ACTIVO, ID_NIVEL_ESCOLARIDAD,NB_NIVEL_ESCOLARIDAD)
                        select V_C_ESCOLARIDADES;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_ESCOLARIDAD
        public XElement InsertaActualiza_C_ESCOLARIDAD(string tipo_transaccion, E_ESCOLARIDAD V_C_ESCOLARIDAD, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                ///pout_clave_retorno.Value = "";
                //ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_INSERTA_ACTUALIZA_C_ESCOLARIDAD(pout_clave_retorno, V_C_ESCOLARIDAD.ID_ESCOLARIDAD, V_C_ESCOLARIDAD.CL_ESCOLARIDAD, V_C_ESCOLARIDAD.NB_ESCOLARIDAD, V_C_ESCOLARIDAD.DS_ESCOLARIDAD, V_C_ESCOLARIDAD.CL_NIVEL_ESCOLARIDAD,V_C_ESCOLARIDAD.CL_INSTITUCION, V_C_ESCOLARIDAD.FG_ACTIVO, usuario, usuario, programa, programa, tipo_transaccion);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pout_clave_retorno.Value.ToString());

            }
        }
        #endregion

        #region ELIMINA DATOS  C_ESCOLARIDAD
        public XElement Elimina_C_ESCOLARIDAD(int? ID_ESCOLARIDAD = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
               // ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
               // pout_clave_retorno.Value = "";
                context.SPE_ELIMINA_C_ESCOLARIDAD(pout_clave_retorno,ID_ESCOLARIDAD, usuario, programa);
                //regresamos el valor de retorno de sql				
                return XElement.Parse(pout_clave_retorno.Value.ToString());
                //  return int.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        #endregion

    }
}
