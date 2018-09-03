using System;
using System.Collections.Generic;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;


namespace SIGE.Negocio.Administracion  // reemplazar por la carpeta correspondiente
{
    public class CatalogoTipoNegocio
    {

        public List<SPE_OBTIENE_S_CATALOGO_TIPO_Result> Obtener_S_CATALOGO_TIPO(int? ID_CATALOGO_TIPO = null, String NB_CATALOGO_TIPO = null, String DS_CATALOGO_TIPO = null)
        {
            CatalogoTipoOperaciones operaciones = new CatalogoTipoOperaciones();
            return operaciones.Obtener_S_CATALOGO_TIPO(ID_CATALOGO_TIPO, NB_CATALOGO_TIPO, DS_CATALOGO_TIPO);
        }

        public int InsertaActualiza_S_CATALOGO_TIPO(string tipo_transaccion, SPE_OBTIENE_S_CATALOGO_TIPO_Result V_S_CATALOGO_TIPO, string usuario, string programa)
        {
            CatalogoTipoOperaciones operaciones = new CatalogoTipoOperaciones();
            return operaciones.InsertaActualiza_S_CATALOGO_TIPO(tipo_transaccion, V_S_CATALOGO_TIPO, usuario, programa);
        }
          
        public int Elimina_S_CATALOGO_TIPO(int? ID_CATALOGO_TIPO = null, string usuario = null, string programa = null)
        {
            CatalogoTipoOperaciones operaciones = new CatalogoTipoOperaciones();
            return operaciones.Elimina_S_CATALOGO_TIPO(ID_CATALOGO_TIPO, usuario, programa);
        }
        
    }
}
