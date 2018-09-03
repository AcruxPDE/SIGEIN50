using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades.PuntoDeEncuentro;
using SIGE.Entidades;
using System.Data.Objects;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data;

namespace SIGE.AccesoDatos.Implementaciones.PuntoDeEncuentro
{
    public class MenuOperaciones
    {
        private SistemaSigeinEntities context;


        public List<SPE_OBTIENE_K_COMUNICADO_EMPLEADO_Result> ObtenerComunicadoEmpleado(string ID_EMPLEADO = null, bool? FG_LEIDO = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_K_COMUNICADO_EMPLEADO(ID_EMPLEADO, FG_LEIDO).ToList();
            }
        }

        public XElement ActualizarComunicadoNoLeido(int ID_COMUNICADO, string ID_EMPLEADO, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ACTUALIZA_EMPLEADO_COMUNICADO_NO_LEIDO(pOutClRetorno, ID_COMUNICADO, ID_EMPLEADO, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        
        }

        public List<SPE_OBTIENE_MODIFICACIONES_INFORMACION_EMPLEADO_Result> ObtenerComunicadoModificacionesEmpleado(string ID_EMPLEADO = null, string ID_PUESTO = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_MODIFICACIONES_INFORMACION_EMPLEADO(ID_EMPLEADO, ID_PUESTO).ToList();
            }
        }

        public List<SPE_OBTIENE_NOTIFICACIONES_A_RRHH_Result> ObtenerNotificacionesRRHH(string  ID_EMPLEADO = null, string CL_ESTADO="")
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_NOTIFICACIONES_A_RRHH(ID_EMPLEADO, CL_ESTADO).ToList();
            }
        }

        public List<SPE_OBTIENE_NOTIFICACIONES_PENDIENTES_Result> ObtenerNotificacionesPendientes(string  ID_EMPLEADO = null, int? IDNOTIFICACION_RRHH = null, string CL_ESTADO="")
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_NOTIFICACIONES_PENDIENTES(ID_EMPLEADO, IDNOTIFICACION_RRHH, CL_ESTADO).ToList();
            }
        }

        public List<SPE_OBTIENE_MODIFICACIONES_PENDIENTES_Result> ObtenerModificacionePendientes(string ID_EMPLEADO = null, int? ID_CAMBIOS = null, string CL_ESTADO = null, string ID_PUESTO="")
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_MODIFICACIONES_PENDIENTES(ID_EMPLEADO, ID_CAMBIOS, CL_ESTADO, ID_PUESTO).ToList();
            }
        }

        public List<SPE_OBTIENE_COMENTARIOS_NOTIFICACIONES_Result> ObtenerComentariosNotificaciones(string  ID_EMPLEADO = null, int? IDNOTIFICACION_RRHH = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_COMENTARIOS_NOTIFICACIONES(ID_EMPLEADO, IDNOTIFICACION_RRHH).ToList();
            }
        }
        public List<SPE_OBTIENE_ARCHIVOS_PDE_Result > ObtenerArchivos(int? ID_ARCHIVO = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_ARCHIVOS_PDE(ID_ARCHIVO).ToList();
            }
        }
    }
}
