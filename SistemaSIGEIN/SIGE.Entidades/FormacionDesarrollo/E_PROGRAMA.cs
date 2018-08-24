using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_PROGRAMA
    {
        public int ID_PROGRAMA { get; set; }
        public Nullable<int> ID_PERIODO { get; set; }
        public string CL_PROGRAMA { get; set; }
        public string NB_PROGRAMA { get; set; }
        public string CL_TIPO_PROGRAMA { get; set; }
        public string CL_ESTADO { get; set; }
        public string CL_AUTORIZACION { get; set; }
        public string DS_NOTAS { get; set; }
        public Nullable<int> ID_DOCUMENTO_AUTORIZACION { get; set; }
        public string CL_DOCUMENTO { get; set; }
        public string VERSION { get; set; }
        public Nullable<int> NO_COMPETENCIAS { get; set; }
        public Nullable<int> NO_PARTICIPANTES { get; set; }
        public System.DateTime FE_CREACION { get; set; }
    }
}
