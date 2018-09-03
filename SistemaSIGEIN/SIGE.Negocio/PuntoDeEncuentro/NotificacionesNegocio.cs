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
   public  class NotificacionNegocio
    {
       public E_RESULTADO INSERTA_ACTUALIZA_NOTIFICACION_EMPLEADO(int ID_NOTIFICACION, string NB_ASUNTO, string DS_NOTIFICACION,
            string ID_EMPLEADO, string CL_ESTADO, string CL_USUARIO_APP, string NB_PROGRAMA, string DS_COMENTARIO, string NB_ARCHIVO, byte[] FI_ARCHIVO, string tipo_transaccion)
       {
           NotificacionesOperaciones operaciones = new NotificacionesOperaciones();
           return UtilRespuesta.EnvioRespuesta(operaciones.INSERTA_ACTUALIZA_NOTIFICACION_EMPLEADO( ID_NOTIFICACION,  NB_ASUNTO ,  DS_NOTIFICACION,
              ID_EMPLEADO ,   CL_ESTADO ,  CL_USUARIO_APP , NB_PROGRAMA,  DS_COMENTARIO, NB_ARCHIVO , FI_ARCHIVO,  tipo_transaccion));
       }
    }
}
