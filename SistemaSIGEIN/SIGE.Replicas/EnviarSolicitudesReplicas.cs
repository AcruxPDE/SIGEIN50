using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.Replicas
{
    class EnviarSolicitudesReplicas
    {
        static void Main(string[] args)
        {
            try
            {
                Replicas objReplicas = new Replicas();
                //ENVIAR CORREO DE SOLICITUDES DE PERIODOS CON FECHA DE ENVIO DEL DIA REPLICA YA CONFIGURADOS 
                var vResultado = objReplicas.enviarSolicitudesReplicas();
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
