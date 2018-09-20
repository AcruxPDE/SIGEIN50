using SIGE.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.Administracion
{
    public class GruposOperaciones
    {
        private SistemaSigeinEntities context;

        public XElement InsertaActualizaGrupo(int? pID_GRUPO = null, string pCL_GRUPO = null, string pNB_GRUPO = null, string pXML_PLAZAS = null, string pCL_USUARIO = null, string pNB_PROGRAMA = null, string pCL_TIPO_TRANSACCION = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_GRUPO(pOutClRetorno, pID_GRUPO, pCL_GRUPO, pNB_GRUPO, pXML_PLAZAS, pCL_USUARIO, pNB_PROGRAMA, pCL_TIPO_TRANSACCION);

                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_GRUPOS_Result> ObtieneGrupos(int? pID_GRUPO = null, string pCL_GRUPO = null, bool? pFG_ACTIVO = null, int? pID_PLAZA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_GRUPOS(pID_GRUPO, pCL_GRUPO, pFG_ACTIVO, pID_PLAZA).ToList();
            }
        }

        public XElement EliminaGrupo(int? pID_GRUPO = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_GRUPO(pOutClRetorno, pID_GRUPO);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

    }
}
