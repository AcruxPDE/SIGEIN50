using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;


namespace SIGE.Negocio.Administracion  // reemplazar por la carpeta correspondiente
{
    public class CatalogoValorNegocio
    {

        #region OBTIENE DATOS  C_CATALOGO_VALOR
        public List<SPE_OBTIENE_C_CATALOGO_VALOR_Result> Obtener_C_CATALOGO_VALOR(int? ID_CATALOGO_VALOR = null, String CL_CATALOGO_VALOR = null, String NB_CATALOGO_VALOR = null, String DS_CATALOGO_VALOR = null, int? ID_CATALOGO_LISTA = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            CatalogoValorOperaciones operaciones = new CatalogoValorOperaciones();
            return operaciones.Obtener_C_CATALOGO_VALOR(ID_CATALOGO_VALOR, CL_CATALOGO_VALOR, NB_CATALOGO_VALOR, DS_CATALOGO_VALOR, ID_CATALOGO_LISTA, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_CATALOGO_VALOR
        public E_RESULTADO InsertaActualiza_C_CATALOGO_VALOR(string tipo_transaccion, SPE_OBTIENE_C_CATALOGO_VALOR_Result V_C_CATALOGO_VALOR, string usuario, string programa)
        {
            CatalogoValorOperaciones operaciones = new CatalogoValorOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualiza_C_CATALOGO_VALOR(tipo_transaccion, V_C_CATALOGO_VALOR, usuario, programa));
        }
        #endregion

        #region ELIMINA DATOS  C_CATALOGO_VALOR
        public E_RESULTADO Elimina_C_CATALOGO_VALOR(int? ID_CATALOGO_VALOR = null, string usuario = null, string programa = null)
        {
            CatalogoValorOperaciones operaciones = new CatalogoValorOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Elimina_C_CATALOGO_VALOR(ID_CATALOGO_VALOR, usuario, programa));
        }
        #endregion
    }
}