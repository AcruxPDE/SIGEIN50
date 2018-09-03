using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.PuntoDeEncuentro;
using SIGE.Entidades.Administracion;
using System.Xml.Linq;
using SIGE.Negocio.Utilerias;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;

namespace SIGE.Negocio.PuntoDeEncuentro
{
    public class MenuNegocio
    {
        public List<SPE_OBTIENE_K_COMUNICADO_EMPLEADO_Result> ObtenerComunicadoEmpleado(string ID_EMPLEADO = null, bool? FG_LEIDO = null)
        {
            MenuOperaciones operaciones = new MenuOperaciones();
            return operaciones.ObtenerComunicadoEmpleado(ID_EMPLEADO, FG_LEIDO).ToList();
        }
        public E_RESULTADO ActualizarComunicadoNoLeido(int ID_COMUNICADO, string  ID_EMPLEADO, string pClUsuario, string pNbPrograma)
        {
            MenuOperaciones operaciones = new MenuOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.ActualizarComunicadoNoLeido(ID_COMUNICADO, ID_EMPLEADO, pClUsuario, pNbPrograma));

        }
        public List<SPE_OBTIENE_MODIFICACIONES_INFORMACION_EMPLEADO_Result> ObtenerComunicadoModificacionesEmpleado(string ID_EMPLEADO = null)
        {
            MenuOperaciones operaciones = new MenuOperaciones();
            return operaciones.ObtenerComunicadoModificacionesEmpleado(ID_EMPLEADO).ToList();
        }
        public List<SPE_OBTIENE_NOTIFICACIONES_A_RRHH_Result> ObtenerNotificacionesRRHH(string  ID_EMPLEADO = null, string CL_ESTADO = "")
        {
            MenuOperaciones operaciones = new MenuOperaciones();
            return operaciones.ObtenerNotificacionesRRHH(ID_EMPLEADO, CL_ESTADO).ToList();
        }
        public List<SPE_OBTIENE_NOTIFICACIONES_PENDIENTES_Result> ObtenerNotificacionesPendientes(string ID_EMPLEADO = null, int? IDNOTIFICACION_RRHH = null, string CL_ESTADO="")
        {
            MenuOperaciones operaciones = new MenuOperaciones();
            return operaciones.ObtenerNotificacionesPendientes(ID_EMPLEADO, IDNOTIFICACION_RRHH, CL_ESTADO).ToList();
        }
        public List<SPE_OBTIENE_MODIFICACIONES_PENDIENTES_Result> ObtenerModificacionePendientes(string ID_EMPLEADO = null, int? ID_CAMBIOS = null, string CL_ESTADO = null,string ID_PUESTO="")
        {
            MenuOperaciones operaciones = new MenuOperaciones();
            return operaciones.ObtenerModificacionePendientes(ID_EMPLEADO, ID_CAMBIOS, CL_ESTADO, ID_PUESTO).ToList();
        }

        public List<SPE_OBTIENE_COMENTARIOS_NOTIFICACIONES_Result> ObtenerComentariosNotificaciones(string  ID_EMPLEADO = null, int? IDNOTIFICACION_RRHH = null)
        {
            MenuOperaciones operaciones = new MenuOperaciones();
            return operaciones.ObtenerComentariosNotificaciones(ID_EMPLEADO, IDNOTIFICACION_RRHH).ToList();
        }
        public List<SPE_OBTIENE_ARCHIVOS_PDE_Result> ObtenerArchivos(int? ID_ARCHIVO = null)
        {
            MenuOperaciones operaciones = new MenuOperaciones();
            return operaciones.ObtenerArchivos(ID_ARCHIVO).ToList();
            
        }
    }
}
