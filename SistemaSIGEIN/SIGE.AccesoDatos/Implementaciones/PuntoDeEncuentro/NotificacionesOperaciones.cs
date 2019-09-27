using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades.Externas;
using System.IO;
using System.Web;
using System.Configuration;
using SIGE.Entidades;
using SIGE.Entidades.PuntoDeEncuentro;
using System.Xml.Linq;
using System.Data.Objects;
namespace SIGE.AccesoDatos.Implementaciones.PuntoDeEncuentro
{
    public class NotificacionesOperaciones
    {
        private SistemaSigeinEntities context;

        public XElement INSERTA_ACTUALIZA_NOTIFICACION_EMPLEADO(int ID_NOTIFICACION, string NB_ASUNTO , string DS_NOTIFICACION,
            string  ID_EMPLEADO , string  CL_ESTADO , string CL_USUARIO_APP ,string NB_PROGRAMA, string DS_COMENTARIO,string NB_ARCHIVO , byte[] FI_ARCHIVO, string tipo_transaccion)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_NOTIFICACION_EMPLEADO(pout_clave_retorno, ID_NOTIFICACION,  NB_ASUNTO ,  DS_NOTIFICACION,
             ID_EMPLEADO ,   CL_ESTADO ,  CL_USUARIO_APP , NB_PROGRAMA,  DS_COMENTARIO, NB_ARCHIVO ,  FI_ARCHIVO,  tipo_transaccion);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
    

    }
}
