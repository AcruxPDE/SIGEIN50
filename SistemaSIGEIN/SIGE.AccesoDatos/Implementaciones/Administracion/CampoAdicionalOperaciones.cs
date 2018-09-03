using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades.Administracion;
//using SIGE.Entidades.EvaluacionOrganizacional;
//using SIGE.Entidades.FormacionDesarrollo;
//using SIGE.Entidades.MetodologiaCompensacion;
using SIGE.Entidades;
using System.Data.Objects;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{

    public class CampoAdicionalOperaciones
    {
        private SistemaSigeinEntities context;
     
        public List<SPE_OBTIENE_C_CAMPO_ADICIONAL_Result> ObtenerCamposAdicionales(int? pIdCampo = null, string pClCampo = null, string pNbCampo = null, string pDsCampo = null, bool? pFgRequerido = null, string pNoValorDefecto = null, string pClTipoDato = null, string pClDimension = null, string pClTablaReferencia = null, string pClEsquemaReferencia = null, bool? pFgMostrar = null, int? pIdCatalogoLista = null, bool? pFgAdicional = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_C_CAMPO_ADICIONAL(pIdCampo, pClCampo, pNbCampo, pDsCampo, pFgRequerido, pNoValorDefecto, pClTipoDato, pClDimension, pClTablaReferencia, pClEsquemaReferencia, pFgMostrar, pIdCatalogoLista, pFgAdicional).ToList();
            }
        }

        public string obtieneCampoAdicionalXml(String CL_TABLA_REFERENCIA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_CAMPO_ADICIONAL_XML(CL_TABLA_REFERENCIA).FirstOrDefault().ToString();
            }
        }
             
        public List<SPE_OBTIENE_C_CAMPO_ADICIONAL_XML_Result> Obtener_C_CAMPO_ADICIONAL_XML(String XML=null,int? ID_CAMPO = null, String CL_CAMPO = null, String NB_CAMPO = null, String DS_CAMPO = null, bool? FG_REQUERIDO = null, String NO_VALOR_DEFECTO = null, String CL_TIPO_DATO = null, String CL_DIMENSION = null, String CL_TABLA_REFERENCIA = null, String CL_ESQUEMA_REFERENCIA = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null, bool? FG_MOSTRAR = null, int? ID_CATALOGO_LISTA = null, bool? FG_ADICIONAL = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_C_CAMPO_ADICIONAL in context.SPE_OBTIENE_C_CAMPO_ADICIONAL_XML(XML,ID_CAMPO, CL_CAMPO, NB_CAMPO, DS_CAMPO, FG_REQUERIDO, NO_VALOR_DEFECTO, CL_TIPO_DATO, CL_DIMENSION, CL_TABLA_REFERENCIA, CL_ESQUEMA_REFERENCIA, FG_MOSTRAR, ID_CATALOGO_LISTA, FG_ADICIONAL)
                        select V_C_CAMPO_ADICIONAL;
                return q.ToList();
            }
        }     
        
        //public int InsertaActualiza_C_CAMPO_ADICIONAL(string tipo_transaccion, SPE_OBTIENE_C_CAMPO_ADICIONAL_Result V_C_CAMPO_ADICIONAL, string usuario, string programa)
        //{
        //    using (context = new SistemaSigeinEntities())
        //    {
        //        //Declaramos el objeto de valor de retorno
        //        ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
        //        context.SPE_INSERTA_ACTUALIZA_C_CAMPO_ADICIONAL(pout_clave_retorno, V_C_CAMPO_ADICIONAL.ID_CAMPO, V_C_CAMPO_ADICIONAL.CL_CAMPO, V_C_CAMPO_ADICIONAL.NB_CAMPO, V_C_CAMPO_ADICIONAL.DS_CAMPO, V_C_CAMPO_ADICIONAL.FG_REQUERIDO, V_C_CAMPO_ADICIONAL.NO_VALOR_DEFECTO, V_C_CAMPO_ADICIONAL.CL_TIPO_DATO, V_C_CAMPO_ADICIONAL.CL_DIMENSION, V_C_CAMPO_ADICIONAL.CL_TABLA_REFERENCIA, V_C_CAMPO_ADICIONAL.CL_ESQUEMA_REFERENCIA, usuario, usuario, programa, programa, V_C_CAMPO_ADICIONAL.FG_MOSTRAR, V_C_CAMPO_ADICIONAL.ID_CATALOGO_LIST,V_C_CAMPO_ADICIONAL.FG_ADICIONAL,tipo_transaccion);
        //        //regresamos el valor de retorno de sql
        //        return Convert.ToInt32(pout_clave_retorno.Value); ;
        //    }
        //}
                
        public int Elimina_C_CAMPO_ADICIONAL(int? ID_CAMPO = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_ELIMINA_C_CAMPO_ADICIONAL(pout_clave_retorno, ID_CAMPO, usuario, programa);
                //regresamos el valor de retorno de sql				
                return Convert.ToInt32(pout_clave_retorno.Value);
            }
        }
        

    }
}
