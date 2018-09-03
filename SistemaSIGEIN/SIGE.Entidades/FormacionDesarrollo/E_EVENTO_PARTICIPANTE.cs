using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_EVENTO_PARTICIPANTE
    {
        public int NO_FILA { get; set; }
        public int ID_EVENTO_PARTICIPANTE { get; set; }
        public int ID_EVENTO { get; set; }
        public Nullable<int> ID_EMPLEADO { get; set; }
        public string CL_PARTICIPANTE { get; set; }
        public string NB_PARTICIPANTE { get; set; }
        public string NB_PUESTO { get; set; }
        public string NB_DEPARTAMENTO { get; set; }
        public Nullable<int> NO_TIEMPO { get; set; }
        public Nullable<decimal> PR_CUMPLIMIENTO { get; set; }
        public string DS_CUMPLIMIENTO { get; set; }
        public decimal NO_DURACION { get; set; }
        public string CL_CORREO_ELECTRONICO { get; set; }
    }
}
