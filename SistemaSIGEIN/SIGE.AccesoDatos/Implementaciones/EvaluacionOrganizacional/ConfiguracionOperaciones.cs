using SIGE.Entidades;
using SIGE.Entidades.EvaluacionOrganizacional;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.EvaluacionOrganizacional
{

    public class ConfiguracionOperaciones
    {
        private SistemaSigeinEntities context;

        public List<SPE_OBTIENE_EMPLEADOS_Result> ObtieneEmpleados(XElement pXmlSeleccion = null, bool? pFgFoto = null, string pCL_USUARIO = null, bool? pFgActivo = null, int? pIdEmpresa = null, int? pIdRol = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                string vXmlFiltro = null;
                if (pXmlSeleccion != null)
                    vXmlFiltro = pXmlSeleccion.ToString();
                return context.SPE_OBTIENE_EMPLEADOS(vXmlFiltro, pCL_USUARIO, pFgActivo, pFgFoto, pIdEmpresa, pIdRol).ToList();
            }
        }

        public List<SPE_OBTIENE_CONFIGURACION_EVALUACION_ORGANIZACIONAL_Result> ObtenerConfiguracionEvaluacionOrganizacional(string pClTipoNotificacion)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_CONFIGURACION_EVALUACION_ORGANIZACIONAL(pClTipoNotificacion).ToList();
            }
        }

        public XElement InsertarConfiguracionNotificado(string pXmlEmpleados, string pClTipoNotificacion, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_CONFIGURACION_NOTIFICADO(pOutClRetorno, pXmlEmpleados, pClTipoNotificacion, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement EliminarConfiguracionNotificado(string pXmlEmpleados)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetrno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_CONFIGURACION_NOTIFICADO(pOutClRetrno, pXmlEmpleados);
                return XElement.Parse(pOutClRetrno.Value.ToString());
            }
        }

    }
}
