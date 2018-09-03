using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
   public class E_CENTROS_OPTVOS
    {
        public System.Guid ID_CENTRO_OPTVO { get; set; }
        public string CL_CLIENTE { get; set; }
        public string CL_CENTRO_OPTVO { get; set; }
        public string NB_CENTRO_OPTVO { get; set; }
        public string NB_CALLE { get; set; }
        public string NB_NO_EXTERIOR { get; set; }
        public string NB_NO_INTERIOR { get; set; }
        public string NB_COLONIA { get; set; }
        public string CL_ESTADO { get; set; }
        public string NB_ESTADO { get; set; }
        public string CL_MUNICIPIO { get; set; }
        public string NB_MUNICIPIO { get; set; }
        public string CL_CODIGO_POSTAL { get; set; }
        public string DOMICILIO { get; set; }
    }
}
