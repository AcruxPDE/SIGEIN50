using SIGE.AccesoDatos.Implementaciones.Administracion;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Negocio.AdministracionSitio
{
    public class GruposNegocio
    {
        public E_RESULTADO InsertaActualizaGrupo(int? pID_GRUPO = null, string pCL_GRUPO = null, string pNB_GRUPO = null, string pXML_PLAZAS = null, string pCL_USUARIO = null, string pNB_PROGRAMA = null, string pCL_TIPO_TRANSACCION = null)
        {
            GruposOperaciones operaciones = new GruposOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualizaGrupo(pID_GRUPO,  pCL_GRUPO, pNB_GRUPO, pXML_PLAZAS, pCL_USUARIO, pNB_PROGRAMA, pCL_TIPO_TRANSACCION));
        }

        public List<SPE_OBTIENE_GRUPOS_Result> ObtieneGrupos(int? pID_GRUPO = null, string pCL_GRUPO = null, bool? pFG_ACTIVO = null, int? pID_PLAZA = null)
        {
            GruposOperaciones operaciones = new GruposOperaciones();
            return operaciones.ObtieneGrupos(pID_GRUPO, pCL_GRUPO, pFG_ACTIVO, pID_PLAZA);
        }

        public E_RESULTADO EliminaGrupo(int? pID_GRUPO = null)
        {
            GruposOperaciones operaciones = new GruposOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.EliminaGrupo(pID_GRUPO));
        }

    }
}
