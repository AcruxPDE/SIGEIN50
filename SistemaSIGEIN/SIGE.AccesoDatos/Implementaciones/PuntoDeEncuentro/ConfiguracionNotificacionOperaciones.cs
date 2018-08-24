using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SIGE.Entidades;
using SIGE.Entidades.PuntoDeEncuentro;

namespace SIGE.AccesoDatos.Implementaciones.PuntoDeEncuentro
{
    public class ConfiguracionNotificacionOperaciones
    {
        private SistemaSigeinEntities context;

        public List<SPE_OBTIENE_CONFIGURACION_NOTIFICACION_Result> ObtenerNotificaciones(int? ID_CONFIGURACION_NOTIFICACION = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_CONFIGURACION_NOTIFICACION(ID_CONFIGURACION_NOTIFICACION).ToList();
            }
        }


        public XElement INSERTA_ACTUALIZA_NOTIFICACION_CONFIGURACION(string tipo_transaccion, E_CONFIGURACION_NOTIFICACION V_C_DS_NOTIFICACION, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_CONFIGURACION_NOTIFICACION(pout_clave_retorno,V_C_DS_NOTIFICACION.ID_CONFIGURACION_NOTIFICACION, V_C_DS_NOTIFICACION.XML_INSTRUCCION, usuario, programa, tipo_transaccion);// V_C_DS_NOTIFICACION.ID_CONFIGURACION_NOTIFICACION
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        public List<SPE_OBTIENE_M_PUESTO_ADMINISTRACION_PDE_Result> ObtienePuestos()
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_M_PUESTO_ADMINISTRACION_PDE().ToList();
            }
        }
        public string ObtienePuestoEmpleado(string xml, string cl_usuario)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_PDE_CONFIGURACION_PDE(xml, cl_usuario).FirstOrDefault();
            }
        }
        public string ObtienePuestoEmpleadoNotificacion(string xml, string cl_usuario)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_PDE_CONFIGURACION_PDE_NOTIFICACION(xml, cl_usuario).FirstOrDefault();
            }
        }
        public XElement INSERTA_ACTUALIZA_EMPLEADO_PUESTO_PDE(string V_C_Puestos, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_EMPLEADO_PUESTO_PDE(pout_clave_retorno, V_C_Puestos, usuario, programa);// V_C_DS_NOTIFICACION.ID_CONFIGURACION_NOTIFICACION
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        public XElement SPE_INSERTA_ACTUALIZA_EMPLEADO_NOTIFICACION_PUESTO_PDE(string V_C_Puestos, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_EMPLEADO_NOTIFICACION_PUESTO_PDE(pout_clave_retorno, V_C_Puestos, usuario, programa);// V_C_DS_NOTIFICACION.ID_CONFIGURACION_NOTIFICACION
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        
    }
}
