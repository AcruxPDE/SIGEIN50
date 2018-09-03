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
    public class OcupacionesOperaciones
    {
        private SistemaSigeinEntities context;

        #region DATOS  DE LAS AREAS 
        public List<SPE_OBTIENE_AREA_OCUPACION_Result> Obtener_SPE_OBTIENE_AREA_OCUPACION(int? PIN_ID_AREA = null, string PIN_CL_AREA= null, string PIN_NB_AREA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_AREA_OCUPACION(PIN_ID_AREA, PIN_CL_AREA, PIN_NB_AREA).ToList();
            }
        }

        public XElement InsertaActualiza_C_AREA(string tipo_transaccion, E_AREA_OCUPACION V_C_AREA, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                //  pout_clave_retorno.Value = "";
                context.SPE_INSERTA_ACTUALIZA_C_AREA(pout_clave_retorno, V_C_AREA.ID_AREA, V_C_AREA.CL_AREA, V_C_AREA.NB_AREA, V_C_AREA.FG_ACTIVO, usuario, programa, tipo_transaccion);

                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public XElement Elimina_C_AREA(int? ID_AREA = null, string CL_AREA = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                //  pout_clave_retorno.Value = "";
                context.SPE_ELIMINA_C_AREA(pout_clave_retorno, ID_AREA, CL_AREA, usuario, programa);
                //regresamos el valor de retorno de sql				
                return XElement.Parse(pout_clave_retorno.Value.ToString());

            }
        }
        #endregion 

        #region DATOS  DE LAS SUBAREAS
        public List<SPE_OBTIENE_SUBAREA_OCUPACION_Result> Obtener_SPE_OBTIENE_SUBAREA_OCUPACION(int? PIN_ID_AREA = null, string PIN_CL_AREA = null, string PIN_NB_AREA = null, 
                                                                                                int? PIN_ID_SUBAREA = null, string PIN_CL_SUBAREA = null, string PIN_NB_SUBAREA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_SUBAREA_OCUPACION(PIN_ID_AREA, PIN_CL_AREA, PIN_NB_AREA, PIN_ID_SUBAREA, PIN_CL_SUBAREA, PIN_NB_SUBAREA).ToList();
            }
        }

        public XElement InsertaActualiza_C_SUBAREA(string tipo_transaccion, E_SUBAREA_OCUPACION V_C_SUBAREA, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                //  pout_clave_retorno.Value = "";
                context.SPE_INSERTA_ACTUALIZA_C_SUBAREA(pout_clave_retorno, V_C_SUBAREA.ID_SUBAREA, V_C_SUBAREA.CL_SUBAREA, V_C_SUBAREA.NB_SUBAREA, V_C_SUBAREA.CL_AREA, V_C_SUBAREA.FG_ACTIVO, usuario, programa, tipo_transaccion);

                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public XElement Elimina_C_SUBAREA(int? ID_SUBAREA = null, string CL_SUBAREA = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                //  pout_clave_retorno.Value = "";
                context.SPE_ELIMINA_C_SUBAREA(pout_clave_retorno, ID_SUBAREA, CL_SUBAREA, usuario, programa);
                //regresamos el valor de retorno de sql				
                return XElement.Parse(pout_clave_retorno.Value.ToString());

            }
        }
        #endregion 

        #region DATOS  DE LOS MÓDULOS
        public List<SPE_OBTIENE_MODULO_OCUPACION_Result> Obtener_SPE_OBTIENE_MODULO_OCUPACION(int? PIN_ID_AREA = null, string PIN_CL_AREA = null, string PIN_NB_AREA = null,
                                                                                              int? PIN_ID_SUBAREA = null, string PIN_CL_SUBAREA = null, string PIN_NB_SUBAREA = null,
                                                                                              int? PIN_ID_MODULO = null, string PIN_CL_MODULO = null, string PIN_NB_MODULO = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_MODULO_OCUPACION(PIN_ID_AREA, PIN_CL_AREA, PIN_NB_AREA, PIN_ID_SUBAREA, PIN_CL_SUBAREA, PIN_NB_SUBAREA,PIN_ID_MODULO,PIN_CL_MODULO,PIN_NB_MODULO).ToList();
            }
        }

        public XElement InsertaActualiza_C_MODULO(string tipo_transaccion, E_MODULO_OCUPACION V_C_MODULO, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                //  pout_clave_retorno.Value = "";
                context.SPE_INSERTA_ACTUALIZA_C_MODULO(pout_clave_retorno, V_C_MODULO.ID_MODULO, V_C_MODULO.CL_MODULO, V_C_MODULO.NB_MODULO, V_C_MODULO.FG_ACTIVO, V_C_MODULO.CL_AREA, V_C_MODULO.CL_SUBAREA, usuario, programa, tipo_transaccion);

                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public XElement Elimina_C_MODULO(int? ID_MODULO = null, string CL_MODULO = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                //  pout_clave_retorno.Value = "";
                context.SPE_ELIMINA_C_MODULO(pout_clave_retorno, ID_MODULO, CL_MODULO, usuario, programa);
                //regresamos el valor de retorno de sql				
                return XElement.Parse(pout_clave_retorno.Value.ToString());

            }
        }
        #endregion 

        #region DATOS  DE LAS OCUPACIONES
        public List<SPE_OBTIENE_OCUPACIONES_Result> Obtener_SPE_OBTIENE_OCUPACIONES(int? PIN_ID_AREA = null, string PIN_CL_AREA = null, string PIN_NB_AREA = null,
                                                                                              int? PIN_ID_SUBAREA = null, string PIN_CL_SUBAREA = null, string PIN_NB_SUBAREA = null,
                                                                                              int? PIN_ID_MODULO = null, string PIN_CL_MODULO = null, string PIN_NB_MODULO = null,
                                                                                              int? PIN_ID_OCUPACION = null, string PIN_CL_OCUPACION = null, string PIN_NB_OCUPACION = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_OCUPACIONES(PIN_ID_AREA, PIN_CL_AREA, PIN_NB_AREA, PIN_ID_SUBAREA, PIN_CL_SUBAREA, PIN_NB_SUBAREA, PIN_ID_MODULO, PIN_CL_MODULO, PIN_NB_MODULO,PIN_ID_OCUPACION,PIN_CL_OCUPACION,PIN_NB_OCUPACION).ToList();
            }
        }

        public XElement InsertaActualiza_C_OCUPACION(string tipo_transaccion, E_OCUPACION V_C_OCUPACION, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                //  pout_clave_retorno.Value = "";
                context.SPE_INSERTA_ACTUALIZA_C_OCUPACION(pout_clave_retorno, V_C_OCUPACION.ID_OCUPACION, V_C_OCUPACION.CL_OCUPACION, V_C_OCUPACION.NB_OCUPACION, V_C_OCUPACION.FG_ACTIVO, V_C_OCUPACION.CL_AREA, V_C_OCUPACION.CL_SUBAREA, V_C_OCUPACION.CL_MODULO, usuario, programa, tipo_transaccion);

                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public XElement Elimina_C_OCUPACION(int? ID_OCUPACION = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                //  pout_clave_retorno.Value = "";
                context.SPE_ELIMINA_C_OCUPACION(pout_clave_retorno, ID_OCUPACION, usuario, programa);
                //regresamos el valor de retorno de sql				
                return XElement.Parse(pout_clave_retorno.Value.ToString());

            }
        }
        #endregion 

        #region ELIMINA DATOS  K_PUESTO_OCUPACION
        public XElement Elimina_K_PUESTO_OCUPACION(int? ID_OCUPACION = null, int? ID_PUESTO = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                //  pout_clave_retorno.Value = "";
                context.SPE_ELIMINA_K_PUESTO_OCUPACION(pout_clave_retorno, ID_OCUPACION, ID_PUESTO, usuario, programa);
                //regresamos el valor de retorno de sql				
                return XElement.Parse(pout_clave_retorno.Value.ToString());

            }
        }
        #endregion
    }
}
