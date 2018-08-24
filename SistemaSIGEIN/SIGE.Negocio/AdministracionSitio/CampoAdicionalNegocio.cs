using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades.Administracion;


namespace SIGE.Negocio.Administracion 
{
    public class CampoAdicionalNegocio
    {
        public List<SPE_OBTIENE_C_CAMPO_ADICIONAL_Result> ObtieneCamposAdicionales(int? pIdCampo = null, string pClCampo = null, string pNbCampo = null, string pDsCampo = null, bool? pFgRequerido = null, string pNoValorDefecto = null, string pClTipoDato = null, string pClDimension = null, string pClReferencia = null, string pClEsquemaReferencia = null, bool? pFgMostrar = null, int? pIdCatalogoLista = null, bool? pFgAdicional = null)
        {
            CampoAdicionalOperaciones operaciones = new CampoAdicionalOperaciones();
            return operaciones.ObtenerCamposAdicionales(pIdCampo, pClCampo, pNbCampo, pDsCampo, pFgRequerido, pNoValorDefecto, pClTipoDato, pClDimension, pClReferencia, pClEsquemaReferencia, pFgMostrar, pIdCatalogoLista, pFgRequerido);
        }

        public List<SPE_OBTIENE_C_CAMPO_ADICIONAL_XML_Result> ObtenerCamposAdicionalesXml(String XML = null, int? ID_CAMPO = null, String CL_CAMPO = null, String NB_CAMPO = null, String DS_CAMPO = null, bool? FG_REQUERIDO = null, String NO_VALOR_DEFECTO = null, String CL_TIPO_DATO = null, String CL_DIMENSION = null, String CL_TABLA_REFERENCIA = null, String CL_ESQUEMA_REFERENCIA = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null, bool? FG_MOSTRAR = null, int? ID_CATALOGO_LISTA = null, bool? FG_ADICIONAL = null)
        {
            CampoAdicionalOperaciones operaciones = new CampoAdicionalOperaciones();
            return operaciones.Obtener_C_CAMPO_ADICIONAL_XML(XML,ID_CAMPO, CL_CAMPO, NB_CAMPO, DS_CAMPO, FG_REQUERIDO, NO_VALOR_DEFECTO, CL_TIPO_DATO, CL_DIMENSION, CL_TABLA_REFERENCIA, CL_ESQUEMA_REFERENCIA, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA, FG_MOSTRAR, ID_CATALOGO_LISTA, FG_REQUERIDO);
        }
        
        //public int InsertaActualiza_C_CAMPO_ADICIONAL(string tipo_transaccion, SPE_OBTIENE_C_CAMPO_ADICIONAL_Result V_C_CAMPO_ADICIONAL, string usuario, string programa)
        //{
        //    CampoAdicionalOperaciones operaciones = new CampoAdicionalOperaciones();
        //    return operaciones.InsertaActualiza_C_CAMPO_ADICIONAL(tipo_transaccion, V_C_CAMPO_ADICIONAL, usuario, programa);
        //}

        public int Elimina_C_CAMPO_ADICIONAL(int? ID_CAMPO = null, string usuario = null, string programa = null)
        {
            CampoAdicionalOperaciones operaciones = new CampoAdicionalOperaciones();
            return operaciones.Elimina_C_CAMPO_ADICIONAL(ID_CAMPO, usuario, programa);
        }

        public string obtieneCampoAdicionalXml(String CL_TABLA_REFERENCIA = null)
        {
            CampoAdicionalOperaciones op = new CampoAdicionalOperaciones();
            return op.obtieneCampoAdicionalXml(CL_TABLA_REFERENCIA);
        }
    }
}
