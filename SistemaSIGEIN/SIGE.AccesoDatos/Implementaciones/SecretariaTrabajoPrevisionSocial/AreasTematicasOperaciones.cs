using SIGE.Entidades;
using SIGE.Entidades.SecretariaTrabajoPrevisionSocial;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.SecretariaTrabajoPrevisionSocial
{
    public class AreasTematicasOperaciones
    {
        private SistemaSigeinEntities context;

        #region OBTIENE DATOS  DE LAS AREAS TEMÁTICAS
        public List<SPE_OBTIENE_C_AREA_TEMATICA_Result> Obtener_SPE_OBTIENE_C_AREA_TEMATICA(int? PIN_ID_AREA_TEMATICA = null, string PIN_CL_AREA_TEMATICA = null, string PIN_NB_AREA_TEMATICA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_C_AREA_TEMATICA(PIN_ID_AREA_TEMATICA, PIN_CL_AREA_TEMATICA,PIN_NB_AREA_TEMATICA).ToList();
            } 
        }
        #endregion 

        #region OTBIENE LAS AREAS TEMATICAS DE LOS CURSOS
        public List<SPE_OBTIENE_AREA_TEMATICA_CURSO_Result> Obtener_SPE_OBTIENE_C_AREA_TEMATICA_CURSO(int? PIN_ID_AREA_TEMATICA_CURSO = null, int? PIN_ID_CURSO = null, int? PIN_ID_AREA_TEMATICA = null, string PIN_CL_AREA_TEMATICA = null, string PIN_NB_AREA_TEMATICA = null, string PIN_CL_CURSO = null, string PIN_NB_CURSO = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_AREA_TEMATICA_CURSO(PIN_ID_AREA_TEMATICA_CURSO,PIN_ID_CURSO,PIN_ID_AREA_TEMATICA, PIN_CL_AREA_TEMATICA, PIN_NB_AREA_TEMATICA,PIN_CL_CURSO,PIN_NB_CURSO).ToList();
            }
        }
        #endregion

        #region INSERTAR O ACTUALIZAR AREAS TEMATICAS
        public XElement InsertaActualiza_C_AREA_TEMATICA(string tipo_transaccion, E_AREA_TEMATICA V_C_AREA_TEMATICA, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                //  pout_clave_retorno.Value = "";
                context.SPE_INSERTA_ACTUALIZA_AREA_TEMATICA(pout_clave_retorno, V_C_AREA_TEMATICA.ID_AREA_TEMATICA, V_C_AREA_TEMATICA.CL_AREA_TEMATICA, V_C_AREA_TEMATICA.NB_AREA_TEMATICA,V_C_AREA_TEMATICA.FG_ACTIVO, usuario, programa, tipo_transaccion);

                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        #endregion

        #region ELIMINA DATOS  C_AREA_TEMATICA
        public XElement Elimina_C_AREA_TEMATICA(int? ID_AREA_TEMATICA = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                //  pout_clave_retorno.Value = "";
                context.SPE_ELIMINA_C_AREA_TEMATICA(pout_clave_retorno, ID_AREA_TEMATICA, usuario, programa);
                //regresamos el valor de retorno de sql				
                return XElement.Parse(pout_clave_retorno.Value.ToString());

            }
        }
        #endregion

        #region ELIMINA DATOS  K_AREA_TEMATICA_CURSO
        public XElement Elimina_K_AREA_TEMATICA_CURSO(int? ID_AREA_TEMATICA = null, int? ID_CURSO = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                //  pout_clave_retorno.Value = "";
                context.SPE_ELIMINA_C_AREA_TEMATICA_CURSO(pout_clave_retorno, ID_AREA_TEMATICA, ID_CURSO, usuario, programa);
                //regresamos el valor de retorno de sql				
                return XElement.Parse(pout_clave_retorno.Value.ToString());

            }
        }
        #endregion
    }
}
