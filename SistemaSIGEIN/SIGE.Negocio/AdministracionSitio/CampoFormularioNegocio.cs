using SIGE.AccesoDatos.Implementaciones.Administracion;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Negocio.Administracion
{
    public class CampoFormularioNegocio
    {
        public List<SPE_OBTIENE_CAMPO_FORMULARIO_Result> ObtieneCamposFormularios(int? pIdCampoFormulario = null, string pClCampoFormulario = null, string pNbCampoFormulario = null, string pNbTooltip = null, string pClFormulario = null, bool? pFgActivo = null, bool? pFgSistema = null)
        {
            CampoFormularioOperaciones oCampoFormulario = new CampoFormularioOperaciones();
            return oCampoFormulario.ObtenerCamposFormularios(pIdCampoFormulario, pClCampoFormulario, pNbCampoFormulario, pNbTooltip, pClFormulario, pFgActivo, pFgSistema);
        }

        public E_RESULTADO InsertaActualizaCampoFormulario(string pClTipoTransaccion, SPE_OBTIENE_CAMPO_FORMULARIO_Result pCampoFormulario, string pClUsuario, string pNbPrograma)
        {
            CampoFormularioOperaciones oCampoFormulario = new CampoFormularioOperaciones();
            return UtilRespuesta.EnvioRespuesta(oCampoFormulario.InsertarActualizarCampoFormulario(pClTipoTransaccion, pCampoFormulario, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO EliminaCampoFormulario(int pIdCampoFormulario, string pClUsuario, string pNbPrograma)
        {
            CampoFormularioOperaciones oCampoFormulario = new CampoFormularioOperaciones();
            return UtilRespuesta.EnvioRespuesta(oCampoFormulario.EliminarCampoFormulario(pIdCampoFormulario, pClUsuario, pNbPrograma));
        }

        public List<SPE_OBTIENE_TIPO_CAMPO_FORMULARIO_Result> ObtieneTiposCampoFormulario()
        {
            CampoFormularioOperaciones oCampoFormulario = new CampoFormularioOperaciones();
            return oCampoFormulario.ObtenerTiposCampoFormulario();
        }
    }
}
