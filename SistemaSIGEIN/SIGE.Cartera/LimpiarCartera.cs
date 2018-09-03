using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.Cartera
{
    class LimpiarCartera
    {
        static void Main(string[] args)
        {
            try
            {
                //EJECUTAR PROCEDURE QUE DETERMINA SI SE ELIMINA LA CARTERA
                Solicitudes objSolicitud = new Solicitudes();
                //ENVIAR CORREO DE ELIMINACION Y ELIMINAN LAS SOLICITUDES
                var respuesta = objSolicitud.enviarEliminarSolicitudes();
            }
            catch (Exception e)
            {
                //Guardar el error en el log o tabla de errores
            }
        }        
    }
}
