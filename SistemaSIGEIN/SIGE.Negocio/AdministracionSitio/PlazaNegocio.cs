using SIGE.AccesoDatos.Implementaciones.Administracion;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.Negocio.AdministracionSitio
{
    public class PlazaNegocio
    {
        public List<SPE_OBTIENE_PLAZAS_Result> ObtienePlazas(int? pIdPlaza = null, XElement pXmlSeleccion = null, int? pID_EMPRESA = null, int? pID_ROL = null)
        {
            PlazaOperaciones oPlaza = new PlazaOperaciones();
            return oPlaza.ObtenerPlazas(pIdPlaza, pXmlSeleccion, pID_EMPRESA, pID_ROL);
        }

        public E_RESULTADO InsertaActualizaPlaza(E_TIPO_OPERACION_DB pClTipoOperacion, SPE_OBTIENE_PLAZAS_Result pPlaza, string pClUsuario, string pNbPrograma)
        {
            PlazaOperaciones oPlaza = new PlazaOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPlaza.InsertarActualizarPlaza(pClTipoOperacion, pPlaza, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO EliminaPlaza(int pIdPlaza, string pClUsuario, string pNbPrograma)
        {
            PlazaOperaciones oPlaza = new PlazaOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPlaza.EliminarPlaza(pIdPlaza, pClUsuario, pNbPrograma));
        }
    }
}
