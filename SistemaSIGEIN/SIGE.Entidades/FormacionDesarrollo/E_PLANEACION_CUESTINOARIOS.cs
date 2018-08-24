using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_PLANEACION_CUESTINOARIOS
    {
        public int ID_EMPLEADO_EVALUADOR { get; set; }
        public int ID_EVALUADO_EVALUADOR { get; set; }
        public string CL_EMPLEADO { get; set; }
        public string NB_EMPLEADO_COMPLETO { get; set; }
        public int ID_PUESTO { get; set; }
        public string CL_PUESTO { get; set; }
        public string NB_PUESTO { get; set; }
        public string CL_ROL_EVALUADOR { get; set; }
        public Nullable<int> ID_EMPLEADO_EVALUADO { get; set; }
        public Nullable<int> ID_EVALUADO { get; set; }
        public bool FG_CUESTIONARIO { get; set; }
        public string CL_ACCION { get; set; }

        public E_PLANEACION_CUESTINOARIOS()
        {
            CL_ACCION = "";
        }

    }
}
