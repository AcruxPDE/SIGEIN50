using SIGE.Entidades;
using SIGE.Entidades.Administracion;
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
    public class CentroOperativoOperaciones
    {
        private SistemaSigeinEntities context;

        #region OBTIENE DATOS C_CENTRO_OPTVO
        public List<SPE_OBTIENE_CENTROS_OPTVOS_Result> Obtener_C_CENTRO_OPTVO(Guid? ID_CENTRO_OPTVO = null, String CL_CLIENTE = null, String CL_CENTRO_OPTVO = null, String NB_CENTRO_OPTVO = null, String NB_CALLE = null, String NB_NO_EXTERIOR = null, String NB_NO_INTERIOR = null, String NB_COLONIA = null, String CL_ESTADO = null, String NB_ESTADO = null, String CL_MUNICIPIO = null, String NB_MUNICIPIO = null, String CL_CODIGO_POSTAL = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_CREA_APP = null, String CL_USUARIO_MODIFICA_APP = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from vCentroOptvo in context.SPE_OBTIENE_CENTROS_OPTVOS(ID_CENTRO_OPTVO, CL_CENTRO_OPTVO, NB_CENTRO_OPTVO, NB_MUNICIPIO, CL_ESTADO,CL_MUNICIPIO, CL_CODIGO_POSTAL)
                        select vCentroOptvo;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_CENTRO_OPTVO
        public XElement InsertarActualizarCCentroOptvo(String pClTipoOperacion, E_CENTROS_OPTVOS vCCentroOptvo, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_CENTRO_OPTVO(poutClaveRetorno, vCCentroOptvo.ID_CENTRO_OPTVO, vCCentroOptvo.CL_CLIENTE, vCCentroOptvo.CL_CENTRO_OPTVO, vCCentroOptvo.NB_CENTRO_OPTVO, vCCentroOptvo.NB_CALLE, vCCentroOptvo.NB_NO_EXTERIOR, vCCentroOptvo.NB_NO_INTERIOR, vCCentroOptvo.NB_COLONIA, vCCentroOptvo.CL_ESTADO, vCCentroOptvo.NB_ESTADO, vCCentroOptvo.CL_MUNICIPIO, vCCentroOptvo.NB_MUNICIPIO, vCCentroOptvo.CL_CODIGO_POSTAL, usuario, usuario, programa, programa, pClTipoOperacion.ToString());
                return XElement.Parse(poutClaveRetorno.Value.ToString());
            }
        }
        #endregion

        #region ELIMINA DATOS  C_CENTRO_OPTVO
        public XElement EliminarCentroOptvo(Guid pIdCentroOptvo, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_CENTRO_OPTVO(poutClaveRetorno, pIdCentroOptvo, usuario, programa);
                return XElement.Parse(poutClaveRetorno.Value.ToString());
            }
        }
        #endregion

    }
}
