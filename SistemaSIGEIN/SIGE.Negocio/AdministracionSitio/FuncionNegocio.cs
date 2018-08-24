using System.Collections.Generic;
using System.Linq;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;

namespace SIGE.Negocio.Administracion
{
    public class FuncionNegocio
    {
        //public List<SPE_OBTIENE_S_FUNCION_Result> Obtener_S_FUNCION(int? ID_FUNCION = null, String CL_FUNCION = null, String CL_TIPO_FUNCION = null, String NB_FUNCION = null, int? ID_FUNCION_PADRE = null, String NB_URL = null, String XML_CONFIGURACION = null)
        //{
        //    FuncionOperaciones operaciones = new FuncionOperaciones();
        //    return operaciones.Obtener_S_FUNCION(ID_FUNCION, CL_FUNCION, CL_TIPO_FUNCION, NB_FUNCION, ID_FUNCION_PADRE, NB_URL, XML_CONFIGURACION);
        //}

        public List<E_FUNCION> ObtieneFunciones(E_TIPO_FUNCION pClTipoFuncion)
        {
            FuncionOperaciones oFunciones = new FuncionOperaciones();
            List<E_FUNCION> lstFunciones = oFunciones.ObtenerFunciones(pClTipoFuncion).Select(s => new E_FUNCION
            {
                CL_FUNCION = s.CL_FUNCION,
                CL_TIPO_FUNCION = s.CL_TIPO_FUNCION,
                ID_FUNCION = s.ID_FUNCION,
                ID_FUNCION_PADRE = s.ID_FUNCION_PADRE,
                NB_FUNCION = s.NB_FUNCION,
                NB_URL = s.NB_URL
            }).ToList();
            return lstFunciones;
        }

        //public int InsertaActualiza_S_FUNCION(string tipo_transaccion, SPE_OBTIENE_S_FUNCION_Result V_S_FUNCION, string usuario, string programa)
        //{
        //    FuncionOperaciones operaciones = new FuncionOperaciones();
        //    return operaciones.InsertaActualiza_S_FUNCION(tipo_transaccion, V_S_FUNCION, usuario, programa);
        //}

        //public int Elimina_S_FUNCION(int? ID_FUNCION = null, string usuario = null, string programa = null)
        //{
        //    FuncionOperaciones operaciones = new FuncionOperaciones();
        //    return operaciones.Elimina_S_FUNCION(ID_FUNCION, usuario, programa);
        //}

    }
}
