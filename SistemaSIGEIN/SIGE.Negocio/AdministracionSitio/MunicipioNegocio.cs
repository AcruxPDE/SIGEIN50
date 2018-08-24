using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal; // reemplazar por la carpeta correspondiente
using SIGE.Entidades.Administracion;

namespace SIGE.Negocio.Administracion  // reemplazar por la carpeta correspondiente
{
    public class MunicipioNegocio
    {
        
        public List<SPE_OBTIENE_C_MUNICIPIO_Result> ObtieneMunicipios(int? pIdMunicipio = null, String pClPais = null, String pClEstado = null, String pClMunicipio = null, String pNbMunicipio = null)
        {
            MunicipioOperaciones operaciones = new MunicipioOperaciones();
            return operaciones.ObtenerMunicipios(pIdMunicipio, pClPais, pClEstado, pClMunicipio, pNbMunicipio);
        }
               
        public int InsertaActualiza_C_MUNICIPIO(string tipo_transaccion, SPE_OBTIENE_C_MUNICIPIO_Result V_C_MUNICIPIO, string usuario, string programa)
        {
            MunicipioOperaciones operaciones = new MunicipioOperaciones();
            return operaciones.InsertaActualiza_C_MUNICIPIO(tipo_transaccion, V_C_MUNICIPIO, usuario, programa);
        }
              
        public int Elimina_C_MUNICIPIO(int? ID_MUNICIPIO = null, string usuario = null, string programa = null)
        {
            MunicipioOperaciones operaciones = new MunicipioOperaciones();
            return operaciones.Elimina_C_MUNICIPIO(ID_MUNICIPIO, usuario, programa);
        }
       
    }
}
