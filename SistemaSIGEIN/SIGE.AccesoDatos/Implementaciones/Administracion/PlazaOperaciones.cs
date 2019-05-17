using SIGE.Entidades;
using SIGE.Entidades.Externas;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.Administracion
{
    public class PlazaOperaciones
    {
        SistemaSigeinEntities contexto;

        public List<SPE_OBTIENE_PLAZAS_Result> ObtenerPlazas(int? pIdPlaza, XElement pXmlSeleccion, int? pID_EMPRESA, int? pID_ROL = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                if (pXmlSeleccion == null)
                    pXmlSeleccion = new XElement("SELECCION", new XAttribute("CL_TIPO", "ACTIVAS_INACTIVAS"));
                return contexto.SPE_OBTIENE_PLAZAS(pIdPlaza, pXmlSeleccion.ToString(), pID_EMPRESA, pID_ROL).ToList();
            }
        }

        public XElement InsertarActualizarPlaza(E_TIPO_OPERACION_DB pClTipoOperacion, SPE_OBTIENE_PLAZAS_Result pPlaza, string pClUsuario, string pNbPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_ACTUALIZA_PLAZA(pOutClRetorno, pPlaza.ID_PLAZA, pPlaza.CL_PLAZA, pPlaza.NB_PLAZA, pPlaza.ID_EMPLEADO, pPlaza.ID_PUESTO,pPlaza.ID_DEPARTAMENTO, pPlaza.ID_PLAZA_SUPERIOR, pPlaza.ID_EMPRESA, pPlaza.FG_ACTIVO, pPlaza.XML_GRUPOS, pClUsuario, pNbPrograma, pClTipoOperacion.ToString());
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement EliminarPlaza(int pIdPlaza, string pClUsuario, string pNbPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ELIMINA_PLAZA(pOutClRetorno, pIdPlaza, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }
    }
}
