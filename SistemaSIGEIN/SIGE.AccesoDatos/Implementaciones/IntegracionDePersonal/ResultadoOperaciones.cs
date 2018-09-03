using SIGE.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal
{
    public class ResultadoOperaciones
    {
        private SistemaSigeinEntities context;

        public XElement insertaResultadosLaboral2(string pXmlResultados, int? pIdCuestionario,  int pIdPrueba, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));                
                context.SPE_INSERTA_RESULTADOS_LABORAL2(pOutClRetorno, pXmlResultados, pIdCuestionario, pIdPrueba, usuario, programa);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement insertaResultadosPensamiento(string pXmlResultados, int? pIdCuestionario, int pIdPrueba, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_RESULTADOS_PENSAMIENTO(pOutClRetorno, pXmlResultados, pIdCuestionario, pIdPrueba, usuario, programa);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement insertaResultadosTiva(string pXmlResultados, int? pIdCuestionario, int pIdPrueba, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_RESULTADOS_TIVA(pOutClRetorno, pXmlResultados, pIdCuestionario, pIdPrueba, usuario, programa);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement insertaResultadosInteres(string pXmlResultados, int? pIdCuestionario, int pIdPrueba, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_RESULTADOS_INTERES_PERSONAL(pOutClRetorno, pXmlResultados, pIdCuestionario, pIdPrueba, usuario, programa);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }


        public XElement insertaResultadosAdaptacionMedio(string pXmlResultados, int? pIdCuestionario, int pIdPrueba, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_RESULTADOS_ADAPTACION(pOutClRetorno, pXmlResultados, pIdCuestionario, pIdPrueba, usuario, programa);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }


        public XElement insertaResultadosLaboral1(string pXmlResultados, int? pIdCuestionario, int pIdPrueba, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_RESULTADOS_LABORAL1(pOutClRetorno, pXmlResultados, pIdCuestionario, pIdPrueba, usuario, programa);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }


        public XElement insertaResultadosTecnicaPc(string pXmlResultados, int? pIdCuestionario, int pIdPrueba, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_RESULTADOS_TECNICAPC(pOutClRetorno, pXmlResultados, pIdCuestionario, pIdPrueba, usuario, programa);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }
        public XElement insertaResultadosAptitud2(string pXmlResultados, int? pIdCuestionario, int pIdPrueba, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_RESULTADOS_APTITUD2(pOutClRetorno, pXmlResultados, pIdCuestionario, pIdPrueba, usuario, programa);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement insertaResultadosAptitud1(string pXmlResultados, int? pIdCuestionario, int pIdPrueba, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_RESULTADOS_APTITUD_1(pOutClRetorno, pXmlResultados, pIdCuestionario, pIdPrueba, usuario, programa);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }



        public XElement insertaResultadosIngles(string pXmlResultados, int? pIdCuestionario, int pIdPrueba,string pnbPrueba, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_RESULTADOS_INGLES(pOutClRetorno, pXmlResultados, pIdCuestionario, pIdPrueba, pnbPrueba, usuario, programa);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement insertaResultadosOrtografias(string pXmlResultados, int? pIdCuestionario, int pIdPrueba,string pnbPrueba, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_RESULTADOS_ORTOGRAFIAS(pOutClRetorno, pXmlResultados, pIdCuestionario, pIdPrueba,pnbPrueba, usuario, programa);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement insertaResultadosRedaccion(string pXmlResultados, int? pIdCuestionario, int pIdPrueba, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_RESULTADOS_REDACCION(pOutClRetorno, pXmlResultados, pIdCuestionario, pIdPrueba, usuario, programa);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement insertaResultadosEntrevista(string pXmlResultados, int? pIdCuestionario, int pIdPrueba, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_RESULTADOS_ENTREVISTA(pOutClRetorno, pXmlResultados, pIdCuestionario, pIdPrueba, usuario, programa);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }
    }
}
