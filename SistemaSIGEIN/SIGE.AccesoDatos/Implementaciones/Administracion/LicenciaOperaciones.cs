using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using System.Data.Objects;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{
   
    public class LicenciaOperaciones
    {
		private SistemaSigeinEntities context;

        public SPE_OBTIENE_CONFIGURACION_LICENCIA_Result obtieneConfiguracion(string CL_CONFIGURACION = null, string CL_USUARIO = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_CONFIGURACION_LICENCIA(CL_CONFIGURACION, CL_USUARIO).FirstOrDefault();
            }
        }

        public List<SPE_OBTIENE_EMPLEADOS_Result> ObtenerEmpleados(XElement pXmlSeleccion = null, bool? pFgFoto = false, string pClUsuario = null, bool? pFgActivo = null, int? pIdEmpresa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                string vXmlFiltro = null;
                if (pXmlSeleccion != null)
                    vXmlFiltro = pXmlSeleccion.ToString();
                return context.SPE_OBTIENE_EMPLEADOS(vXmlFiltro, pClUsuario, pFgActivo, pFgFoto, pIdEmpresa).ToList();
            }
        }

        public XElement InsertaActualiza_S_CONFIGURACION(string CL_CONFIGURACION = null, string NO_CONFIGURACION = null, string CL_USUARIO = null, string NB_PROGRAMA = null, string TIPO_TRANSACCION = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(int));
                context.SPE_INSERTA_ACTUALIZA_S_CONFIGURACION_LICENCIA(pout_clave_retorno, CL_CONFIGURACION, NO_CONFIGURACION, CL_USUARIO, NB_PROGRAMA, TIPO_TRANSACCION);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
	}
}