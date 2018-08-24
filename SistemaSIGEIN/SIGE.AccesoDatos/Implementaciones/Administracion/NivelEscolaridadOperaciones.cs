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

    public class NivelEscolaridadOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTIENE DATOS  C_NIVEL_ESCOLARIDAD
        public List<SPE_OBTIENE_C_NIVEL_ESCOLARIDAD_Result> Obtener_C_NIVEL_ESCOLARIDAD(int? ID_NIVEL_ESCOLARIDAD = null, String CL_NIVEL_ESCOLARIDAD = null, String DS_NIVEL_ESCOLARIDAD = null, String CL_TIPO_ESCOLARIDAD = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_C_NIVEL_ESCOLARIDAD in context.SPE_OBTIENE_C_NIVEL_ESCOLARIDAD(ID_NIVEL_ESCOLARIDAD, CL_NIVEL_ESCOLARIDAD, DS_NIVEL_ESCOLARIDAD,CL_TIPO_ESCOLARIDAD, FG_ACTIVO)
                        select V_C_NIVEL_ESCOLARIDAD;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_NIVEL_ESCOLARIDAD
        public XElement InsertaActualiza_C_NIVEL_ESCOLARIDAD(string tipo_transaccion, E_NIVEL_ESCOLARIDAD V_C_NIVEL_ESCOLARIDAD, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_C_NIVEL_ESCOLARIDAD(pout_clave_retorno, V_C_NIVEL_ESCOLARIDAD.ID_NIVEL_ESCOLARIDAD, V_C_NIVEL_ESCOLARIDAD.CL_NIVEL_ESCOLARIDAD, V_C_NIVEL_ESCOLARIDAD.DS_NIVEL_ESCOLARIDAD,V_C_NIVEL_ESCOLARIDAD.CL_TIPO_ESCOLARIDAD, V_C_NIVEL_ESCOLARIDAD.FG_ACTIVO, usuario, usuario, programa, programa, tipo_transaccion);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        #endregion

        #region ELIMINA DATOS  C_NIVEL_ESCOLARIDAD
        public XElement Elimina_C_NIVEL_ESCOLARIDAD(int? ID_NIVEL_ESCOLARIDAD = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_C_NIVEL_ESCOLARIDAD(pout_clave_retorno, ID_NIVEL_ESCOLARIDAD, usuario, programa);
                //regresamos el valor de retorno de sql				
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        #endregion


        #region OBTIENE DATOS  C_NIVEL_ESCOLARIDAD
        public List<SPE_OBTIENE_VW_TIPO_ESCOLARIDAD_Result> Obtener_VW_TIPO_ESCOLARIDAD(int? ID_NIVEL_ESCOLARIDAD = null,  String DS_NIVEL_ESCOLARIDAD = null, String CL_TIPO_ESCOLARIDAD = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_C_NIVEL_ESCOLARIDAD in context.SPE_OBTIENE_VW_TIPO_ESCOLARIDAD(ID_NIVEL_ESCOLARIDAD, DS_NIVEL_ESCOLARIDAD, CL_TIPO_ESCOLARIDAD)
                        select V_C_NIVEL_ESCOLARIDAD;
                return q.ToList();
            }
        }
        #endregion

    }
}
