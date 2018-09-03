using SIGE.Entidades.Externas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.Negocio.Utilerias
{
    public static class UtilRespuesta
    {
        public static E_RESULTADO EnvioRespuesta(XElement pRespuesta)
        {
            return new E_RESULTADO(pRespuesta);
            //return new E_RESULTADO
            //{
            //    NO_AFECTADOS = pRespuesta.Attribute("NO_AFECTADOS").Value,
            //    CL_TIPO_ERROR = ObtenerClTipoRespuesta(pRespuesta),
            //    MENSAJE = pRespuesta.Element("MENSAJES").Elements("MENSAJE").Select(el => new E_MENSAJE
            //        {
            //            CL_IDIOMA = el.Attribute("CL_IDIOMA").Value,
            //            DS_MENSAJE = el.Attribute("DS_MENSAJE").Value
            //        }).ToList()
            //};
        }

        public static XElement ObtieneDatosRespuesta(XElement pRespuesta)
        {
            return (ObtenerClTipoRespuesta(pRespuesta).Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL)) ? pRespuesta.Element("DATOS") : (XElement)null;
        }

        private static E_TIPO_RESPUESTA_DB ObtenerClTipoRespuesta(XElement pRespuesta)
        {
            switch (pRespuesta.Attribute("CL_TIPO_ERROR").Value)
            {
                case "ERROR":
                    return E_TIPO_RESPUESTA_DB.ERROR;
                case "WARNING":
                    return E_TIPO_RESPUESTA_DB.WARNING;
                case "SUCCESSFUL":
                    return E_TIPO_RESPUESTA_DB.SUCCESSFUL;
                default:
                    return E_TIPO_RESPUESTA_DB.SUCCESSFUL;
            }
        }

        public static List <E_PRUEBA_LABORAL_I> MensajesPruebaLaboralI(XElement pRespuesta)
        {
            List<E_PRUEBA_LABORAL_I> vListaMensajes = pRespuesta.Elements("MENSAJE").Select(el => new E_PRUEBA_LABORAL_I
                {
                    CL_MENSAJE = el.Attribute("CL_MENSAJE").Value,
                    NB_TITULO = el.Attribute("NB_TITULO").Value,
                    DS_CONCEPTO = el.Attribute("DS_MENSAJE").Value,
                    TIPO_MENSAJE = el.Attribute("TIPO_MENSAJE").Value,
                    SECCION = el.Attribute("SECCION").Value
                }).ToList();


            return vListaMensajes;
        }
    }
}
