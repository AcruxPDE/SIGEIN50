using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_ANTIGUEDAD
    {
        public System.Guid ID_TABLA_ANTIGUEDAD { get; set; }
        public string CL_CLIENTE { get; set; }
        public System.Guid ID_PAQUETE_PRESTACIONES { get; set; }
        public short NO_ANTIGUEDAD { get; set; }
        public decimal NO_DIAS_VACACIONES { get; set; }
        public decimal NO_DIAS_PRIMA_VAC { get; set; }
        public decimal NO_FACTOR_SBC { get; set; }
        public Nullable<decimal> NO_CAMPO01 { get; set; }
        public Nullable<decimal> NO_CAMPO02 { get; set; }
        public Nullable<decimal> NO_CAMPO03 { get; set; }
        public Nullable<decimal> NO_CAMPO04 { get; set; }
        public Nullable<decimal> NO_CAMPO05 { get; set; }
    }
}
