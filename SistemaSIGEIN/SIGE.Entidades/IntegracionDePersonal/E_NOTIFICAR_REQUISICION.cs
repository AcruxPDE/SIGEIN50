using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.IntegracionDePersonal
{
    [Serializable]
    public class E_NOTIFICAR_REQUISICION
    {
        public Nullable<Decimal> MN_SUELDO { get; set; }
        public string DESCRIPCION { get; set; }
        public string CUALIDADES { get; set; }
        public string NB_AREA { get; set; }
        public int EDADMAX { get; set; }
        public int EDADMIN { get; set; }
        public string EXPERIENCIA { get; set; }
        public string JUSTIFICACION { get; set; }
        public string GENERO { get; set; }
        public string ESCOLARIDAD { get; set; }
        public string NB_PUESTO { get; set; }
        public int ID_PUESTO { get; set; }
        public DateTime? FE_REQUERIMIENTO { get; set; }
    }
}
