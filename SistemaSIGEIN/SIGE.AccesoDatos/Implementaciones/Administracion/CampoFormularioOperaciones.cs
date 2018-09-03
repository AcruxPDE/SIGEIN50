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
    public class CampoFormularioOperaciones
    {
        private SistemaSigeinEntities contexto;

        public List<SPE_OBTIENE_CAMPO_FORMULARIO_Result> ObtenerCamposFormularios(int? pIdCampoFormulario = null, string pClCampoFormulario = null, string pNbCampoFormulario = null, string pNbTooltip = null, string pClFormulario = null, bool? pFgActivo = null, bool? pFgSistema = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_CAMPO_FORMULARIO(pIdCampoFormulario, pClCampoFormulario, pNbCampoFormulario, pNbTooltip, pClFormulario, pFgActivo, pFgSistema).ToList();
            }
        }

        public XElement InsertarActualizarCampoFormulario(string pClTipoTransaccion, SPE_OBTIENE_CAMPO_FORMULARIO_Result pCampoFormulario, string pClUsuario, string pNbPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_ACTUALIZA_CAMPO_FORMULARIO(pOutClRetorno, pCampoFormulario.ID_CAMPO_FORMULARIO, pCampoFormulario.CL_CAMPO_FORMULARIO, pCampoFormulario.NB_CAMPO_FORMULARIO, pCampoFormulario.NB_TOOLTIP, pCampoFormulario.CL_TIPO_CAMPO, pCampoFormulario.CL_FORMULARIO, pCampoFormulario.FG_ACTIVO, pCampoFormulario.FG_SISTEMA, pCampoFormulario.XML_CAMPO_FORMULARIO, pClUsuario, pNbPrograma, pClTipoTransaccion);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement EliminarCampoFormulario(int pIdCampoFormulario, string pClCampoFormulario, string pNbCampoFormulario)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ELIMINA_CAMPO_FORMULARIO(pOutClRetorno, pIdCampoFormulario, pClCampoFormulario, pNbCampoFormulario);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_TIPO_CAMPO_FORMULARIO_Result> ObtenerTiposCampoFormulario()
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_TIPO_CAMPO_FORMULARIO().ToList();
            }
        }
    }
}
