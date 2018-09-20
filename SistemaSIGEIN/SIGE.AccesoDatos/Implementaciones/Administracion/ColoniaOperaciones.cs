using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades.Administracion;
using SIGE.Entidades;
using System.Data.Objects;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal // reemplazar por la carpeta correspondiente
{

    public class ColoniaOperaciones
    {

        private SistemaSigeinEntities context;


        public List<SPE_OBTIENE_C_COLONIA_Result> ObtenerColonias(int? pIdColonia = null, String pClPais = null, String pClEstado = null, String pNbEstado = null, String pClMunicipio = null, String pNbMunicipio = null, String pClColonia = null, String pNbColonia = null, String pClTipoAsentamiento = null, String pClCodigoPostal = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_C_COLONIA(pIdColonia, pClPais, pClEstado,pNbEstado,  pClMunicipio,pNbMunicipio, pClColonia, pNbColonia, pClTipoAsentamiento, pClCodigoPostal).ToList();
            }
        }
                
        public List<SPE_OBTENER_TIPO_ASENTAMIENTO_Result> Obtener_TIPO_ASENTAMIENTO()
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTENER_TIPO_ASENTAMIENTO().ToList();
            }
        }
                
        public XElement InsertaActualiza_C_COLONIA(string tipo_transaccion, SPE_OBTIENE_C_COLONIA_Result V_C_COLONIA, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                pout_clave_retorno.Value = "";

                context.SPE_INSERTA_ACTUALIZA_C_COLONIA(pout_clave_retorno, V_C_COLONIA.ID_COLONIA, V_C_COLONIA.CL_PAIS, V_C_COLONIA.CL_ESTADO, V_C_COLONIA.CL_MUNICIPIO, V_C_COLONIA.CL_COLONIA, V_C_COLONIA.NB_COLONIA, V_C_COLONIA.CL_TIPO_ASENTAMIENTO, V_C_COLONIA.CL_CODIGO_POSTAL, usuario, usuario, programa, programa, tipo_transaccion);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        
        public XElement Elimina_C_COLONIA(int? ID_COLONIA = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                pout_clave_retorno.Value = "";

                context.SPE_ELIMINA_C_COLONIA(pout_clave_retorno, ID_COLONIA, usuario, programa);
                //regresamos el valor de retorno de sql				
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
                
        public List<SPE_OBTIENE_DATOS_CP_Result> ObtenerDatosColonia(string CL_CODIGO_POSTAL = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_C_DATOS in context.SPE_OBTIENE_DATOS_CP(CL_CODIGO_POSTAL)
                        select V_C_DATOS;
                return q.ToList();
            }
        }
               
        public List<SPE_OBTIENE_C_COLONIA_CP_Result> Obtener_C_COLONIA_CP(String ID_COLONIA = null, String CL_PAIS = null, String CL_ESTADO = null, String CL_MUNICIPIO = null, String CL_COLONIA = null, String NB_COLONIA = null, String CL_TIPO_ASENTAMIENTO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null, String CL_CODIGO_POSTAL = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_C_COLONIA in context.SPE_OBTIENE_C_COLONIA_CP(ID_COLONIA, CL_PAIS, CL_ESTADO, CL_MUNICIPIO, CL_COLONIA, NB_COLONIA, CL_TIPO_ASENTAMIENTO, CL_CODIGO_POSTAL)
                        select V_C_COLONIA;
                return q.ToList();
            }
        }

        public List<SPE_OBTIENE_C_CODIGO_POSTAL_Result> ObtenerCodigoPostal(String CL_CODIGO_POSTAL = null, String NB_COLONIA = null, String NB_MUNICIPIO = null, String NB_ESTADO = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_C_CODIGO_POSTAL(CL_CODIGO_POSTAL, NB_COLONIA, NB_MUNICIPIO, NB_ESTADO).ToList();
            }
        }

       
    }
}
