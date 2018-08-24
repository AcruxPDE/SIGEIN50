using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.AccesoDatos.Implementaciones.PuntoDeEncuentro;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.PuntoDeEncuentro;
using SIGE.Negocio.Utilerias;

namespace SIGE.Negocio.PuntoDeEncuentro
{
    public class ConfiguracionNotificacionNegocio
    {
        public List<SPE_OBTIENE_CONFIGURACION_NOTIFICACION_Result> ObtenerNotificaciones(int? ID_CONFIGURACION_NOTIFICACION = null)
        {
            ConfiguracionNotificacionOperaciones operaciones = new ConfiguracionNotificacionOperaciones();
            return operaciones.ObtenerNotificaciones(ID_CONFIGURACION_NOTIFICACION).ToList();
        }

        public E_RESULTADO INSERTA_ACTUALIZA_CONFIGURACION_NOTIFICACION(string tipo_transaccion, E_CONFIGURACION_NOTIFICACION pDescripcionNotificacion, string usuario, string programa)
        {
            ConfiguracionNotificacionOperaciones operaciones = new ConfiguracionNotificacionOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.INSERTA_ACTUALIZA_NOTIFICACION_CONFIGURACION(tipo_transaccion, pDescripcionNotificacion, usuario, programa));
        }
        public List<SPE_OBTIENE_M_PUESTO_ADMINISTRACION_PDE_Result> ObtienePuestos()
        {
            ConfiguracionNotificacionOperaciones operaciones = new ConfiguracionNotificacionOperaciones();
            return operaciones.ObtienePuestos();

        }
        public string ObtienePuestoEmpleado(string xml, string cl_usuario)
        {
            ConfiguracionNotificacionOperaciones operaciones = new ConfiguracionNotificacionOperaciones();
            return operaciones.ObtienePuestoEmpleado(xml, cl_usuario);

        }
        public string ObtienePuestoEmpleadoNotificacion(string xml, string cl_usuario)
        {
            ConfiguracionNotificacionOperaciones operaciones = new ConfiguracionNotificacionOperaciones();
            return operaciones.ObtienePuestoEmpleadoNotificacion(xml, cl_usuario);

        }
        public E_RESULTADO INSERTA_ACTUALIZA_EMPLEADO_PUESTO_PDE(string V_C_Puestos, string usuario, string programa)
        {
            ConfiguracionNotificacionOperaciones operaciones = new ConfiguracionNotificacionOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.INSERTA_ACTUALIZA_EMPLEADO_PUESTO_PDE(V_C_Puestos, usuario, programa));
        }
        public E_RESULTADO SPE_INSERTA_ACTUALIZA_EMPLEADO_NOTIFICACION_PUESTO_PDE(string V_C_Puestos, string usuario, string programa)
        {
            ConfiguracionNotificacionOperaciones operaciones = new ConfiguracionNotificacionOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.SPE_INSERTA_ACTUALIZA_EMPLEADO_NOTIFICACION_PUESTO_PDE(V_C_Puestos, usuario, programa));
        }
    }
}
