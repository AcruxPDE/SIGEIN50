using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
    [Serializable]
    public class E_OBTIENE_FUNCIONES_METAS
    {
        public int ID_EVALUADO { get; set; }
        public int ID_PERIODO { get; set; }
        public Nullable<int> ID_EMPLEADO { get; set; }
        public int ID_PUESTO { get; set; }
        public string DS_PUESTO_FUNCION { get; set; }
    }
}
