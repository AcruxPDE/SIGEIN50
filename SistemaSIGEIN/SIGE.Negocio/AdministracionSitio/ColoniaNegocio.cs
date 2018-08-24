using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades.Administracion;
using System.Xml.Linq;
using SIGE.Negocio.Utilerias;
using SIGE.Entidades.Externas;

namespace SIGE.Negocio.Administracion  // reemplazar por la carpeta correspondiente
{
    public class ColoniaNegocio
    {
        public List<SPE_OBTIENE_C_COLONIA_Result> ObtieneColonias(int? pIdColonia = null, String pClPais = null, String pClEstado = null, String pClMunicipio = null, String pClColonia = null, String pNbColonia = null, String pClTipoAsentamiento = null, String pClCodigoPostal = null)
        {
            ColoniaOperaciones operaciones = new ColoniaOperaciones();
            return operaciones.ObtenerColonias(pIdColonia, pClPais, pClEstado, pClMunicipio, pClColonia, pNbColonia, pClTipoAsentamiento, pClCodigoPostal);
        }
                
        public List<SPE_OBTENER_TIPO_ASENTAMIENTO_Result> Obtener_TIPO_ASENTAMIENTO()
        {
            ColoniaOperaciones operaciones = new ColoniaOperaciones();
            return operaciones.Obtener_TIPO_ASENTAMIENTO();
        }
        
        public XElement InsertaActualiza_C_COLONIA(string tipo_transaccion, SPE_OBTIENE_C_COLONIA_Result V_C_COLONIA, string usuario, string programa)
        {
            ColoniaOperaciones operaciones = new ColoniaOperaciones();
            return operaciones.InsertaActualiza_C_COLONIA(tipo_transaccion, V_C_COLONIA, usuario, programa);
        }
                
        public E_RESULTADO Elimina_C_COLONIA(int? ID_COLONIA = null, string usuario = null, string programa = null)
        {
            ColoniaOperaciones operaciones = new ColoniaOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Elimina_C_COLONIA(ID_COLONIA, usuario, programa));
        }
        
        public List<SPE_OBTIENE_DATOS_CP_Result> ObtenerDatosColoniaCp(string CL_CODIGO_POSTAL = null)
        {
            ColoniaOperaciones operaciones = new ColoniaOperaciones();
            return operaciones.ObtenerDatosColonia(CL_CODIGO_POSTAL);
        }

        public List<SPE_OBTIENE_C_COLONIA_CP_Result> Obtener_C_COLONIA_CP(String ID_COLONIA = null, String CL_PAIS = null, String CL_ESTADO = null, String CL_MUNICIPIO = null, String CL_COLONIA = null, String NB_COLONIA = null, String CL_TIPO_ASENTAMIENTO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null, String CL_CODIGO_POSTAL = null)
        {
            ColoniaOperaciones operaciones = new ColoniaOperaciones();
            return operaciones.Obtener_C_COLONIA_CP(ID_COLONIA, CL_PAIS, CL_ESTADO, CL_MUNICIPIO, CL_COLONIA, NB_COLONIA, CL_TIPO_ASENTAMIENTO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA, CL_CODIGO_POSTAL);
        }

        public List<SPE_OBTIENE_C_CODIGO_POSTAL_Result> ObtieneCodigoPostal(String CL_CODIGO_POSTAL = null, String NB_COLONIA = null, String NB_MUNICIPIO = null, String NB_ESTADO = null)
        {
            ColoniaOperaciones operaciones = new ColoniaOperaciones();
            return operaciones.ObtenerCodigoPostal(CL_CODIGO_POSTAL, NB_COLONIA, NB_MUNICIPIO, NB_ESTADO);
        }


    }
}