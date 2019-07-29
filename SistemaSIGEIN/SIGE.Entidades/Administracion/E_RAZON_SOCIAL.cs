using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_RAZON_SOCIAL
    {
        public System.Guid ID_RAZON_SOCIAL { get; set; }
        public string CL_CLIENTE { get; set; }
        public string CL_RAZON_SOCIAL { get; set; }
        public string NB_RAZON_SOCIAL { get; set; }
        public string CL_RFC { get; set; }
        public bool FG_ACTIVO { get; set; }
        public string NB_CALLE { get; set; }
        public string NB_NO_EXTERIOR { get; set; }
        public string NB_NO_INTERIOR { get; set; }
        public string NB_COLONIA { get; set; }
        public string NB_MUNICIPIO { get; set; }
        public string DS_ACTIVIDAD_ECONOMICA { get; set; }
        public string CL_ESTADO { get; set; }
        public string CL_CODIGO_POSTAL { get; set; }
        public string NB_REPRESENTANTE_LEGAL { get; set; }
        public bool FG_INSCRITO_IMSS { get; set; }
        public string DS_LEYENDA_RECIBO { get; set; }
        public bool FG_PERSONA_FISICA { get; set; }
        public string DS_ACTIVO { get; set; }
        public string DS_INSCRITO_IMSS { get; set; }
        public string DS_PERSONA_FISCA { get; set; }
        public string CL_LADA { get; set; }
        public string NO_TELEFONO { get; set; }
        public string CL_REGIMEN_FISCAL { get; set; }
        public string NB_REGIMEN_FISCAL { get; set; }
        public Nullable<bool> CL_SUBCONTRATACION { get; set; }
        public string DS_SUBCONTRATACION { get; set; }
        public string NB_CER_FILE { get; set; }
        public string CL_PASS_FILE { get; set; }
        public string CL_PASS_PAC { get; set; }
        public string CL_USUARIO_PAC { get; set; }
        public string NB_KEY_FILE { get; set; }
        public string PATH_CERTIFICADO { get; set; }
        public string CL_MODO_EJECUCION { get; set; }
        public Nullable<bool> FG_E_HTML { get; set; }
        public string CL_E_NAME { get; set; }
        public string CL_E_SERVER { get; set; }
        public string CL_E_SENDER { get; set; }
        public Nullable<bool> FG_E_SSL { get; set; }
        public string CL_E_PASSWORD { get; set; }
        public string CL_E_USER { get; set; }
        public Nullable<int> NO_E_PORT { get; set; }
        public string NO_CUENTA_BANAMEX { get; set; }
        public string CL_SUCURSAL_BANAMEX { get; set; }
        public string CL_CONTRATO_BANAMEX { get; set; }
        public string DS_CUERPO_RECIBO { get; set; }
    }
}
