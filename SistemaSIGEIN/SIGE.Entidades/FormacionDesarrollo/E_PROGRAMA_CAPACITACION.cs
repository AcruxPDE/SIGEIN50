using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
   public class E_PROGRAMA_CAPACITACION
    {
        public int ID_PROGRAMA_EMPLEADO_COMPETENCIA { get; set; }
        public int ID_PROGRAMA { get; set; }
        public int ID_PROGRAMA_COMPETENCIA { get; set; }
        public int ID_PROGRAMA_EMPLEADO { get; set; }
        public string CL_PRIORIDAD { get; set; }
        public decimal PR_RESULTADO { get; set; }
        public Nullable<int> ID_PERIODO { get; set; }
        public string CL_PROGRAMA { get; set; }
        public string NB_PROGRAMA { get; set; }
        public string CL_TIPO_PROGRAMA { get; set; }
        public string CL_ESTADO { get; set; }
        public string CL_VERSION { get; set; }
        public Nullable<int> ID_DOCUMENTO_AUTORIZACION { get; set; }
        public string DS_NOTAS { get; set; }
        public Nullable<int> ID_COMPETENCIA { get; set; }
        public string NB_COMPETENCIA { get; set; }
        public string NB_CLASIFICACION_COMPETENCIA { get; set; }
        public string CL_TIPO_COMPETENCIA { get; set; }
        public string CL_COLOR { get; set; }
        public Nullable<int> ID_EMPLEADO { get; set; }
        public string NB_EVALUADO { get; set; }
        public string CL_EVALUADO { get; set; }
        public string NB_PUESTO { get; set; }
        public string CL_PUESTO { get; set; }
        public string NB_DEPARTAMENTO { get; set; }
        public string CL_PERIODO { get; set; }
        public string NB_PERIODO { get; set; }
        public string DS_PERIODO { get; set; }
        public string TIPO_EVALUACION { get; set; }
        public int? ID_AUXILIAR { get; set; }

    }
}
