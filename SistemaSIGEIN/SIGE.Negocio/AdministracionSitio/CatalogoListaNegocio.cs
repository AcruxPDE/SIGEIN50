using System;
using System.Collections.Generic;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;


namespace SIGE.Negocio.Administracion 
{
    public class CatalogoListaNegocio
    {        
        public List<SPE_OBTIENE_C_CATALOGO_LISTA_Result> ObtieneCatalogoLista(int? pIdCatalogoLista = null, string pNbCatalogoLista = null, string pDsCatalogoLista = null, int? pIdCatalogoTipo = null, DateTime? pFeCreacion = null, DateTime? pFeModificacion = null, string pClUsuarioCrea = null, string pClUsuarioModifica = null, string pNbProgramaCrea = null, string pNbProgramaModifica = null)
        {
            CatalogoListaOperaciones operaciones = new CatalogoListaOperaciones();
            return operaciones.ObtenerCatalogoLista(pIdCatalogoLista, pNbCatalogoLista, pDsCatalogoLista, pIdCatalogoTipo, pFeCreacion, pFeModificacion, pClUsuarioCrea, pClUsuarioModifica, pNbProgramaCrea, pNbProgramaModifica);
        }
         
        public E_RESULTADO InsertaActualiza_C_CATALOGO_LISTA(string tipo_transaccion, SPE_OBTIENE_C_CATALOGO_LISTA_Result V_C_CATALOGO_LISTA, string usuario, string programa)
        {
            CatalogoListaOperaciones operaciones = new CatalogoListaOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualiza_C_CATALOGO_LISTA(tipo_transaccion, V_C_CATALOGO_LISTA, usuario, programa));
        }
 
        public E_RESULTADO Elimina_C_CATALOGO_LISTA(int? ID_CATALOGO_LISTA = null, string usuario = null, string programa = null)
        {
            CatalogoListaOperaciones operaciones = new CatalogoListaOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Elimina_C_CATALOGO_LISTA(ID_CATALOGO_LISTA, usuario, programa));
        }
    }
}