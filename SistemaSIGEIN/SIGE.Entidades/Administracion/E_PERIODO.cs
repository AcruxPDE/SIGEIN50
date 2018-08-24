using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_PERIODO
    {
        public int ID_PERIODO { get; set; }
        public string CL_PERIODO { get; set; }
        public string NB_PERIODO { get; set; }
        public string DS_PERIODO { get; set; }
        public System.DateTime FE_INICIO { get; set; }
        public Nullable<System.DateTime> FE_TERMINO { get; set; }
        public string CL_ESTADO_PERIODO { get; set; }
        public string CL_TIPO_PERIODO { get; set; }
        public string DS_NOTAS { get; set; }
        public Nullable<int> ID_BITACORA { get; set; }
        public string XML_CAMPOS_ADICIONALES { get; set; }
        public string TIPO_EVALUACION { get; set; }
        public string DS_FILTRO { get; set; }

        //public string CL_ESTADO_PERIODO { get; set; }
        //public string CL_PERIODO { get; set; }
        //public string DS_PERIODO { get; set; }
        //public Nullable<System.DateTime> FE_MODIFICACION { get; set; }
        //public string FE_TERMINO { get; set; }
        //public int? ID_PERIODO { get; set; }
        //public string NB_PERIODO { get; set; }
        //public string XML_CAMPOS_ADICIONALES { get; set; } 
    }
}
