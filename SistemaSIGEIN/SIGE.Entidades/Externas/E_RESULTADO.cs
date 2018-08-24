using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.Entidades.Externas
{
    //ENTIDAD RESULTADO
    public partial class E_RESULTADO
    {
        public string NO_AFECTADOS { get; set; }
        public E_TIPO_RESPUESTA_DB CL_TIPO_ERROR { get; set; }
        public List<E_MENSAJE> MENSAJE { get; set; }
        public XElement XML_MENSAJE_ORIGINAL { get; set; }

        public E_RESULTADO()
        {
        }

        public E_RESULTADO(XElement pXmlResultado)
        {
            this.NO_AFECTADOS = pXmlResultado.Attribute("NO_AFECTADOS").Value;
            this.CL_TIPO_ERROR = ObtenerClTipoRespuesta(pXmlResultado);
            this.MENSAJE = pXmlResultado.Element("MENSAJES").Elements("MENSAJE").Select(el => new E_MENSAJE
                {
                    CL_IDIOMA = el.Attribute("CL_IDIOMA").Value,
                    DS_MENSAJE = el.Attribute("DS_MENSAJE").Value
                }).ToList();
            this.XML_MENSAJE_ORIGINAL = pXmlResultado;
        }

        public static XElement ObtieneDatosRespuesta(XElement pRespuesta)
        {
            return pRespuesta.Element("DATOS");
        }

        public static XElement ObtieneDatosRespuesta(E_RESULTADO pResultado)
        {
            return pResultado.ObtieneDatosRespuesta();
        }

        public XElement ObtieneDatosRespuesta()
        {
            return ObtieneDatosRespuesta(this.XML_MENSAJE_ORIGINAL);
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
    }

    public partial class E_MENSAJE
    {
        public string CL_IDIOMA { get; set; }
        public string DS_MENSAJE { get; set; }
    }
}
